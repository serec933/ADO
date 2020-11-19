using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO
{
    public class ConnectedMode
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security= true; Initial Catalog = CinemaDB; Server = WINAPMGUDJIV7BS\SQLEXPRESS";

        public static void Connected()
        {
            //creare una connessione
                //Primo metodo:
                    // SqlConnection connection1 = new SqlConnection();
                    //connection1.ConnectionString = connectionString;
                //Secondo metodo:
                    //SqlConnection connection2 = new SqlConnection(connectionString);
                //Terzo Metodo
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                            //aprire la connessione
                             connection.Open();

                             //creare command
                             SqlCommand command = new SqlCommand();
                             command.Connection = connection;
                            command.CommandType = System.Data.CommandType.Text;
                            command.CommandText = "SELECT * FROM Movies";

                            //Eseguire Command -> DataReader
                            SqlDataReader reader = command.ExecuteReader();
                             //Leggere i dati
                            while (reader.Read())
                            {
                            Console.WriteLine("{0} - {1} {2} {3}",
                            reader["ID"], reader["Title"], reader["Genere"], reader["Durata"]);

                        }
                        connection.Close();
                        reader.Close();
                    }

        }

        public static void ConnctedPar()
        {
            //Creare connessione 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //inserimento del paramentro da riga di comando 
                Console.WriteLine("Genere del film?");
                string Genere;
                Genere = Console.ReadLine();

                //Aprire le connessione
                connection.Open();
                //creare il command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Movies WHERE Genere = @Genere";

                //Creare parametri
                SqlParameter genereParam = new SqlParameter();
                genereParam.ParameterName = "@Genere";

                //QUESTE DUE RIGHE EQUIVALENRI A QUELLE SOTTO
                genereParam.Value = Genere;
                command.Parameters.Add(genereParam);

                //command.Parameters.AddWithValue("@Genere",Genere);

                //Eseguire il command
                SqlDataReader reader = command.ExecuteReader();

                //Lettura dei dati
                while (reader.Read())
                {
                    Console.WriteLine("{0}-{1} {2}",
                        reader["ID"],reader["Title"],reader["Genere"]);
                }
                //Chiudere connessione
                connection.Close();
                reader.Close();
            }
        }

        public static void ConnectedSTP()
        {
            //Creare connessione 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Aprire le connessione
                connection.Open();
                //creare il command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "stpGetActorsByCachet";

                //Creare parametri
                command.Parameters.AddWithValue("@cachetmin",5000);
                command.Parameters.AddWithValue("@cachetmax", 9000);
                
                //Creare il valore di ritorno, ha un output
                SqlParameter returnValue = new SqlParameter();
                returnValue.ParameterName = "@returnCount";
                returnValue.SqlDbType = System.Data.SqlDbType.Int;
                returnValue.Direction = System.Data.ParameterDirection.Output;

                command.Parameters.Add(returnValue);
                //Eseguire il command
                //SqlDataReader reader = command.ExecuteReader();               

                ////Lettura dei dati
                //while (reader.Read())
                //{
                //    Console.WriteLine("{0}-{1} {2}",
                //        reader["ID"], reader["FirstName"], reader["LastName"],
                //        reader["Cachet"]);
                //}
                ////Chiudere connessione
                //reader.Close();
                //Parametro di ritorno
                command.ExecuteNonQuery(); //NON VOGLIO VEDERE LA TABELLA
                Console.WriteLine("#Actors: {0}", command.Parameters["@returnCount"].Value);

                connection.Close();
            }
        }

        public static void ConnectedScalar()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand scalarCommand = new SqlCommand();
                scalarCommand.Connection = connection;
                scalarCommand.CommandType = System.Data.CommandType.Text;
                scalarCommand.CommandText = "SELECT COUNT(*) FROM Movies";

                int count = (int)scalarCommand.ExecuteScalar();

                Console.WriteLine("Il numero di film è {0}", count);
                connection.Close();
            }   
        }
    }
}
