using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL
{
    public class SqlVariables : DataBaseHandler
    {
        public static void SetSqlVariables(out SqlDataAdapter adapter, out string sql, out SqlConnection cnn)
        {

            DataBaseHandler handler = new DataBaseHandler();

            //set sql variables
            SqlCommand command;
            adapter = new SqlDataAdapter();
            sql = "";
            string connectionString = null!;
            connectionString = $"Data Source={dataSource};Initial Catalog={dataBase};User ID={handler.username};Password={pwd}";
            cnn = new SqlConnection(connectionString);
        }
    }
}
