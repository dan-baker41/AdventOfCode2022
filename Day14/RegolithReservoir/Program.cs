
Part1();

Part2();

void Part1()
{
    FillCaveWithRocks("input.txt", out Dictionary<(int, int), char> caveCoordinates, out int maxY);

    const int entryX = 500;

    var deltaX = 0;
    var deltaY = 0;
    var particleCounter = 0;
    while(deltaY <= maxY)
    {
        // check for a collision at the next location
        if(caveCoordinates.ContainsKey((entryX + deltaX, deltaY + 1)))
        {
            // first, try to move down to the left
            if (!caveCoordinates.ContainsKey((entryX + deltaX - 1, deltaY + 1)))
            {
                // move down and to the left
                deltaX -= 1;
                deltaY += 1;
            }
            // next, try to move down and to the right
            else if(!caveCoordinates.ContainsKey((entryX + deltaX + 1, deltaY + 1)))
            {
                // move down and to the right
                deltaX += 1;
                deltaY += 1;
            }
            // otherwise, the sand particle goes here
            else
            {
                caveCoordinates[((entryX + deltaX, deltaY))] = 'O';
                deltaX = 0;
                deltaY = 0;
                particleCounter++;
            }
        }
        else
        {
            deltaY++;
        }
    }

    Console.WriteLine($"Part 1: {particleCounter}");
}

void Part2()
{
    FillCaveWithRocks("input.txt", out Dictionary<(int, int), char> caveCoordinates, out int maxY);

    const int entryX = 500;
    int floor = maxY + 2;

    var deltaX = 0;
    var deltaY = 0;
    var particleCounter = 0;
    while (!caveCoordinates.ContainsKey((500, 0)))
    {
        // if the floor is reached, the particle goes here
        if(deltaY + 1 == floor)
        {
            caveCoordinates[((entryX + deltaX, deltaY))] = 'O';
            deltaX = 0;
            deltaY = 0;
            particleCounter++;
        }
        // check for a collision at the next location, or if the next location is the floor at maxY + 2
        else if (caveCoordinates.ContainsKey((entryX + deltaX, deltaY + 1)))
        {
            // first, try to move down to the left
            if (!caveCoordinates.ContainsKey((entryX + deltaX - 1, deltaY + 1)))
            {
                // move down and to the left
                deltaX -= 1;
                deltaY += 1;
            }
            // next, try to move down and to the right
            else if (!caveCoordinates.ContainsKey((entryX + deltaX + 1, deltaY + 1)))
            {
                // move down and to the right
                deltaX += 1;
                deltaY += 1;
            }
            // otherwise, the sand particle goes here
            else
            {
                caveCoordinates[((entryX + deltaX, deltaY))] = 'O';
                deltaX = 0;
                deltaY = 0;
                particleCounter++;
            }
        }
        else
        {
            deltaY++;
        }
    }

    Console.WriteLine($"Part 2: {particleCounter}");
}

Dictionary<(int, int), char> FillCaveWithRocks(string inputFile, out Dictionary<(int, int), char> caveCoordinates, out int maxY)
{
    caveCoordinates = new Dictionary<(int, int), char>();
    maxY = 0;

    // fill the cave coordinates with rocks using the input file
    using (var stream = new StreamReader(inputFile))
    {
        while (!stream.EndOfStream)
        {
            var line = stream.ReadLine()!;

            var coordinateSplit = line.Split(" -> ");
            if(coordinateSplit.Length == 0) 
                throw new ArgumentException("Line is not formatted properly");

            // calculate the difference between each coordinate
            ParseCoordinate(coordinateSplit[0], out int currentX, out int currentY);

            // check if this is a new maximum Y value
            if (currentY > maxY)
                maxY = currentY;

            for(var i = 1; i < coordinateSplit.Length; i++)
            {
                ParseCoordinate(coordinateSplit[i], out int nextX, out int nextY);
                // check if there is a new maximum Y value
                if(nextY > maxY)
                    maxY = nextY;

                // if the X coordinates are not equal, the line moves left-right
                if(currentX != nextX)
                {
                    var delta = nextX - currentX;
                    var diff = Math.Abs(delta);
                    var multiplier = delta / diff;
                    for(var j = 0; j <= diff; j++)
                    {
                        caveCoordinates[(currentX + (j * multiplier), currentY)] = '#';
                    }
                }
                // if the Y coordinates are not equal, the line moves up-down
                else if(currentY != nextY) 
                {
                    var delta = nextY - currentY;
                    var diff = Math.Abs(delta);
                    var multiplier = delta / diff;
                    for (var j = 0; j <= diff; j++)
                    {
                        caveCoordinates[(currentX, currentY + (j * multiplier))] = '#';
                    }
                }

                // set the next x and y as current for the next iteration
                currentX = nextX;
                currentY = nextY;
            }
        }
    }

    return caveCoordinates;
}

void ParseCoordinate(string pair, out int x, out int y)
{
    var split = pair.Split(',');
    if (split.Length != 2)
        throw new ArgumentException("Coordinate cannot be initialized");

    if (!int.TryParse(split[0], out x))
    {
        throw new ArgumentException("X Coordinate cannot be initialized");
    }
    if (!int.TryParse(split[1], out y))
    {
        throw new ArgumentException("Y Coordinate cannot be initialized");
    }
}