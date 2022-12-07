
Part1();

Part2();

Console.ReadKey();

void Part1()
{
    using (var stream = new StreamReader("input.txt"))
    {
        Console.WriteLine($"Part 1: {FindStartPacket(stream, 4)}");
    }
}

void Part2()
{
    using (var stream = new StreamReader("input.txt"))
    {
        Console.WriteLine($"Part 2: {FindStartPacket(stream, 14)}");
    }
}

int FindStartPacket(StreamReader input, int messageSize)
{
    var charactersProcessed = 0;
    Queue<char> streamQueue = new Queue<char>();

    while(!input.EndOfStream)
    {
        if (streamQueue.Count == messageSize)
            break;

        var character = (char)input.Read();
        while (streamQueue.Contains(character))
        {
            streamQueue.Dequeue();
        }

        streamQueue.Enqueue(character);
        charactersProcessed++;
    }

    return charactersProcessed;
}