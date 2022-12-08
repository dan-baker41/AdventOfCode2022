
using NoSpaceLeftOnDevice;

Part1();

Console.ReadKey();

void Part1()
{
    // create the top level directory
    var fileSystem = new FileSystem();

    // use the input file to file fill the filesystem
    using (var stream = new StreamReader("input.txt"))
    {

        while(!stream.EndOfStream)
        {
            var input = stream.ReadLine();
            if(input == null)
            {
                Console.WriteLine("Input stream error");
                break;
            }

            // user command
            if (input.StartsWith("$"))
            {
                // parse the command
                var split = input.Split(' ');
                if (split.Length < 2)
                {
                    Console.WriteLine($"{input} is missing command, invalid syntax");
                }
                else if (split[1] == "cd")
                {
                    if (split.Length != 3)
                    {
                        Console.WriteLine("cd needs to specify a directory");
                    }
                    else
                    {
                        fileSystem.ChangeDirectory(split[2]);
                    }
                }
            }
            else
            {
                // use this input line to generate a file in the current directory
                var split = input.Split(' ');
                if(split.Length < 2)
                {
                    Console.WriteLine($"{input} is not formatted properly to create a file");
                }
                else
                {
                    // if the first value is a number, the file type is "File"
                    if (int.TryParse(split[0], out int fileSize))
                    {
                        fileSystem.AddFile(FileType.File, split[1], fileSize);
                    }
                    else
                    {
                        fileSystem.AddFile(FileType.Directory, split[1], 0);
                    }
                }
            }
        }

        // after processing the input, display the result
        fileSystem.ShowDiskUsage();
        fileSystem.ShowDirectoryToDelete();
    }
}