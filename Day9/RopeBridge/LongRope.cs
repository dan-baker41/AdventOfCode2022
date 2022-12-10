using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RopeBridge
{
    public class LongRope
    {
        private int X = 0, Y = 1, HEAD = 0, TAIL = 0;

        private List<int[]> positions;
        private HashSet<string> positionsTailVisited;

        public LongRope(int ropeSize)
        {
            positions = new List<int[]>(ropeSize);
            for (var i = 0; i < ropeSize; i++) {
                var p = new int[2] { 0, 0 };
                positions.Add(p);
            }

            TAIL = ropeSize - 1;

            positionsTailVisited = new HashSet<string>();
            positionsTailVisited.Add("0,0");
        }

        public int PositionsVisitedByTail()
        {
            return positionsTailVisited.Count;
        }

        public void Move(string movement)
        {
            // make sure the movement string is the correct length
            if (movement.Length < 3)
                throw new ArgumentException("Movement input value is invalid");

            // find the movement value
            if (!int.TryParse(movement.Substring(2, movement.Length - 2), out int moveVal))
                throw new ArgumentException("Movement Value is not a number");

            // check the movement direction
            switch (movement[0])
            {
                case 'R':
                    MoveRight(moveVal);
                    break;
                case 'L':
                    MoveLeft(moveVal);
                    break;
                case 'U':
                    MoveUp(moveVal);
                    break;
                case 'D':
                    MoveDown(moveVal);
                    break;
                default:
                    throw new ArgumentException("Movement direction is invalid");
            }
        }

        private void MoveLeft(int numToMove)
        {
            // iterate through a loop numToMove times
            for(var i =  0; i < numToMove; i++)
            {
                // move the head to the left
                positions[HEAD][X]--;

                // update every other rope position
                MoveChain();

                // after every movement, record the tail position
                AddTailPosition();
            }
        }

        private void MoveRight(int numToMove)
        {
            // iterate through a loop numToMove times
            for (var i = 0; i < numToMove; i++)
            {
                // move the head to the right
                positions[HEAD][X]++;

                // update every other rope position
                MoveChain();

                // after every movement, record the tail position
                AddTailPosition();
            }
        }

        private void MoveUp(int numToMove)
        {
            // iterate through a loop numToMove times
            for (var i = 0; i < numToMove; i++)
            {
                // move the head upwards
                positions[HEAD][Y]--;

                // update every other rope position
                MoveChain();

                // after every movement, record the tail position
                AddTailPosition();
            }
        }

        private void MoveDown(int numToMove)
        {
            // iterate through a loop numToMove times
            for (var i = 0; i < numToMove; i++)
            {
                // move the head downwards
                positions[HEAD][Y]++;

                // update every other rope position
                MoveChain();

                // after every movement, record the tail position
                AddTailPosition();
            }
        }

        private void MoveChain()
        {
            var parentX = positions[HEAD][X];
            var parentY = positions[HEAD][Y];
            for(var i = HEAD + 1; i <= TAIL; i++)
            {
                var moved = false;

                // calculate the delta between this segment and it's parent
                var deltaX = parentX - positions[i][X];
                var deltaY = parentY - positions[i][Y];
                var absDeltaX = Math.Abs(deltaX);
                var absDeltaY = Math.Abs(deltaY);

                // check which delta is greater
                if(absDeltaX > absDeltaY && absDeltaX >= 2)
                {
                    if (deltaX < 0)
                        positions[i][X] = parentX + 1;
                    else
                        positions[i][X] = parentX - 1;
                    positions[i][Y] = parentY;
                    moved = true;
                }
                else if(absDeltaY > absDeltaX && absDeltaY >= 2)
                {
                    positions[i][X] = parentX;

                    if(deltaY < 0)
                        positions[i][Y] = parentY + 1;
                    else
                        positions[i][Y] = parentY - 1;

                    moved = true;
                }
                else if(absDeltaX == absDeltaY && absDeltaX == 2)
                {
                    if (deltaY < 0)
                        positions[i][Y] = parentY + 1;
                    else
                        positions[i][Y] = parentY - 1;

                    if (deltaX < 0)
                        positions[i][X] = parentX + 1;
                    else
                        positions[i][X] = parentX - 1;

                    moved = true;
                }

                // if this segment didn't move, there is no need to continue
                if (!moved)
                    break;
                else
                {
                    parentX = positions[i][X];
                    parentY = positions[i][Y];
                }
            }
        }

        private void AddTailPosition()
        {
            positionsTailVisited.Add($"{positions[TAIL][X]},{positions[TAIL][Y]}");
        }
    }
}
