
using CathodeRayTube;

Part1();

Part2();

Console.ReadKey();

void Part1()
{
    var device = new ComsDevice();
    using (var stream = new StreamReader("input.txt"))
    {
        while (!stream.EndOfStream)
        {
            var line = stream.ReadLine();

            var parse = line!.Split(' ');

            
            switch (parse[0])
            {
                case "noop":
                    device.Noop();
                    break;
                case "addx":
                    if (parse.Length < 2)
                        throw new Exception("addx needs a number parameter");
                    if (!int.TryParse(parse[1], out int val))
                        throw new Exception($"{parse[1]} is not a number");
                    device.AddX(val);
                    break;
                default:
                    throw new Exception($"{parse[0]} is not a valid instruction");
            }

            // stop after 220
            if (device.ClockCycle >= 220)
                break;
        }

        Console.WriteLine($"Part 1: {device.SignalStrengthSum()}");
    }
}

void Part2()
{

}