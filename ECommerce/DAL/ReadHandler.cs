using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Database
{
     class ReadHandler : DataBaseHandler
    {
        public static void ReadSqlDb()
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

            CloseSqlConnection.CloseSql();
        }
    }
}
