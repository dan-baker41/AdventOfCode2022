using CampCleanup;

Part1();

Part2();

Console.ReadKey();

void Part1()
{
    using (var stream = new StreamReader("input.txt"))
    {
        // initialize a counter
        int numberFullyOverlapped = 0;

        while(!stream.EndOfStream)
        {
            var line = stream.ReadLine();

            if(line == null)
            {
                Console.WriteLine("Input error");
            }
            else
            {
                // split the input to get the range assigned to each elf
                var assignments = line.Split(',');

                if(assignments.Length != 2)
                {
                    Console.WriteLine("Input not formatted properly");
                }
                else
                {
                    // create the section assignment objects
                    var assignment1 = SectionAssignmentFactory.Create(assignments[0]);
                    var assignment2 = SectionAssignmentFactory.Create(assignments[1]);

                    // Check if either assignment fully overlaps the other
                    if (assignment1.DoesFullyContain(assignment2) || assignment2.DoesFullyContain(assignment1))
                        numberFullyOverlapped++;
                }

            }
        }

        Console.WriteLine($"Part1: {numberFullyOverlapped}");
    }
}

void Part2()
{

}