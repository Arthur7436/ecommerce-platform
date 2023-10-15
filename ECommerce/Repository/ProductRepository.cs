using ECommerce.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;


namespace ECommerce.Repository
{
    public class ProductRepository
    {
        public static SqlCommand? command;
        public static SqlDataAdapter? adapter;
        public static string? sql;
        public static SqlConnection? cnn;

        public static void ProgramShutDown()
        {
        }

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

                            ProductDataBaseHandler.CloseSqlConnection() ;

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

        public static void ClearProductList(List<Product> ListOfProducts)
        {
            //remove everything in the list
            ListOfProducts.Clear();
            //push those changes and serialize as Json 
            string json = JsonConvert.SerializeObject(ListOfProducts);
            File.WriteAllText(@"C:\FileStorage\Test.json", json);
        }

        public static void ViewProduct(List<Product> ListOfProducts) //ListOfProducts <List> is already deserialized into a list from the file
        {
            if (ListOfProducts == null || ListOfProducts.Count == 0) //give error message if list is empty
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No products to view!");
                Console.ResetColor();

                Console.ReadLine();
            }
            else //list all the products
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Here is the list of all products:");
                Console.ResetColor();

                //display all objects within List
                foreach (Product products in ListOfProducts)
                {
                    Console.WriteLine(products.ToString());
                }

                Console.WriteLine();
            }
        }

        public static void RemoveProduct(List<Product> ListOfProducts)
        {
            ProductDataBaseHandler.SetSqlVariables(out adapter, out sql, out cnn);
            cnn.Open();

            //collect info from user
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Which product would you like to delete?");
            Console.ResetColor();

            foreach (Product products in ListOfProducts!) //print out all the products available
            {
                Console.WriteLine(products.ToString());
            }

            ProductDataBaseHandler.ViewSqlDb();

            Console.Write("Input: ");
            string userRemovalInput = Console.ReadLine()!;

            //remove based on name of product
            if (!ListOfProducts.Any(x => x.NameOfProduct == userRemovalInput)) //if users inputs a product that already exists => give error
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The product doesn't exist!");
                Console.ResetColor();

                Console.ReadLine();

                ProductDataBaseHandler.CloseSqlConnection();
                return;
            }

            if (ListOfProducts.Any(x => x.NameOfProduct == userRemovalInput)) //else remove the product from the list
            {
                //loop through the whole list 
                for (int i = 0; i < ListOfProducts.Count; i++)
                {
                    //if the name of product is equal to the userRemovalInput, find the index of that object
                    if (ListOfProducts[i].NameOfProduct == userRemovalInput)
                    {
                        //remove from list
                        ListOfProducts.RemoveAt(i);

                        //remove product also from sql db
                        sql = $"Delete dbo.Product where Identify={i + 1}";

                        command = new SqlCommand(sql, cnn);
                        //cnn.Open();
                        adapter.DeleteCommand = new SqlCommand(sql, cnn);
                        adapter.DeleteCommand.ExecuteNonQuery();

                        ProductDataBaseHandler.CloseSqlConnection ();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Product successfully removed!");
                        Console.ResetColor();

                        Console.ReadLine();
                    }
                }
            }
        }

        public static void AddProductToListAndSqlDb(List<Product> ListOfProducts)
        {
            ProductDataBaseHandler.SetSqlVariables(out adapter, out sql, out cnn);

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

            //Loop through the list and find the row number + Id + NameOfProduct + Description in order to be sent to sql db
            for (int i = 0; i < ListOfProducts.Count; i++)
            {
                sql = $"Insert into dbo.Product (Identify,Id,NameOfProduct,Description) values({i + 1},'" + $"{product.Id}" + "', '" + $"{product.NameOfProduct}" + "' , '" + $"{product.Description}" + "')";
            }

            //push the product into sql db
            command = new SqlCommand(sql, cnn);
            adapter.InsertCommand = new SqlCommand(sql, cnn);

            cnn.Open();
            adapter.InsertCommand.ExecuteNonQuery();

            ProductDataBaseHandler.CloseSqlConnection();

            Console.ReadLine();
        }

        public static void DisplayMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the E-commerce platform!");
            Console.ResetColor();
            Console.WriteLine("Please select one of the following: ");

            List<string> displayMenu = new List<string>()
                {
                    "1. View all products",
                    "2. Add a product",
                    "3. Remove a product",
                    "4. Update a product",
                    "Enter 'r' to reset the list to null",
                    "Enter 'q' to exit the program"
                };


            for (int i = 0; i < displayMenu.Count; i++)
            {
                Console.WriteLine(displayMenu[i]); //display menu
            }
        }
    }
}
