using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeDay1Part2
{
    public class CalorieCounter
    {
        /// <summary>
        /// Initializes object with data from the input file
        /// </summary>
        public CalorieCounter(string fileName)
        {
            _inventoryList = new List<string>();

            // make sure the file exists
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"{fileName} doesn't exist!");
            }
            else
            {
                // read all lines from the file to initialize the inventory list
                _inventoryList.AddRange(File.ReadAllLines(fileName));
            }
        }

        /// <summary>
        /// Displays the sum of the hightest calorie counts held by the elves.
        /// </summary>
        /// <param name="range">The number of elves to include in the sum</param>
        public void DisplayHighestCounts(int range)
        {
            Console.WriteLine(_CalculateAnswer(range));
        }

        /// <summary>
        /// Calculates the highest calorie counts and returns the value as an int
        /// </summary>
        /// <param name="range">The number of elves to include in the sum</param>
        /// <returns>The highest calorie count held by an elf</returns>
        private int _CalculateAnswer(int range)
        {
            // list to store calculated calorie counts
            var calorieCounts = new List<int>();
            calorieCounts.Add(0);

            // iterate the entire inventory list
            foreach (var i in _inventoryList)
            {
                // if the list item is an empty string, the current elf's inventory has been fully counted.
                // add a new entry to calculate the next elf's inventory
                if (i == "")
                    calorieCounts.Add(0);
                else
                    calorieCounts[calorieCounts.Count - 1] += Int32.Parse(i); // add this calorie to the current count
            }

            // return the highest values
            var sum = 0;
            for(var i = 0; i < range; i++)
            {
                // add the highest calorie count
                var highest = calorieCounts.Max();
                sum += highest;

                // remove the highest count
                calorieCounts.Remove(highest);
            }
            return sum;
        }

        private List<string> _inventoryList;
    }
}