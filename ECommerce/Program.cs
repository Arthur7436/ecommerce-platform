using ECommerce.Models;
using ECommerce.Repository;

namespace ECommercePlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> ListOfProducts = new List<Product>(); //create a list to store all products inside
            ProductRepository.ConnectToSqlDb(); //connect program to database

            //create folder if doesn't exist
            string path = @"c:\FileStorage";

            try
            {
                if (Directory.Exists(path)) //if directory exists then return
                {
                    return;
                }

                //create directory
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine($"Directory was successfully created at {Directory.GetCreationTime(path)}");

                //create file in directory


            } catch (Exception e) {
                Console.WriteLine($"The process failed: {e.ToString}"); //give error message 
                
            }
            finally { }

            //create a file in directory if it doesn't exist
            string filePath = @"c:\FileStorage\Test.json";

            try
            {
                if (!Directory.Exists(filePath))
                {
                    return;
                }

                using (FileStream fs = File.Create(filePath)); 
            }
            catch (Exception e) {
                Console.WriteLine($"The process failed: {e.ToString}");
            }
            finally { }

            do
            {
                ProductRepository.InstantiateJsonFileFromSqlDb(ListOfProducts); //json file is to reflect sql db at all times

                ListOfProducts = ProductRepository.DeserializeJsonFileToList(); //allows product stored in file as memory upon start up

                ProductRepository.MakeIdentifyColumnNumberingUpToDate(); //Makes SQL db column for "Identify" in chronological numerical sequence 

                ProductRepository.DisplayMenu();//Display the menu to user

                string? input = Console.ReadLine(); //store the users input into variable to determine program flow

                //FIND A WAY TO MAKE SWITCH FOR THE IF-ELSE PROGRAM FLOW AT THE BOTTOM

                if (input == "q") //quit the program
                {
                    ProductRepository.TurnOffConnectionToDb(); //close the connection of db when they click q
                    return; //close the program
                }
                else if (input == "r") //reset program memory
                {
                    ProductRepository.ClearProductList(ListOfProducts); //reset the list and json file
                }
                else if (input == "1") //view all products available
                {
                    ProductRepository.ViewProduct(ListOfProducts); //views what is in list & JSON file

                    ProductRepository.ViewSqlDb(); //views what is in db

                    Thread.Sleep(500);
                }
                else if (input == "2") //add the product requested by user via the console application
                {
                    ProductRepository.AddProductToListAndSqlDb(ListOfProducts!); //Add product to JSON file and SQL db
                    ProductRepository.SerializeToJsonFile(ListOfProducts); //Serialize the updated list to the JSON file
                }
                else if (input == "3") //remove the product requested by user
                {
                    ProductRepository.RemoveProduct(ListOfProducts!);//removes the requested product
                    ProductRepository.SerializeToJsonFile(ListOfProducts);//Serialize the updated list to the JSON file
                }
                else if (input == "4") //update the product requested by user
                {
                    ProductRepository.UpdateProduct(ListOfProducts!);//Updates the products name or description in both JSON file and SQL db
                }
            } while (true);
        }
    }
}
