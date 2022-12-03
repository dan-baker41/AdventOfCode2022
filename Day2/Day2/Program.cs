// call Part1 to solve the first puzzle
Part1();

// call Part2 to solve the second puzzle
Part2();

Console.Read();

/**
 * Part1: Calculate the score if the strategy guide is followed
 */
void Part1()
{
    using (var stream = new StreamReader("input.txt"))
    {
        var score = 0;
        while(!stream.EndOfStream)
        {
            // read the next round of rock paper scissors
            var inputLine = stream.ReadLine();

            // verify that the guide has three characters (opponent's move and suggested move, separated by a space
            if(inputLine == null || inputLine.Length != 3)
            {
                Console.WriteLine("Error in Strategy Guide input format");
                return;
            }
            else
            {
                // convert the moves to a value, 1 - 3, by subtracting the minimum ASCII value and adding 1
                // this value can be used to determine the winner, and also the point value each move adds to the score
                var opponentMove = inputLine.ElementAt(0) - 'A' + 1;  // opponent moves start at 'A'
                var suggestedMove = inputLine.ElementAt(2) - 'X' + 1; // suggested moves start at 'X'

                // add the point value of the suggested move
                score += suggestedMove;

                // if the suggested move is 1 greater than the opponent's move, that is a victory.
                // mod the sum by 3 so the value wraps
                if(suggestedMove == (opponentMove % 3) + 1)
                {
                    score += 6; // 6 points for victory
                }
                // if the moves are equal, it's a draw
                else if(suggestedMove == opponentMove)
                {
                    score += 3; // 3 points for draw
                }
            }
        }

        Console.WriteLine($"Part 1: {score}");
    }
}

void Part2()
{

}