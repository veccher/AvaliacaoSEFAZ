using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loteria.Controller;
using Loteria.Model;
/*
Autor: Vinícius Carvalho Eccher
*/
namespace Loteria.View
{
    /* Classe responsável pela interface com o usuario */
    static class View
    {
        static void newRaffleTicket(bool invalidInputMsg = false, int lastId = -1, int[] lastRaffleNumbers = null)
        {
            //declaração 
            int[] raffleNumbers=new int[6];
            string input;
            int id=-1,i=0;
            Console.Clear();

            //mensagens de ultimas chamadas
            if (lastId>=0 && lastRaffleNumbers.Length==6)
            {
                Console.WriteLine("Aposta com id: " + lastId.ToString() + " criada, números escolhidos: " +
                    String.Join(", ", lastRaffleNumbers.Select(p => p.ToString()).ToArray()) + "\n");
            }
            else if (invalidInputMsg)
            {
                Console.WriteLine("Entrada invalida, certifique-se de usar uma das entradas abaixo:\n ");
            }

            //Criação de nova aposta.
            //oferece as opções ao usuario instruindo a oferecer a entrada.
            Console.WriteLine("Digite os números separados por espaço para cadastrar uma nova aposta, são 6 números não repetidos de 1 a 60\n");
            Console.WriteLine("Caso deseja surpresinha (números escolhidos automaticamente de forma aleatória) digite apenas a letra s");
            Console.WriteLine("Para voltar ao menu principal digite a palavra: voltar");
            input = Console.ReadLine();
            try
            {
                foreach (string digit in input.Split())
                {
                    /*volta pro menu principal caso o usuario entre 'voltar', essa função vai continuar na pilha, desperdiçando um pouco de memória
                     mas o impacto deve ser insignificante*/
                    if (digit.Equals("voltar") || digit.Equals("Voltar") || digit.Equals("VOLTAR"))
                    {
                        mainMenu();
                        //A Saida do programa não deve ser executada, já que o programa não deve retornar a essa função depois de entrar no menu
                        //mas fica aqui por precaução.
                        Environment.Exit(1);
                    }
                    //se o usuario quer surpresinha
                    if (digit.Equals("s") || digit.Equals("S"))
                    {
                        //defino o vetor como vazio.
                        raffleNumbers = null;
                        break;
                    }
                    //le as entradas e converte para inteiro.
                    raffleNumbers[i++] = Convert.ToInt32(digit);
                }
                //Cria a aposta
                id=RaffleController.createRaffleTicket(ref raffleNumbers);
                //recomeça a função, passando os dados da ultima aposta.
                newRaffleTicket(false, id, raffleNumbers);
                
            }
            //essa exceção será lançada se a entrada do usuario não estiver de acordo com o especificado.
            catch (ArgumentException)
            {
                newRaffleTicket(true);
            }
            //essa aqui é se der merda mesmo.
            catch (Exception ex)
            {
                newRaffleTicket(true);
            }

        }

        //Realiza o sorteio da mega sena.
        static void DoRaffle (bool invalidInputMsg = false)
        {
            //declaração 
            int[] raffleNumbers = new int[6];
            string input;
            int id = -1, i = 0;
            Console.Clear();

            if (invalidInputMsg)
            {
                Console.WriteLine("Entrada invalida, certifique-se de usar uma das entradas abaixo:\n ");
            }

            //Criação de novo sorteio.
            //oferece as opções ao usuario instruindo a oferecer a entrada.
            Console.WriteLine("Digite os números separados por espaço para cadastrar o sorteio caso ele ja tenha sido realizado, "
                + "são 6 números não repetidos de 1 a 60\n");
            Console.WriteLine("Caso deseje que o sistema realize o sorteio e escolha os números digite apenas a letra s");
            Console.WriteLine("Para voltar ao menu principal digite a palavra: voltar");
            input = Console.ReadLine();
            try
            {
                foreach (string digit in input.Split())
                {
                    /*volta pro menu principal caso o usuario entre 'voltar', essa função vai continuar na pilha, desperdiçando um pouco de memória
                     mas o impacto deve ser insignificante*/
                    if (digit.Equals("voltar") || digit.Equals("Voltar") || digit.Equals("VOLTAR"))
                    {
                        mainMenu();
                        //A Saida do programa não deve ser executada, já que o programa não deve retornar a essa função depois de entrar no menu
                        //mas fica aqui por precaução.
                        Environment.Exit(1);
                    }
                    //se o usuario quer que o programa realize o sorteio
                    if (digit.Equals("s") || digit.Equals("S"))
                    {
                        //defino o vetor como vazio.
                        raffleNumbers = null;
                        break;
                    }
                    //le as entradas e converte para inteiro.
                    raffleNumbers[i++] = Convert.ToInt32(digit);
                }
                //Cria o sorteio
                RaffleController.doRaffle(ref raffleNumbers);
                //exibe o resultado
                Console.Clear();
                Console.WriteLine("\nnúmeros sorteados:"+ string.Join(" ", raffleNumbers) + "\nvencedores: \n");
                Console.WriteLine("id | numeros | acertos");

                foreach (RaffleTicket ticket in RaffleController.getRaffleWinners().OrderBy(item => item.getPontuation()))
                {
                    Console.WriteLine(ticket.id + " | " + string.Join(" ", ticket.numbers) + " | " + ticket.getPontuation());
                }
                Console.WriteLine("\n pressione qualquer tecla para voltar ao menu principal: \n");
                Console.ReadLine();
                mainMenu();

            }
            //essa exceção será lançada se a entrada do usuario não estiver de acordo com o especificado.
            catch (ArgumentException)
            {
                DoRaffle(true);
            }
            //essa aqui é se der merda mesmo.
            catch (Exception)
            {
                DoRaffle(true);
            }

            

        }
        //exibe o menu principal/inicial, o parametro de entrada é caso o menu esteja sendo aberto logo após uma entrada invalida.
        static void mainMenu(bool invalidInputMsg=false)
        {
            int option=5;
            Console.Clear();
            if (invalidInputMsg)
            {
                Console.WriteLine("Entrada invalida, certifique-se de usar uma das entradas abaixo:\n ");
            }
            //oferece opções ao usuario.
            Console.WriteLine("Digite 1 para cadastrar uma nova aposta");
            Console.WriteLine("Digite 2 para realizar o sorteio");
            Console.WriteLine("Digite 3 para encerrar o programa");
            //tenta ler e converter a entrada pra inteiro,se não conseguir não tem problema, pq option foi iniciado com 5 logo vai dar entrada invalida
            Int32.TryParse(Console.ReadLine(), out option);  
            //decide o que fazer com base na entrada do usuario.
            switch (option)
            {
                //Apresenta a interface para cadastrar aposta.
                case 1:
                    newRaffleTicket();
                    break;
                //Apresenta a interface para realizar/cadastrar o sorteio.
                case 2:
                    DoRaffle();
                    break;
                //Sai do programa
                case 3:
                    Environment.Exit(1);
                    break;
                default:
                    //caso não tenha entrado em nenhum outro menu, foi por que a entrada foi invalida, retorna avisando ao usuario.
                    mainMenu(true);
                    break;
            }
        }

        static void Main(string[] args)
        {
            mainMenu();
        }
    }
}
