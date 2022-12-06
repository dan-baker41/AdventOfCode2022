using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyStacks
{
    public class ContainerStack
    {
        public ContainerStack(string stacksInput)
        {
            var lines = stacksInput.Split("\n");

            // the number of stacks can be found in the last line of input, which is the second to last
            // element in the array
            var lastLine = lines[lines.Length - 2];

            // parse the int value of the last number in the line
            if (Int32.TryParse(lastLine.ElementAt(lastLine.Length - 2).ToString(), out int stackCount))
            {
                stacks = new List<Stack<char>>(stackCount);
                for (var i = 0; i < stackCount; i++)
                    stacks.Add(new Stack<char>());
            }
            else
                throw new ArgumentException("Last input line is invalid");

            // iterate through the rest of the lines in reverse order to fill the stacks
            for(var i = lines.Length - 3; i >= 0; i--)
            {
                var lineIndexer = 0;

                // create a loop to iterate each stack
                for (var j = 0; j < stacks.Count; j++)
                {
                    lineIndexer++; // if there is an item for this stack, it will be at this index
                    if (lines[i][lineIndexer] != ' ')
                        stacks.ElementAt(j).Push(lines[i][lineIndexer]);

                    lineIndexer += 3; // move to next item
                }
            }
        }

        /// <summary>
        /// Moves an item from the top of one stack to another
        /// </summary>
        /// <param name="action">Specifies the source and destination</param>
        /// <exception cref="ArgumentException">If the source or destination are out of bounds</exception>
        public void PerformAction(ContainerStackAction action)
        {
            // make sure the source and destination is within bounds (action values are 1 greater than list indexers)
            if (action.MoveFrom < 1 || action.MoveFrom > stacks.Count)
                throw new ArgumentException($"Source stack {action.MoveFrom} is invalid");
            if (action.MoveTo < 1 || action.MoveTo > stacks.Count)
                throw new ArgumentException($"Destination stack {action.MoveFrom} is invalid");

            // get the corresponding stacks
            var from = stacks.ElementAt(action.MoveFrom - 1);
            var to = stacks.ElementAt(action.MoveTo - 1);

            // make sure there is enough quantity in the source stack
            if(action.Quantity > from.Count)
                throw new ArgumentException($"{action.Quantity} exceeds stack quantity of {from.Count}");

            // move the top element 1 at a time 
            for (var i = 0; i < action.Quantity; i++)
            {
                var item = from.Pop();
                to.Push(item);
            }
        }

        /// <summary>
        /// Returns the value of the top item on each stack as a string
        /// </summary>
        public string TopItemFromEach()
        {
            var topVals = "";

            foreach(var i in stacks)
            {
                topVals += i.Peek();
            }

            return topVals;
        }

        private List<Stack<char>> stacks;
    }
}
