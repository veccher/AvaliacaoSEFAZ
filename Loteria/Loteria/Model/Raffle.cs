using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Loteria.Model
{
    /*No nosso caso, pelo RF3 "a aplicação deve permitir cadastrar UM sorteio de 6 números aleatórios", teremos apenas 1 sorteio, onde
     todas as apostas estarão relacionadas a este sorteio, mas como no caso da aplicação real sabemos que isso ocorre todo mes, então esse código
     ja deixa preparado para futuras atualizações onde fizessemos um sorteio todo mês e manter informações dos sorteios anteriores.*/
    public abstract class Raffle
    {
        //Data do sorteio
        public DateTime date { get; protected set; }
        //Código identificador do sorteio
        public int id { get; protected set; }
        //resultado do sorteio (números sorteados)
        public int[] result { get; protected set; }
        //salvo 
        public abstract bool save();

    }
}
