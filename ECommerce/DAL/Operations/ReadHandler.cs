using ECommerce.DAL;
using ECommerce.Database;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ECommerce.DAL.Operations
{
    class ReadHandler : DataBaseHandler
    {
        public static void ReadSqlDb()
        {
            // Assuming SqlVariables.SetSqlVariables initializes and sets up the SQL connection (cnn) and command (command)
            SqlVariables.SetSqlVariables(out var adapter, out var sql, out var cnn);

            // Open the connection
            cnn.Open();

            // Prepare the SQL command
            SqlCommand command = new SqlCommand("Select Identify, Id, NameOfProduct, Description from dbo.Product", cnn);

            // Execute the command and process results
            using (var dataReader = command.ExecuteReader())
            {
                var output = new StringBuilder();

                while (dataReader.Read())
                {
                    output.AppendLine($"{dataReader.GetValue(0)} - {dataReader.GetValue(1)} - {dataReader.GetValue(2)} - {dataReader.GetValue(3)}");
                }

                // Display results
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("SQL database:");
                Console.ResetColor();
                Console.WriteLine(output.ToString());
            }

            // Close the connection
            cnn.Close();
        }
    }
}


