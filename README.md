# FoldersToHierarchy
Takes a windows folder structure and produces a json file with a parent/child hierarchy.

The console command will ignore files, so the resulting hierarchy will only contain the folder.

The process requires some manual intervention outlined below:

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
