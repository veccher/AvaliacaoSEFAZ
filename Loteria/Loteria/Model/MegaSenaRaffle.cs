using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Model
{
    public class MegaSenaRaffle:Raffle
    {
        //número de números necessários para realizar uma aposta.
        public static int neededNumbers = 6;
        //construtor vazio (antes de realizar o sorteio).
        public MegaSenaRaffle()
        {
            this.id = 1;
        }
        //construtor, atribui o resultado passado caso a entrada seja valida, caso contrário atribui um resultado aleatório.
        //na pratica esse construtor não está sendo usado.
        public MegaSenaRaffle(int[] result)
        {
            //se a entrada é nula, surpresinha.
            if (result == null)
                this.result = MegaSenaRaffle.RandomRaffle();
            //se não é nula, e tem 6 valores distintos
            else if (result.Length == MegaSenaRaffle.neededNumbers && result.Distinct().ToArray().Length == result.Length)
                this.result = result;
            //se não é uma entrada invalida, reclama.
            else
                throw new ArgumentException();
            this.save();
        }
        public override bool save()
        {
            DBDealer.doRaffle(this.result);
            return true;
        }
        public static int [] RandomRaffle()
        {
            int[] rand = new Int32[neededNumbers];
            Random randnum = new Random();
            for (int i=0; i < neededNumbers; i++)
            {
                //gera um número aleatório n tal que 1<=n<61
                int temp = randnum.Next(1, 61);
                //se o número aleatório já está contido no vetor
                if (rand.Contains(temp))
                {
                    //decrementa (na pratica isso serve pra manter o i inalterado, eu decremento por que ele vai ser incrementado após o continue)
                    i--;
                    //e recomeça
                    continue;
                }
                else
                    rand[i] = temp;
            }
            return rand;
        }
    }
}
