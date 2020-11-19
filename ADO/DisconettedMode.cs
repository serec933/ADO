using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADO
{
    public class DisconettedMode
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security= true; Initial Catalog = CinemaDB; Server = WINAPMGUDJIV7BS\SQLEXPRESS";

        public static void Disconnected()
        {
            using (SqlConnection connction = new SqlConnection(connectionString))
            {
                //costruzione adapter
                SqlDataAdapter adapter = new SqlDataAdapter();

                //Creazione dei comandi da associare all'adapter

                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connction;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "SELECT * FROM Movies";

                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connction;
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO Movies VALUES(@titolo,@genere,@durata)";

                insertCommand.Parameters.Add("@titolo", System.Data.SqlDbType.NVarChar, 255, "Title");
                insertCommand.Parameters.Add("@genere", System.Data.SqlDbType.NVarChar, 255, "Genere");
                insertCommand.Parameters.Add("@durata", System.Data.SqlDbType.Int, 500, "Durata");

                //Associa i comandi all'adapter

                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;

                //Creiamo il dataset

                System.Data.DataSet dataset = new System.Data.DataSet();

                try
                {
                    connction.Open();
                    adapter.Fill(dataset, "Movies");
                    //Creiamo il recordo
                    DataRow movie = dataset.Tables["Movies"].NewRow();
                    movie["Title"] = "V per vendetta";
                    movie["Genere"] = "Azione";
                    movie["Durata"] = 125;

                    dataset.Tables["Movies"].Rows.Add(movie);

                    //Update
                    adapter.Update(dataset, "Movies");
                    //QUI STO USANDO I COMENDI SQL CHE AVEVO DEFINITO PRIMA
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    connction.Close();
                }
            }
        }
    }
}