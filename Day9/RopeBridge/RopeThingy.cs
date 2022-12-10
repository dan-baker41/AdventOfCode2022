using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RopeBridge
{
    public class RopeThingy
    {
        private const int X = 0, Y = 1;
        private int[] headPosition;
        private int[] tailPosition;

        private HashSet<string> positionsVisited;

        public RopeThingy()
        {
            headPosition = new int[2] { 0, 0 };
            tailPosition = new int[2] { 0, 0 };
            positionsVisited = new HashSet<string>();
            positionsVisited.Add("0,0"); // tail visites 0,0 by default
        }

        public void Move(string movement)
        {
            // make sure the movement string is the correct length
            if (movement.Length < 3)
                throw new ArgumentException("Movement input value is invalid");

            // check the movement value
            switch(movement[0])
            {
                case 'R':
                    MoveRight(movement.Substring(2, movement.Length - 2));
                    break;
                case 'L':
                    MoveLeft(movement.Substring(2, movement.Length - 2));
                    break;
                case 'U':
                    MoveUp(movement.Substring(2, movement.Length - 2));
                    break;
                case 'D':
                    MoveDown(movement.Substring(2, movement.Length - 2));
                    break;
                default:
                    throw new ArgumentException("Movement direction is invalid");
            }
        }

        public int PositionsVisitedByTail()
        {
            return positionsVisited.Count;
        }

        private void MoveUp(string val)
        {
            if(!int.TryParse(val.ToString(), out int value))
            {
                throw new ArgumentException("Movement value is not a number");
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    // increment the y position of the head
                    headPosition[Y]++;

                    // if the tail is more than 1 y position below the head, move the tail directly behind the head
                    if (tailPosition[Y] <= headPosition[Y] - 2)
                    {
                        tailPosition[Y] = headPosition[Y] - 1;
                        tailPosition[X] = headPosition[X];

                        // add this position to the hash set
                        positionsVisited.Add($"{tailPosition[X]},{tailPosition[Y]}");
                    }
                }
            }
        }

        private void MoveDown(string val)
        {
            if (!int.TryParse(val.ToString(), out int value))
            {
                throw new ArgumentException("Movement value is not a number");
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    // decrement the y position of the head
                    headPosition[Y]--;

                    // if the tail is more than 1 y position above the head, move the tail directly behind the head
                    if (tailPosition[Y] >= headPosition[Y] + 2)
                    {
                        tailPosition[Y] = headPosition[Y] + 1;
                        tailPosition[X] = headPosition[X];

                        // add this position to the hash set
                        positionsVisited.Add($"{tailPosition[X]},{tailPosition[Y]}");
                    }
                }
            }
        }

        private void MoveLeft(string val)
        {
            if (!int.TryParse(val.ToString(), out int value))
            {
                throw new ArgumentException("Movement value is not a number");
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    // decrement the x position of the head
                    headPosition[X]--;

                    // if the tail is more than 1 y position above the head, move the tail directly behind the head
                    if (tailPosition[X] >= headPosition[X] + 2)
                    {
                        tailPosition[Y] = headPosition[Y];
                        tailPosition[X] = headPosition[X] + 1;

                        // add this position to the hash set
                        positionsVisited.Add($"{tailPosition[X]},{tailPosition[Y]}");
                    }
                }
            }
        }

        private void MoveRight(string val)
        {
            if (!int.TryParse(val.ToString(), out int value))
            {
                throw new ArgumentException("Movement value is not a number");
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    // decrement the x position of the head
                    headPosition[X]++;

                    // if the tail is more than 1 y position above the head, move the tail directly behind the head
                    if (tailPosition[X] <= headPosition[X] - 2)
                    {
                        tailPosition[Y] = headPosition[Y];
                        tailPosition[X] = headPosition[X] - 1;

                        // add this position to the hash set
                        positionsVisited.Add($"{tailPosition[X]},{tailPosition[Y]}");
                    }
                }
            }
        }
    }
}
