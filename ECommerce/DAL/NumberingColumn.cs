using ECommerce.Database;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.DAL
{
    public class NumberingColumn : SqlVariables
    {
        //public static void MakeIdentifyColumnNumberingUpToDate()
        //{
        //    SetSqlVariables(out adapter, out sql, out cnn);

        //    cnn.Open();

        //    //Make Identify to be sequential numbering
        //    sql = "DECLARE @id varchar(255) SET @id = 0 UPDATE dbo.Product SET @id = Identify = @id + 1";

        //    command = new SqlCommand(sql, cnn);
        //    adapter.UpdateCommand = new SqlCommand(sql, cnn);
        //    adapter.UpdateCommand.ExecuteNonQuery();

        //    CloseSqlConnection.CloseSql();
        //}
    }
}
