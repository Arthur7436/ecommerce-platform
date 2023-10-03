using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.FileManagement
{
    public class CheckForFileAndDirectory
    {
        public static void CheckForDirectory()
        {
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e.ToString}"); //give error message 
            }
            finally { }
        }

        public static void CheckForFile()
        {
            //create a file in directory if it doesn't exist
            string filePath = @"c:\FileStorage\Test.json";
            try
            {
                if (Directory.Exists(filePath)) //id directory exists then return
                {
                    return;
                }
                using (FileStream fs = File.Create(filePath)) ; //create the file
            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e.ToString}");
            }
            finally { }
        }
    }
}
