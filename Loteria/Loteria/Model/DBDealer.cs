namespace Loteria.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    public class DBDealer
    {
        //s� vai ter um sorteio no bd mesmo ent�o vou criar uma variavel est�tica pra poder acessar ela por toda classe mesmo sem instanciar.
        private static MegaSenaRaffle MSRaffle;
        //private MegaSenaTicket MSTickets;

        //conecta ao banco de dados e retorna a conex�o.
        private static SqlConnection startConnection()
        {
            SqlConnection con = new SqlConnection();
            //essa connection string ta em App.config
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Loteria"].ConnectionString;
            con.Open();

            //da uma checada no banco de dados e garante que ele est� pronto caso seja o primeiro acesso
            using (SqlCommand command = new SqlCommand("SELECT * FROM Raffle", con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    //se entrou aqui n�o tem nenhum sorteio da mega sena cadastrado, e o necessario � que tenha 1 pra formar o vinculo
                    if (!reader.Read())
                    {
                        //ent�o cria
                        MSRaffle = new MegaSenaRaffle();
                        SqlCommand initializeCommand = new SqlCommand("INSERT INTO Raffle (result) output INSERTED.ID VALUES ('0')", con);
                        MSRaffle.id=(int)initializeCommand.ExecuteScalar();
                    }
                }
            }

            return con;
        }
        public static MegaSenaTicket[] getMSTickets()
        {
            List<MegaSenaTicket> tickets=new List<MegaSenaTicket>();
            SqlConnection con = startConnection();
            using (SqlCommand command = new SqlCommand("SELECT * FROM RaffleTicket", con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MegaSenaTicket ticket = new MegaSenaTicket(Convert.ToInt32(reader["id"]),
                            Convert.ToDateTime(reader["date"]),
                            Convert.ToString(reader["numbers"]),
                            MSRaffle);
                        tickets.Add(ticket);
                    }
                }
            }
            con.Close();
            return tickets.ToArray();
        }
        public static void addMSTicket(MegaSenaTicket ticket)
        {
            SqlConnection con = startConnection();
            using (SqlCommand cmd = new SqlCommand("INSERT INTO RaffleTicket(numbers,raffle_id) output INSERTED.ID VALUES(@numbers,@fkey)", con))
            {                                                                //

                cmd.Parameters.AddWithValue("@numbers", string.Join(" ",ticket.numbers));
                cmd.Parameters.AddWithValue("@fkey", MSRaffle.id);

                int id = (int)cmd.ExecuteScalar();

                ticket.id = id;
                con.Close();
            }
        }
        public static void doRaffle(int[] numbers)
        {
            SqlConnection con = startConnection();
            //atualiza o sorteio que ja tem no banco de dados (abrir uma conex�o cria um se estiver vazio), em teoria sempre haver� um quando
            //uma conex�o estiver aberta
            using (SqlCommand cmd = new SqlCommand("UPDATE Raffle SET result = @result where id=@id", con))
            {
                //troca o resultado no banco de dados pelos n�meros que entram como argumento.
                cmd.Parameters.AddWithValue("@result", string.Join(" ", numbers));
                cmd.Parameters.AddWithValue("@id", MSRaffle.id);
                cmd.ExecuteNonQuery();
                con.Close();
                //atualiza o resultado do sorteio que est� em mem�ria.
                MSRaffle.result = numbers;
            }
        }


    }
}
