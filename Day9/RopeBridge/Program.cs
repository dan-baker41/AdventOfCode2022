
using RopeBridge;

Part1();

Part2();

Console.ReadKey();

void Part1()
{
    var rope = new LongRope(2);

    using (var stream = new StreamReader("input.txt"))
    {
        while(!stream.EndOfStream)
        {
            var line = stream.ReadLine();

            rope.Move(line!);
        }
    }

    Console.WriteLine($"Part 1: {rope.PositionsVisitedByTail()}");
}

void Part2()
{
    var rope = new LongRope(10);

    using (var stream = new StreamReader("input.txt"))
    {
        while (!stream.EndOfStream)
        {
            var line = stream.ReadLine();

            rope.Move(line!);
        }
    }

    Console.WriteLine($"Part 2: {rope.PositionsVisitedByTail()}");
}