using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business_Logic.Operations
{
    public class Read
    {
        public delegate void DisplayMessage(string message);
        public static DisplayMessage displayDelegate;

        static Read()
        {
            displayDelegate = DisplayText;
        }

        public static void DisplayText(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        
        public static void ViewProduct(List<Product> ListOfProducts) //ListOfProducts <List> is already deserialized into a list from the file
        {
            if (ListOfProducts == null || ListOfProducts.Count == 0) //give error message if list is empty
            {
                displayDelegate("No products to view!");
                Console.ReadLine();
            }
            else //list all the products
            {
                displayDelegate("Here is the list of all products:");

                //display all objects within List
                foreach (Product products in ListOfProducts)
                {
                    Console.WriteLine(products.ToString());
                }

            }
        }
    }
}
