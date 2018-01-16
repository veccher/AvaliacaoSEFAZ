using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loteria.Model;

namespace Loteria.Controller
{
    public static class RaffleController
    {
        //cria um jogo, insere no banco de dados, retorna o id, se o id estiver vazio uma surprezinha é criada. A entrada é por referencia
        //por que no caso de uma surpresinha os números selecionados são armazenados nesse vetor para informar ao usuario.
        public static int createRaffleTicket(ref int[] raffleNumbers)
        {
            MegaSenaTicket ticket = new MegaSenaTicket(raffleNumbers);
            //eu faço essa atribuição por que caso a entrada dessa função foi null (surpresinha) eu atualizo o raffle numbers pra "retornar"
            //os números que foram gerados aleatóriamente.
            raffleNumbers = ticket.numbers; 
            return ticket.id;
        }

        //Adiciona o resultado do jogo.
        public static void doRaffle(ref int[] raffleNumbers)
        {
            //se entrou com null é por que pediu pro sistema sortear.
            if (raffleNumbers == null)
                //o sistema sorteia e atualiza a entrada que foi passada como referencia e serve como saida.
                raffleNumbers = MegaSenaRaffle.RandomRaffle();
            //se não for null e não tem tamanho certo então deu ruim, reclama dos argumentos.
            else if (raffleNumbers.Length != MegaSenaRaffle.neededNumbers || raffleNumbers.Min()<1 || raffleNumbers.Max()>60)
                throw new ArgumentException();
            DBDealer.doRaffle(raffleNumbers);
        }

        public static RaffleTicket[] getRaffleWinners()
        {
            
            return DBDealer.getMSTickets().Where(e => e.isWinner()).ToArray();
        }
    }
}
