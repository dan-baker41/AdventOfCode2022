
using RucksackReorganization;

Part1();

Part2();

Console.ReadKey();

/**
 * Part 1:  Calculate the priority value sum of item that appears in
 *          both compartments of each rucksack
 */
void Part1()
{
    using (var stream = new StreamReader("input.txt"))
    {
        var prioritySum = 0;
        while(!stream.EndOfStream)
        {
            var rucksackContents = stream.ReadLine();
            if(rucksackContents != null)
            {
                var errorItem = Rucksack.FindCompartmentError(rucksackContents);
                prioritySum += Rucksack.PriorityValue(errorItem);
            }
        }
        Console.WriteLine($"Part 1: {prioritySum}");
    }
}

void Part2()
{

}