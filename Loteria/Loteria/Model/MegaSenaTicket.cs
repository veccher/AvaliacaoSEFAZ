using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Model
{
    public class MegaSenaTicket : RaffleTicket
    {
        //pra criar um novo
        public MegaSenaTicket(int[] numbers)
        {
            //se a entrada é nula, surpresinha.
            if (numbers == null)
                this.numbers = MegaSenaRaffle.RandomRaffle();
            //se não é nula, e tem 6 valores distintos e entre 1 e 60
            else if (numbers.Length == MegaSenaRaffle.neededNumbers && numbers.Distinct().ToArray().Length == numbers.Length && numbers.Min()>=1 && numbers.Max()<=60)
                this.numbers = numbers;
            //se não é uma entrada invalida, reclama.
            else
                throw new ArgumentException();
            this.save();
        }
        //pra instanciar um que ja existe no banco de dados.
        public MegaSenaTicket(int id, DateTime date, string numbers, Raffle raffle)
        {
            this.id = id;
            this.date = date;
            //quebra a string (como está no bd) pra um array de inteiros
            this.numbers = Array.ConvertAll(numbers.Split(), s => Int32.Parse(s));
            this.raffle = raffle;
        }
        public override bool isWinner()
        {
            int count = 0;//conta o número de "coincidencias" na aposta e no resultado
            for (int i=0;i<MegaSenaRaffle.neededNumbers;i++)
            {
                if (raffle.result.Contains(numbers[i]))
                    count++;
            }//uma aposta é vencedora se tem mais de 4 acertos.
            return count>=4;
        }

        public override bool save()
        {
            DBDealer.addMSTicket(this);
            return true;
        }
    }
}
