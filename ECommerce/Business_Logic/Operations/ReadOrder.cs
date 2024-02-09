using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business_Logic.Operations
{
    public static class ReadOrder
    {
        public static void ReadAllOrders(List<Order> ListOfOrders)
        {
            //Console.WriteLine(ListOfOrders.ToString());

            for (int i = 0; i < ListOfOrders.Count; i++)
            {
                Console.WriteLine(ListOfOrders[i].ToString());
            }
        }
    }
}
