
using HillClimbing;

Part1();

Part2();

Console.ReadKey();

void Part1()
{
    var heightMap = GenerateHeightMap(out int StartX, out int StartY, out int EndX, out int EndY);

    Stack<(int, int)> nodes = new Stack<(int, int)>();
    nodes.Push(( StartY, StartX ));

    while(nodes.Count != 0)
    {
        var location = nodes.Pop();
        int x = location.Item2;
        int y = location.Item1;
        var currentHeight = heightMap[y][x];        

        // check up, but not if this node is on the top border
        if(y > 0)
        {
            var destination = heightMap[y - 1][x];
            if (CompareNodes(currentHeight, destination))
            {
                destination.StepsToReach = currentHeight.StepsToReach + 1;
                if (destination.Height != 'E')
                    nodes.Push((y - 1, x));
            }
        }

        // check down, but not if this node is on the bottom border
        if (y < heightMap.Count - 1)
        {
            var destination = heightMap[y + 1][x];
            if (CompareNodes(currentHeight, destination))
            {
                destination.StepsToReach = currentHeight.StepsToReach + 1;
                if (destination.Height != 'E')
                    nodes.Push((y + 1, x));
            }
        }

        // check left, but not if this node is on the left border
        if(x > 0)
        {
            var destination = heightMap[y][x - 1];
            if (CompareNodes(currentHeight, destination))
            {
                destination.StepsToReach = currentHeight.StepsToReach + 1;
                if (destination.Height != 'E')
                    nodes.Push((y, x - 1));
            }
        }

        // check right
        if (x < heightMap[y].Count - 1)
        {
            var destination = heightMap[y][x + 1];
            if(CompareNodes(currentHeight, destination))
            {
                destination.StepsToReach = currentHeight.StepsToReach + 1;
                if (destination.Height != 'E')
                    nodes.Push((y, x + 1));
            }
        }
    }

    Console.WriteLine($"Steps to reach destination: {heightMap[EndY][EndX].StepsToReach}");
}

void Part2()
{
    var heightMap = GenerateHeightMap(out int StartX, out int StartY, out int EndX, out int EndY);

    Stack<(int, int)> nodes = new Stack<(int, int)>();
    nodes.Push((StartY, StartX));

    while (nodes.Count != 0)
    {
        var location = nodes.Pop();
        int x = location.Item2;
        int y = location.Item1;
        var currentHeight = heightMap[y][x];

        // check up, but not if this node is on the top border
        if (y > 0)
        {
            var destination = heightMap[y - 1][x];
            if (CompareNodes(currentHeight, destination))
            {
                if (destination.Height == 'a')
                    destination.StepsToReach = 0;
                else
                    destination.StepsToReach = currentHeight.StepsToReach + 1;
                if (destination.Height != 'E')
                    nodes.Push((y - 1, x));
            }
        }

        // check down, but not if this node is on the bottom border
        if (y < heightMap.Count - 1)
        {
            var destination = heightMap[y + 1][x];
            if (CompareNodes(currentHeight, destination))
            {
                if (destination.Height == 'a')
                    destination.StepsToReach = 0;
                else
                    destination.StepsToReach = currentHeight.StepsToReach + 1;
                if (destination.Height != 'E')
                    nodes.Push((y + 1, x));
            }
        }

        // check left, but not if this node is on the left border
        if (x > 0)
        {
            var destination = heightMap[y][x - 1];
            if (CompareNodes(currentHeight, destination))
            {
                if (destination.Height == 'a')
                    destination.StepsToReach = 0;
                else
                    destination.StepsToReach = currentHeight.StepsToReach + 1;
                if (destination.Height != 'E')
                    nodes.Push((y, x - 1));
            }
        }

        // check right
        if (x < heightMap[y].Count - 1)
        {
            var destination = heightMap[y][x + 1];
            if (CompareNodes(currentHeight, destination))
            {
                if (destination.Height == 'a')
                    destination.StepsToReach = 0;
                else
                    destination.StepsToReach = currentHeight.StepsToReach + 1;

                if (destination.Height != 'E')
                    nodes.Push((y, x + 1));
            }
        }
    }

    Console.WriteLine($"Steps from nearest starting point: {heightMap[EndY][EndX].StepsToReach}");
}

bool CompareNodes(GraphNode current, GraphNode destination)
{
    // keep track of the actual height values ('S' == 'a' and 'E' == 'z')
    var actualDestHeight = destination.Height;
    if (actualDestHeight == 'S')
        actualDestHeight = 'a';
    else if (actualDestHeight == 'E')
        actualDestHeight = 'z';

    var actualCurrentHeight = current.Height;
    if (actualCurrentHeight == 'S')
        actualCurrentHeight = 'a';

    // check if the destination is reachable
    if (actualDestHeight <= actualCurrentHeight || actualDestHeight == actualCurrentHeight + 1)
    {
        // next, check if the steps to reach from the current location are less than the current route for the destination node
        return destination.StepsToReach > current.StepsToReach + 1;
    }
    else
        return false;
}

List<List<GraphNode>> GenerateHeightMap(out int StartX, out int StartY, out int EndX, out int EndY)
{
    StartX = 0;
    StartY = 0;
    EndX = 0;
    EndY = 0;
    List<List<GraphNode>> graphNodes = new List<List<GraphNode>>();

    using (var stream = new StreamReader("input.txt"))
    {
        int y = 0;
        while (!stream.EndOfStream)
        {
            var line = stream.ReadLine();

            var row = new List<GraphNode>();
            int x = 0;
            foreach (var c in line!)
            {
                int initialSteps = int.MaxValue;
                if(c == 'S')
                {
                    StartX = x;
                    StartY = y;
                    initialSteps = 0;
                }
                else if(c == 'E')
                {
                    EndX = x;
                    EndY = y;
                }
                // initialize node with height c and steps to reach = max int
                var node = new GraphNode()
                {
                    Height = c,
                    StepsToReach = initialSteps
                };

                row.Add(node);
                x++;
            }
            graphNodes.Add(row);
            y++;
        }
    }

    return graphNodes;
}