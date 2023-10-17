using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{

    public class DataBaseHandler
    {
        public static SqlCommand? command;
        public static SqlDataAdapter? adapter;
        public static string? sql;
        public static SqlConnection? cnn;
        public static string usernameSecret = File.ReadAllText(@"C:\ProjectSecrets\username.txt");
        public static string username = usernameSecret;
        public static string dataSourceName = File.ReadAllText(@"C:\ProjectSecrets\dataSourceName.txt");
        public static string dataSource = dataSourceName;
        public static string dataBase = "dbProduct";
        public static string path = @"C:\ProjectSecrets\EcommerceSecrets.txt";
        public static string pwd = File.ReadAllText(path);

        public static void ConnectToSqlDb()
        {
   

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Storage of password in variable was successful...");
            Console.ResetColor();
            Thread.Sleep(50);


            //Attempt to connect console application to server database

            //variable declaration
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source={dataSource};Initial Catalog={dataBase};User ID={username};Password={pwd}";

            //assign connection
            cnn = new SqlConnection(connectionString);

            //See if the connection works
            try //if connection to db is successful
            {
                cnn.Open();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Connection to SQL database was successful... ");
                Console.ResetColor();
                Thread.Sleep(50);
                //cnn.Close(); Move this to TurnOffConnectionToDb method when user enters q
            }
            catch (Exception ex) //if connection to db is unsuccessful
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot open connection... ");
                Console.ResetColor();
                Thread.Sleep(3000);
                Environment.Exit(0); //exit program
            }
        }
        public static void CloseSqlConnection()
        {
            command?.Dispose();
            cnn?.Close();
        }

        public static void SetSqlVariables(out SqlDataAdapter adapter, out string sql, out SqlConnection cnn)
        {
            //set sql variables
            SqlCommand command;
            adapter = new SqlDataAdapter();
            sql = "";
            string connectionString = null!;
            connectionString = $"Data Source={dataSource};Initial Catalog={dataBase};User ID={username};Password={pwd}";
            cnn = new SqlConnection(connectionString);
        }

        public static void InstantiateJsonFileFromSqlDb(List<Product> ListOfProducts)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Storage of password in variable was successful...");
            Console.ResetColor();
            Thread.Sleep(500);

            //Attempt to connect console application to server database

            //variable declaration
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source={dataSource};Initial Catalog={dataBase};User ID={username};Password={pwd}";

            //assign connection
            cnn = new SqlConnection(connectionString);

            //See if the connection works
            try //if connection to db is successful
            {
                cnn.Open();

            }
            catch (Exception ex) //if connection to db is unsuccessful
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot open connection... ");
                Console.ResetColor();
                Thread.Sleep(3000);
            }

            //create sql commands to be able to read from db
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";
            sql = "Select Identify,Id,NameOfProduct,Description from dbo.Product";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();


            //convert what is in the db and deserialize into json file
            List<Product> products = new List<Product>();

            while (dataReader.Read())
            {
                Product product = new Product();
                product.Id = (string)dataReader["Id"];
                product.NameOfProduct = (string)dataReader["NameOfProduct"];
                product.Description = (string)dataReader["Description"];
                products.Add(product);
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();

            ProductFileManager.SerializeToJsonFile(products); //serializes the most up to date list into a json file
        }
        public static void ViewSqlDb()
        {
            DataBaseHandler.SetSqlVariables(out adapter, out sql, out cnn);

            //assign connection
            SqlDataReader dataReader;
            String sql1, Output = "";

            //cnn = new SqlConnection(connectionString);
            cnn.Open();
            command = new SqlCommand(sql, cnn);
            command.CommandText = "Select Identify,Id,NameOfProduct,Description from dbo.Product";
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2) + " - " + dataReader.GetValue(3) + "\n";
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SQL database:");
            Console.ResetColor();
            Console.WriteLine(Output);

            dataReader.Close();

            CloseSqlConnection();
        }

        //public static void MakeIdentifyColumnNumberingUpToDate()
        //{
        //    SetSqlVariables(out adapter, out sql, out cnn);

        //    cnn.Open();

        //    //Make Identify to be sequential numbering
        //    sql = "DECLARE @id INT SET @id = 0 UPDATE dbo.Product SET @id = Identify = @id + 1";

        //    command = new SqlCommand(sql, cnn);
        //    adapter.UpdateCommand = new SqlCommand(sql, cnn);
        //    adapter.UpdateCommand.ExecuteNonQuery();

        //    CloseSqlConnection();
        //}
    }
}
