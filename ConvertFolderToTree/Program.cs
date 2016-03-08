using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertFolderToTree1
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
            Step 1 - Navigate to the root folder to conver to a hierarchy
            Step 2 - Shift right click in the folder, "Open Command Window Here" 
            Step 3 - Run this command:
                dir /ad /s >output.txt
            Step 4 - take output.txt and open it in your favourite text editor then removed the useless info. (I used sublime with find and replace using regex)
                    Remove the Volume lines (1 & 2)
                    Remove the Directory line (4)
                    Remove all the lines for the current and previous folder ("03/03/2016  02:32 PM    <DIR>          .", "17/02/2016  10:28 AM    <DIR>          ..")
                    Remove the text on each line upto the first character of the folder. Eg:
                        01/03/2016  01:50 PM    <DIR>          4 - Outbye
                        becomes
                        4 - Outbye
                    Remove the file count lines ("               0 File(s)              0 bytes")
                    Remove the common Directory text up to the root directory.
            Step 5 - Save the file as a txt file in the bin of the project
            Step 6 - Run
            */

            int counter = 0;
            string line;
            List<Node> hierarchy = new List<Node>();
            Node currentParent = null;

            // Read the file and line by line.
            using (System.IO.StreamReader file =
               new System.IO.StreamReader("test1.txt")) // Path to txt file to convert. I placed mine in the bin folder to make things easy
            {
                while ((line = file.ReadLine()) != null)
                {
                    counter++;
                    if (line == "")
                    {
                        continue;
                    }

                    // Create the root
                    if (line == "1 - record") // 
                    {
                        currentParent = CreateRootNode(counter, line, hierarchy);
                        continue;
                    }

                    // New parent
                    if (line.StartsWith(@"1 - record\"))
                    {
                        currentParent = CreateParentNode(counter, line, hierarchy, currentParent);
                    }
                    else // Child of current parent
                    {
                        AddChildNode(counter, line, hierarchy, currentParent);
                    }
                }

                file.Close();
            }

            string hierarchyString = Newtonsoft.Json.JsonConvert.SerializeObject(hierarchy);

            string json =  Newtonsoft.Json.JsonConvert.SerializeObject(hierarchy, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(@"c:\temp\NewHierarchy.json", json); // Caution, will overwrite existing files

            Console.WriteLine("Hierarchy saved.");

            // Suspend the screen.
            Console.ReadLine();
        }

        private static void AddChildNode(int counter, string line, List<Node> hierarchy, Node currentParent)
        {
            hierarchy.Add(
                new Node
                {
                    Id = counter,
                    ParentId = currentParent.Id,
                    Name = line
                });
        }

        private static Node CreateParentNode(int counter, string line, List<Node> hierarchy, Node currentParent)
        {
            currentParent = new Node
            {
                Id = counter,
                ParentId = currentParent.Id,
                Name = line.Substring(line.LastIndexOf(@"\") + 1)
            };

            hierarchy.Add(currentParent);
            return currentParent;
        }

        private static Node CreateRootNode(int counter, string line, List<Node> hierarchy)
        {
            Node currentParent = new Node
            {
                Id = counter,
                ParentId = null,
                Name = line
            };
            hierarchy.Add(currentParent);
            return currentParent;
        }
    }

    public class Node
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
