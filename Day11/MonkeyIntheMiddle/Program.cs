using MonkeyIntheMiddle;

Part1();

Part2();

Console.ReadKey();

void Part1()
{
    List<Monkey> monkeys = new List<Monkey>();

    using (var stream = new StreamReader("input.txt"))
    {
        while(!stream.EndOfStream)
        {
            // read 6 lines
            var monkeyInit = "";
            for(var i = 0; i < 6; i++)
                monkeyInit += stream.ReadLine() + "\n";

            // discard 1 line
            stream.ReadLine();

            // create the monkey and add it to the list
            monkeys.Add(new Monkey(monkeyInit));
        }

        // perform 20 rounds
        for(int i = 0; i < 20; i++)
        {
            // for each round, each monkey gets a turn
            foreach(var monkey in monkeys)
            {
                while(monkey.HasItems())
                {
                    monkey.PerformOperation(false, out int id, out UInt64 item);

                    // add this item to monkey specified by id
                    monkeys.ElementAt(id).GiveItem(item);
                }
            }
        }

        // find the top two monkeys
        PriorityQueue<int, int> mostInspections = new PriorityQueue<int, int>();
        foreach (var monkey in monkeys)
        {
            mostInspections.Enqueue(monkey.TimesItemsInspected, -monkey.TimesItemsInspected);
        }

        var highest = mostInspections.Dequeue();
        var second = mostInspections.Dequeue();

        Console.WriteLine($"Part 1: {highest * second}");
    }
}

void Part2()
{
    List<Monkey> monkeys = new List<Monkey>();

    using (var stream = new StreamReader("input.txt"))
    {
        while (!stream.EndOfStream)
        {
            // read 6 lines
            var monkeyInit = "";
            for (var i = 0; i < 6; i++)
                monkeyInit += stream.ReadLine() + "\n";

            // discard 1 line
            stream.ReadLine();

            // create the monkey and add it to the list
            monkeys.Add(new Monkey(monkeyInit));
        }

        // for each monkey, add references to the monkeys they pass to
        var lcm = LeastCommonMultiple(monkeys[0].TestValue, monkeys[1].TestValue);
        for(var i = 2; i < monkeys.Count; i++)
        {
            lcm = LeastCommonMultiple(lcm, monkeys[i].TestValue);
        }

        // perform 10000 rounds
        for (int i = 0; i < 10000; i++)
        {
            // for each round, each monkey gets a turn
            foreach (var monkey in monkeys)
            {
                monkey.LowestCommonDenominator = lcm;
                while (monkey.HasItems())
                {
                    monkey.PerformOperation(true, out int id, out UInt64 item);

                    // add this item to monkey specified by id
                    monkeys.ElementAt(id).GiveItem(item);
                }
            }
        }

        // find the top two monkeys
        PriorityQueue<int, int> mostInspections = new PriorityQueue<int, int>();
        foreach (var monkey in monkeys)
        {
            mostInspections.Enqueue(monkey.TimesItemsInspected, -monkey.TimesItemsInspected);
        }

        var highest = mostInspections.Dequeue();
        var second = mostInspections.Dequeue();

        Console.WriteLine($"Part 2: {(long)highest * (long)second}");
    }
}

int LeastCommonMultiple(int a, int b)
{
    int num1, num2;
    if (a > b)
    {
        num1 = a; num2 = b;
    }
    else
    {
        num1 = b; num2 = a;
    }

    for (int i = 1; i < num2; i++)
    {
        int mult = num1 * i;
        if (mult % num2 == 0)
        {
            return mult;
        }
    }
    return num1 * num2;
}