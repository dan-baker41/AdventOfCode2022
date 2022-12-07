
Part1();

Part2();

Console.ReadKey();

void Part1()
{
    using (var stream = new StreamReader("input.txt"))
    {
        Console.WriteLine($"Part 1: {FindStartPacket(stream)}");
    }
}

void Part2()
{

}

int FindStartPacket(StreamReader input)
{
    var charactersProcessed = 0;
    Queue<char> streamQueue = new Queue<char>();

    while(!input.EndOfStream)
    {
        if (streamQueue.Count == 4)
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