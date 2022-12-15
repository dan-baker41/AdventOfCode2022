
using DistressSignal;

Part1();

Part2();

Console.ReadKey();

void Part1()
{
    using(var stream = new StreamReader("input.txt"))
    {
        List<SignalPair> pairs = new List<SignalPair>();
        while(!stream.EndOfStream)
        {
            var left = stream.ReadLine()!;
            var right = stream.ReadLine()!;

            // discard blank line
            stream.ReadLine();

            // create the signal pair
            pairs.Add(new SignalPair(left, right));
        }

        var sum = 0;
        for(var i = 0; i < pairs.Count; i++)
        {
            if (pairs[i].SignalInRightOrder() == true)
            {
                //Console.Write($"{i + 1} ");
                sum += i + 1;
            }
        }

        Console.WriteLine($"Plz work: {sum}");
    }
}

void Part2()
{

}