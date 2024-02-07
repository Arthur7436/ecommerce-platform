using ECommerce.Business_Logic;
using ECommerce.Business_Logic.Operations;
using ECommerce.DAL;
using ECommerce.DAL.Operations;
using ECommerce.Database;
using ECommerce.FileManagement;
using ECommerce.Models;
using ECommerce.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ECommercePlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> ListOfProducts = new List<Product>(); //create a list to store all products inside
            List<Order> ListOfOrders = new List<Order>();
            ConnectToSqlDB.ConnectToSqlDb(); //connect program to databasee
            CheckForFileAndDirectory.CheckForDirectory(); //if there is no directory, then it is created
            CheckForFileAndDirectory.CheckForFile(); //if there is no file, then it is created

            UpdateJsonFile.InstantiateJsonFileFromSqlDb(ListOfProducts); //json file is to reflect sql db at all times
            //new text
            do //continue the program until user wants to quit
            {

                ListOfProducts = ProductFileManager.DeserializeJsonFileToList(); //allows product stored in file as memory upon start up

                //order sql db identify column to be chronological order, if this is gone identify column is incorrect
                NumberingColumn.MakeIdentifyColumnNumberingUpToDate();

                Display.DisplayMenu();//Display the menu to user

                string? input = Console.ReadLine(); //store the users input into variable to determine program flow 

                switch (input)
                {
                    case "q": //quit the program

                        Environment.Exit(0);
                        return; //close the program

                    case "r": //reset program memory and remove all data

                        ClearList.ClearAllList(ListOfProducts); //clears the list
                        ClearFile.ClearAllFiles(ListOfProducts); //clears the file
                        ListOfProducts = ProductFileManager.DeserializeJsonFileToList();

                        DeleteAllSqlData.DeleteAllFromSqlDb(); //clears the sql db

                        break;
                    case "1": //view all products available
                        Read.ViewProduct(ListOfProducts); //views what is in list & JSON file

                        ReadHandler.ReadSqlDb(); //views what is in db
                        Console.ReadLine();
                        break;
                    case "2": //add the product requested by user via the console application

                        Create.AddToList(ListOfProducts!); //Add product to JSON file 
                        CreateHandler.AddToSqlDb(ListOfProducts); //Add to SQL db
                        ProductFileManager.SerializeToJsonFile(ListOfProducts); //Serialize the updated list to the JSON file
                        Create.CreateOrder(ListOfOrders, ListOfProducts);
                        break;
                    case "3": //remove the product requested by user
                        Delete.DeleteFromList(ListOfProducts!);//removes product from list
                        DeleteHandler.DeleteFromSqlDb(ListOfProducts); //removes product form sql db
                        ProductFileManager.SerializeToJsonFile(ListOfProducts);//Serialize the updated list to the JSON file
                        break;
                    case "4": //update the product requested by user
                        Update.UpdateProductInList(ListOfProducts!);//Updates the products name or description in both JSON file and SQL db
                        //UpdateHandler.UpdateProductInSqlDb();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (true);
        }
    }
}
