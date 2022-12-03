// call Part1 to solve the first puzzle
Part1();

// call Part2 to solve the second puzzle
Part2();

Console.Read();

/**
 * Part1: Calculate the score if the strategy guide is followed
 * 
 * Opponent A - Rock
 *          B - Paper
 *          C - Scissors
 *          
 * Self     X - Rock
 *          Y - Paper
 *          Z - Scissors
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

            // verify that the guide has three characters (opponent's move and suggested move, separated by a space)
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

/**
 * Part1: Calculate the score if the strategy guide is followed
 * 
 * Opponent A - Rock
 *          B - Paper
 *          C - Scissors
 *          
 * Self     X - Lose
 *          Y - Draw
 *          Z - Win
 */
void Part2()
{
    using (var stream = new StreamReader("input.txt"))
    {
        var score = 0;
        while (!stream.EndOfStream)
        {
            // read the next round of rock paper scissors
            var inputLine = stream.ReadLine();

            // verify that the guide has three characters (opponent's move and suggested round outcome, separated by a space)
            if (inputLine == null || inputLine.Length != 3)
            {
                Console.WriteLine("Error in Strategy Guide input format");
                return;
            }
            else
            {
                // convert the opponent's moves to a value, 1 - 3, by subtracting the minimum ASCII value and adding 1
                var opponentMove = inputLine.ElementAt(0) - 'A' + 1;  // opponent moves start at 'A'

                // convert the suggested outcome to a value, 0 - 2, by subtracting the minimum ASCII value
                var suggestedOutcome = inputLine.ElementAt(2) - 'X'; // suggested outcomes start at 'X'

                // the point value earned by the suggested outcome can be calcualted by multiplying the outcome value by 3
                // (0 * 3) = 0 for a loss, (1 * 3) = 3 for a draw, (2 * 3) = 6 for a win
                score += suggestedOutcome * 3;

                // add the move score value based on the opponents score and the suggested outcome
                switch(suggestedOutcome)
                {
                    case 0: // lose game
                        if (opponentMove == 1)
                            score += 3; // value for scissors
                        else
                            score += opponentMove - 1;
                        break;
                    case 1: // draw
                        score += opponentMove;
                        break;
                    case 2: // win
                        if (opponentMove == 3)
                            score += 1; // value for rock
                        else
                            score += opponentMove + 1;
                        break;
                }
            }
        }

        Console.WriteLine($"Part 2: {score}");
    }
}