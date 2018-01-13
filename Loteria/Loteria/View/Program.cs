using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loteria.Controller;
/*
 Autor: Vinícius Carvalho Eccher
     */
namespace Loteria.View
{
    /* Classe responsável pela interface com o usuario */
    static class View
    {
        static void newRaffleTicket (bool invalidInputMsg = false)
        {
            int[] raffleNumbers=new int[6];
            int id=-1,i=0;
            if (invalidInputMsg)
            {
                Console.WriteLine("Entrada invalida, certifique-se de usar uma das entradas abaixo:\n ");
            }
            //oferece as opções ao usuario instruindo a oferecer a entrada.
            Console.WriteLine("Digite os números separados por espaço para cadastrar uma nova aposta, são 6 números não repetidos de 1 a 60\n");
            Console.WriteLine("Caso deseja surpresinha (números escolhidos automaticamente de forma aleatória) digite apenas a letra s");
            try
            {
                foreach (string digit in Console.ReadLine().Split())
                {
                    //se o usuario quer surpresinha
                    if (digit.Equals("s") || digit.Equals("S"))
                    {
                        //defino o vetor como vazio.
                        raffleNumbers = null;
                        break;
                    }
                    raffleNumbers[i++] = Convert.ToInt32(digit);
                }
                id=RaffleController.createRaffleTicket(ref raffleNumbers);
            }
            //essa exceção será lançada se a entrada do usuario não estiver de acordo com o especificado.
            catch (ArgumentException)
            {
                newRaffleTicket(true);
            }
            //essa aqui é se der merda mesmo.
            catch (Exception ex)
            {
                Console.WriteLine("exceção não esperada " + ex.Message);
            }
            //le as entradas e converte para inteiro.
            
        }
        static void DoRaffle ()
        {

        }
        //exibe o menu principal/inicial, o parametro de entrada é caso o menu esteja sendo aberto logo após uma entrada invalida.
        static void mainMenu(bool invalidInputMsg=false)
        {
            int option=3;
            Console.Clear();
            if (invalidInputMsg)
            {
                Console.WriteLine("Entrada invalida, certifique-se de usar uma das entradas abaixo:\n ");
            }
            //oferece opções ao usuario.
            Console.WriteLine("Digite 1 para cadastrar uma nova aposta");
            Console.WriteLine("Digite 2 para realizar o sorteio");
            Console.WriteLine("Digite 3 para encerrar o programa");
            option = Console.Read() - 48; //subtraindo 48 por que esse resultado retorna o inteiro na tabela ascii, onde 0=48
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
                //Retorna pedido para sair do programa.
                case 3:
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
