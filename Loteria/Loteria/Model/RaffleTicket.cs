using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Model
{
    public abstract class RaffleTicket
    {
        //data de criação da aposta

        public DateTime date { get; protected set; }
        //Código identificador da aposta

        public int id { get;  set; }
        //números escolhidos
        public int[] numbers { get; protected set; }
        //Sorteio vinculado a essa aposta
        public Raffle raffle;

        //salva
        public abstract bool save();
        //da uma "pontuação" pra a aposta
        public virtual int getPontuation()
        {
            return raffle.result.Intersect(numbers).Count();
        }

        public abstract bool isWinner();

    }
}
