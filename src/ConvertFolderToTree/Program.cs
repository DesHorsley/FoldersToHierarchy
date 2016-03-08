using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConvertFolderToTree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            StreamReader file = new System.IO.StreamReader(new FileStream(@"C:\Users\desHo\Source\Repos\ConvertFolderToTree\src\ConvertFolderToTree\test1.txt", FileMode.Open));
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
                counter++;
            }

            file.Close();

            // Suspend the screen.
            Console.ReadLine();
        }
    }
}
