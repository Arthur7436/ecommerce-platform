using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Database
{
    public class CloseSqlConnection : DataBaseHandler
    {
        public static void CloseSql()
        {
            command?.Dispose();
            cnn?.Close();
        }
    }
}
