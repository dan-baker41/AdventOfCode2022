
using DistressSignal;

Part1();

Part2();

//Console.ReadKey();

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
                sum += i + 1;
            }
        }

        Console.WriteLine($"\nPart 1: {sum}");
    }
}

void Part2()
{
    using (var stream = new StreamReader("input.txt"))
    {
        List<SignalListItem> signals = new List<SignalListItem>();
        while (!stream.EndOfStream)
        {
            var signal = stream.ReadLine()!;

            // ignore blank lines
            if (signal != "")
            {
                signals.Add(new SignalListItem(signal));
            }
        }

        // add the divider packets
        var dividerPacket1 = new SignalListItem("[[2]]");
        var dividerPacket2 = new SignalListItem("[[6]]");
        signals.Add(dividerPacket1);
        signals.Add(dividerPacket2);

        for(int i = 0; i < signals.Count; i++)
        {
            var smallestIndex = i;
            for(var j = smallestIndex + 1; j < signals.Count; j++)
            {
                if (signals[smallestIndex].Compare(signals[j]) == false)
                    smallestIndex = j;
            }
            if(smallestIndex != i)
            {
                var temp = signals[i];
                signals[i] = signals[smallestIndex];
                signals[smallestIndex] = temp;
            }
        }

        var index1 = signals.IndexOf(dividerPacket1) + 1;
        var index2 = signals.IndexOf(dividerPacket2) + 1;

        Console.WriteLine($"\nPart 2: {index1 * index2}");
    }
}