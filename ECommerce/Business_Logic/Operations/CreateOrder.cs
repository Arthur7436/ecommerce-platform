using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business_Logic.Operations
{
    public class CreateOrder
    {
        public static void CreateOrderAndAddToList(List<Order> ListOfOrders)
        {
            Order order = new Order();

            Random rand = new Random();
            order.OrderId = rand.Next(1, 10000).ToString("D4"); //generate a random 4 digit number for this order

            order.OrderDate = DateTime.Now; //mark the date of creating the order

            ListOfOrders.Add(order); //add the new order to the list


        }
    }
}
