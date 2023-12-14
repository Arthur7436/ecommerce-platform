using ECommerce.DAL;
using ECommerce.Database;
using ECommerce.GlobalVariables;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Business_Logic.Operations
{
    public class Create : Main
    {
        public static void AddToList(List<Product> ListOfProducts)
        {
            //Create instance and add details to the instance which will be added to the list
            Product product = new Product();

            string GenerateRandomID = Guid.NewGuid().ToString("N");

            Console.WriteLine($"Product Id: {GenerateRandomID}");
            product.Id = GenerateRandomID;

            Console.WriteLine("Name of product: ");
            string? productNameInput = Console.ReadLine();

            //if the name of product already exists, then notify user
            for (int i = 0; i < ListOfProducts.Count; i++)
            {
                if (ListOfProducts[i].NameOfProduct == productNameInput)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This product name already exists!");
                    Console.ResetColor();

                    Console.ReadLine();
                    return;
                }
            }

            product.NameOfProduct = productNameInput;

            Console.WriteLine("Description of product: ");
            string? DescriptionOfProductInput = Console.ReadLine();
            product.Description = DescriptionOfProductInput!;

            ListOfProducts!.Add(product);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product added!");
            Console.ResetColor();
        }

        // New method to create an order
        public static void CreateOrder(List<Order> ListOfOrders, List<Product> ListOfProducts)
        {
            Order newOrder = new Order
            {
                OrderId = Guid.NewGuid().ToString("N"),
                OrderDate = DateTime.Now
            };

            Console.WriteLine("Creating a new order. Please add products to this order by entering their name.");

            while (true)
            {
                Console.WriteLine("Enter product ID to add (or 'done' to finish):");
                string input = Console.ReadLine();

                if (input.ToLower() == "done")
                {
                    break;
                }

                Product productToAdd = ListOfProducts.Find(p => p.NameOfProduct == input);
                if (productToAdd != null)
                {
                    newOrder.Products.Add(productToAdd);
                    Console.WriteLine($"Product {productToAdd.NameOfProduct} added.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }

            if (newOrder.Products.Count > 0)
            {
                ListOfOrders.Add(newOrder);
                Console.WriteLine($"Order {newOrder.OrderId} created with {newOrder.Products.Count} products.");
            }
            else
            {
                Console.WriteLine("No products added. Order not created.");
            }
        }
    }
}



