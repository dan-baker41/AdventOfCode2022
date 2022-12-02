using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeDay1CalorieCounting
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
            if(!File.Exists(fileName))
            {
                Console.WriteLine($"{fileName} doesn't exist!");
            }
            else
            {
                // read all lines from the file to initialize the inventory list
                _inventoryList.AddRange(File.ReadAllLines(fileName));
            }
        }

        public void DisplayAnswer()
        {
            Console.WriteLine(_CalculateAnswer());
        }

        /// <summary>
        /// Calculates the highest calorie count and returns the value as an int
        /// </summary>
        /// <returns>The highest calorie count held by an elf</returns>
        private int _CalculateAnswer()
        {
            // list to store calculated calorie counts
            var calorieCounts = new List<int>();
            calorieCounts.Add(0); 

            // iterate the entire inventory list
            foreach(var i in _inventoryList)
            {
                // if the list item is an empty string, the current elf's inventory has been fully counted.
                // add a new entry to calculate the next elf's inventory
                if (i == "")
                    calorieCounts.Add(0);
                else
                    calorieCounts[calorieCounts.Count - 1] += Int32.Parse(i); // add this calorie to the current count
            }

            // return the highest value
            return calorieCounts.Max();
        }

        private List<string> _inventoryList;
    }
}
