
using SupplyStacks;

Part1();

void Part1()
{
    using (var stream = new StreamReader("input.txt"))
    {
        string? line = "";
        string stack = "";

        while(!stream.EndOfStream)
        {
            line = stream.ReadLine();

            if(line != null)
            {
                // exit the loop when an empty string is found, indicating the end of the stack structure input
                if (line == "")
                    break;
                else
                    stack += line + "\n";
            }
            else
            {
                Console.WriteLine("Input failed");
            }
        }

        // use the generated stack input string to initialize the container stack object
        var containers = new ContainerStack(stack);
        
        // iterate through the rest of the input to perform all actions on the container stack
        while(!stream.EndOfStream)
        {
            var action = stream.ReadLine();
            if(action != null)
            {
                var containerAction = new ContainerStackAction(action);
                containers.PerformAction(containerAction);
            }
            else
            {
                Console.WriteLine("Input read failed");
            }
        }

        // display the result
        Console.WriteLine(containers.TopItemFromEach());
    }
}