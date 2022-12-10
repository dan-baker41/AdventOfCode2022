
using TreetopTreeHouse;

var grid = FillGrid();

Part1(grid);

Part2(grid);

Console.ReadKey();

void Part1(List<List<Tree>> grid)
{
    int visibleCount = 0;
    // iterate the column from 1 to size - 1 because the borders are known to be visible
    for(var i = 1; i < grid.Count - 1; i++)
    {
        var row = grid[i];

        // iterate the row from 1 to size - 1, also because the borders are known to be visible
        for(var j = 1; j < row.Count - 1; j++)
        {
            // check upwards until a taller tree is found (making this on invisible from up),
            // or until a shorter visible tree is found
            var upCount = 1;
            while(i - upCount >= 0)
            {
                // check if the tree to the north is a taller tree or the same size
                if (grid[i - upCount][j].Size >= row[j].Size)
                {
                    break;
                }
                // check if the tree to the north is visible
                if (grid[i - upCount][j].VisibleUp)
                {
                    if (!row[j].IsVisible)
                        visibleCount++;

                    row[j].VisibleUp = true;
                    break;
                }
                upCount++;
            }

            // check to the left until a taller tree is found (making this one invisible from left),
            // or a shorter visible tree is found
            var leftCount = 1;
            while(j - leftCount >= 0)
            {
                // check if this is a taller tree or the same size
                if(row[j - leftCount].Size >= row[j].Size)
                {
                    break;
                }
                // check if this is a visible tree
                if (row[j - leftCount].VisibleLeft)
                {
                    // if this tree is not already visible, increment the visible count
                    if (!row[j].IsVisible)
                        visibleCount++;

                    row[j].VisibleLeft = true;
                    break;
                }
                leftCount++; // increment leftCount to continue checking left
            }

            // there's no need to check if it's visible from the south if the tree is already visible.
            if (!row[j].IsVisible)
            {
                var downCount = 1;
                row[j].VisibleDown = true; // assume the tree is visible from the south
                while (i + downCount < grid.Count)
                {
                    // if the tree to the right is bigger, then this tree is not visible from the right
                    if (grid[i + downCount][j].Size >= row[j].Size)
                    {
                        row[j].VisibleDown = false;
                        break;
                    }
                    downCount++;
                }

                // if the row is still visible from the right, increment visible count
                if (row[j].VisibleDown)
                    visibleCount++;
            }

            // there's no need to check if it's visible from the right if the tree is already visible.
            if (!row[j].IsVisible)
            {
                var rightCount = 1;
                row[j].VisibleRight = true; // assume the tree is visible from the right
                while (j + rightCount < row.Count)
                {
                    // if the tree to the right is bigger, then this tree is not visible from the right
                    if (row[j + rightCount].Size >= row[j].Size)
                    {
                        row[j].VisibleRight = false;
                        break;
                    }
                    rightCount++;
                }

                // if the row is still visible from the right, increment visible count
                if (row[j].VisibleRight)
                    visibleCount++;
            }
        }
    }

    visibleCount += grid.Count * 2; // add the right and left edges
    visibleCount += (grid[0].Count - 2) * 2; // add the remaining sections of the top and bottom edges

    Console.WriteLine($"Number of trees visible: {visibleCount}");
}

void Part2(List<List<Tree>> grid)
{
    int highestScenicScore = 0;
    // iterate through the grid, but ignore the edges because they will always have a score of 0
    for(var i = 1; i < grid.Count - 1; i++)
    {
        for(var j = 1; j < grid[i].Count - 1; j++)
        {
            // since the visibility has already been determined, the scenic
            // score for a direction can be determined by the index in each direction

            // check down
            if(grid[i][j].VisibleDown)
            {
                grid[i][j].ScenicDown = grid.Count - (i + 1);
            }
            else
            {
                // otherwise, increment the scenic down value until an equal or taller tree is found
                int counter = i + 1;
                while(counter < grid.Count)
                {
                    grid[i][j].ScenicDown++;

                    if (grid[counter][j].Size >= grid[i][j].Size)
                    {
                        break;
                    }
                    counter++;
                }
            }

            // check left
            if (grid[i][j].VisibleUp)
            {
                grid[i][j].ScenicUp = i;
            }
            else
            {
                // otherwise, increment the scenic up value until an equal or taller tree is found
                int counter = i - 1;
                while (counter >= 0)
                {
                    grid[i][j].ScenicUp++;

                    if (grid[counter][j].Size >= grid[i][j].Size)
                    {
                        break;
                    }
                    counter++;
                }
            }

            // check left
            if (grid[i][j].VisibleLeft)
            {
                grid[i][j].ScenicLeft = j;
            }
            else
            {
                // otherwise, increment the scenic left value until an equal or taller tree is found
                int counter = j - 1;
                while (counter >= 0)
                {
                    grid[i][j].ScenicLeft++;

                    if (grid[i][counter].Size >= grid[i][j].Size)
                    {
                        break;
                    }
                    counter++;
                }
            }

            // check right
            if (grid[i][j].VisibleRight)
            {
                grid[i][j].ScenicRight = grid[i].Count - (j + 1);
            }
            else
            {
                // otherwise, increment the scenic right value until an equal or taller tree is found
                int counter = j + 1;
                while (counter < grid[i].Count)
                {
                    grid[i][j].ScenicRight++;

                    if (grid[i][counter].Size >= grid[i][j].Size)
                    {
                        break;
                    }
                    counter++;
                }
            }

            // check if this score is higher
            if (grid[i][j].ScenicScore > highestScenicScore)
                highestScenicScore = grid[i][j].ScenicScore;
        }
    }

    Console.WriteLine($"Scenic Score: {highestScenicScore}");
}

List<List<Tree>> FillGrid()
{
    var grid = new List<List<Tree>>();

    // open the input file
    using (var stream = new StreamReader("input.txt"))
    {
        // keep a row counter
        int rowCount = 0;

        while(!stream.EndOfStream)
        {
            // get the next row of trees
            var line = stream.ReadLine();
            if(line == null)
            {
                throw new Exception("Input error");
            }

            var row = new List<Tree>();
            for (var i = 0; i < line.Length; i++)
            {
                if (int.TryParse(line[i].ToString(), out int size))
                {
                    // if the tree is on the border, initialize it to be visible from that border's direction
                    var tree = new Tree(size);
                    if (rowCount == 0)
                        tree.VisibleUp = true;
                    if (i == 0)
                        tree.VisibleLeft = true;
                    else if (i == line.Length - 1)
                        tree.VisibleRight = true;

                    if (stream.EndOfStream)
                        tree.VisibleDown = true;

                    row.Add(tree);
                }
                else
                {
                    throw new Exception("Invalid input value");
                }
            }

            // add tree row to the grid
            grid.Add(row);
            rowCount++;
        }
    }

    return grid;
}