﻿using ECommerce.DAL;
using ECommerce.DAL.Operations;
using ECommerce.Database;
using ECommerce.FileManagement;
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
    public class Update : Main
    {
        public static void UpdateProductInList(List<Product> ListOfProducts)
        {

            //SqlVariables.SetSqlVariables(out adapter, out sql, out cnn);

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

                for (int j = 0; j < productDetails.Count; j++)
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
                            Console.WriteLine("Name updated!");

                            ////update in sql db
                            UpdateHandler.UpdateProductNameInSqlDb(newProductName, i);
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
                            Console.WriteLine("Description updated!");

                            //update in sql db
                            UpdateHandler.UpdateProductDescriptionInSqlDb(newProductDescription, i);
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
