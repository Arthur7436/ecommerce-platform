using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Database
{

    public class ConnectToSqlDB : DataBaseHandler
    {
        public static void ConnectToSqlDb()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Storage of password in variable was successful...");
            Console.ResetColor();
            Thread.Sleep(10);

            DataBaseHandler database = new DataBaseHandler();

            //Attempt to connect console application to server database

            //variable declaration
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source={dataSource};Initial Catalog={dataBase};User ID={database.username};Password={pwd}";

            //assign connection
            cnn = new SqlConnection(connectionString);

            //See if the connection works
            try //if connection to db is successful
            {
                cnn.Open();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Connection to SQL database was successful... ");
                Console.ResetColor();
                Thread.Sleep(10);
                //cnn.Close(); Move this to TurnOffConnectionToDb method when user enters q
            }
            catch (Exception ex) //if connection to db is unsuccessful
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot open connection... ");
                Console.ResetColor();
                Thread.Sleep(1000);
                Environment.Exit(0); //exit program
            }
        }
    }
}
