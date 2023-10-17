using ECommerce.Models;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Business_Logic
{
    public class UpdateHandler : Main
    {
        public static void UpdateProduct(List<Product> ListOfProducts)
        {
            ProductDataBaseHandler.SetSqlVariables(out adapter, out sql, out cnn);

            //Ask the user
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Which product did you want to update: ");
            Console.ResetColor();

            foreach (Product products in ListOfProducts)
            {
                Console.WriteLine(products);
            }

            string? UserInput = Console.ReadLine();
            //loop through list and if the name of product equals userInput then ask which they want to update: NameOfProduct or Description
            List<string> productDetails = new List<string>();
            productDetails.Add("Name of Product");
            productDetails.Add("Description of product");

            if (ListOfProducts.Any(x => x.NameOfProduct == UserInput)) //if user selects a product via NameOfProduct
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Which property did you want to update for '{UserInput}':");
                Console.ResetColor();

                for (int j = 0; j < (productDetails.Count); j++)
                {
                    Console.WriteLine($"{j + 1}: {productDetails[j]}"); //display properties of Product that can be updated
                    //if we have reached to the last option of the list then break

                }

                int? numInput = int.Parse(Console.ReadLine());
                if (numInput == 1) //<-------HANDLE SYSTEM IF USER INPUTS NOT A NUMBER -------->
                {
                    Console.WriteLine("Enter new product name: ");
                    string? newProductName = Console.ReadLine();

                    //update the Object's property NameOfProduct
                    //go through whole list and find the object's index
                    for (int i = 0; i < ListOfProducts.Count; i++)
                    {
                        if (ListOfProducts[i].NameOfProduct == UserInput)
                        {
                            ListOfProducts[i].NameOfProduct = newProductName;
                            ProductFileManager.SerializeToJsonFile(ListOfProducts); //serialize to json file so that it would not be overwritten at the start of Main Program
                            //update in sql db
                            cnn.Open();
                            sql = "Update dbo.Product set NameOfProduct='" + $"{newProductName}" + $"' where Identify={i + 1}"; //Update the column NameOfProduct at the row of that product

                            command = new SqlCommand(sql, cnn);

                            adapter.UpdateCommand = new SqlCommand(sql, cnn);
                            Console.WriteLine(adapter.UpdateCommand.ExecuteNonQuery());
                            adapter.UpdateCommand.ExecuteNonQuery();

                            ProductDataBaseHandler.CloseSqlConnection();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Product name updated!");
                            Console.ResetColor();
                            Thread.Sleep(500);
                        }
                    }
                }
                else if (numInput == 2)
                {
                    Console.WriteLine("Enter new product description: ");
                    string? newProductDescription = Console.ReadLine();
                    //update the Object's property NameOfProduct
                    //go through whole list and find the object's index
                    for (int i = 0; i < ListOfProducts.Count; i++)
                    {
                        if (ListOfProducts[i].NameOfProduct == UserInput)
                        {
                            ListOfProducts[i].Description = newProductDescription;
                            ProductFileManager.SerializeToJsonFile(ListOfProducts); //serialize to json file so that it would not be overwritten

                            //update in sql db
                            cnn.Open();
                            sql = "Update dbo.Product set Description='" + $"{newProductDescription}" + $"' where Identify={i + 1}"; //Update the column Description at the row of that product

                            command = new SqlCommand(sql, cnn);

                            adapter.UpdateCommand = new SqlCommand(sql, cnn);
                            adapter.UpdateCommand.ExecuteNonQuery();

                            ProductDataBaseHandler.CloseSqlConnection();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Product description updated!");
                            Console.ResetColor();
                            Thread.Sleep(500);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Product doesn't exist");

            }
        }
    }
}
