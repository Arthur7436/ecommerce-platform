using ECommerce.FileManagement;
using ECommerce.Models;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL
{
    public class UpdateJsonFile : DataBaseHandler
    {
        public static void InstantiateJsonFileFromSqlDb(List<Product> ListOfProducts)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Storage of password in variable was successful...");
            Console.ResetColor();
            Thread.Sleep(500);

            //Attempt to connect console application to server database

            DataBaseHandler handler = new DataBaseHandler();

            //variable declaration
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source={dataSource};Initial Catalog={dataBase};User ID={handler.username};Password={pwd}";

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
            string sql, Output = "";
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
    }
}
