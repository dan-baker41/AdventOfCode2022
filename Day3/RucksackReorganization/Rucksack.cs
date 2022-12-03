using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RucksackReorganization
{
    public class Rucksack
    {
        /// <summary>
        /// The rucksack has two compartments.
        /// All Items of a given type must be within a one of the two compartments.
        /// The function will find the error item and return it's value.
        /// </summary>
        /// <param name="contents">contents of the compartment</param>
        /// <returns>value of the compartment item, or null terminating character if there is no error</returns>
        public static char FindCompartmentError(string contents)
        {
            var compartmentSize = contents.Length / 2;
            string compartment1 = contents.Substring(0, compartmentSize);
            string compartment2 = contents.Substring(compartmentSize, compartmentSize);

            HashSet<char> sortedCompartment1 = new HashSet<char>();
            HashSet<char> sortedCompartment2 = new HashSet<char>();

            // fill each set with the compartment contents. If an item already exists in the opposite set, it is the error item
            for(var i = 0; i < contents.Length / 2; i++)
            {
                var item1 = compartment1.ElementAt(i);
                var item2 = compartment2.ElementAt(i);

                // check if the next item in compartment 2 is in compartment 1
                if (sortedCompartment1.Contains(item2))
                    return item2;
                // check if the next item in compartment 1 is in compartment 2
                if (sortedCompartment2.Contains(item1))
                    return item1;
                if (item1 == item2)
                    return item1;

                // add items to their comparment hash sets
                sortedCompartment1.Add(item1);
                sortedCompartment2.Add(item2);
            }

            // if reached, return an error
            return '\0';
        }

        /// <summary>
        /// Finds the item that is contained in all three contents and returns the value
        /// </summary>
        /// <param name="contents1">rucksack contents of first elf</param>
        /// <param name="contents2">rucksack contents of second elf</param>
        /// <param name="contents3">rucksack contents of third elf</param>
        /// <returns>item value possessed by all three elves</returns>
        public static char FindGroupBadge(string contents1, string contents2, string contents3)
        {
            HashSet<char> firstHash = new HashSet<char>();
            HashSet<char> secondHash = new HashSet<char>();

            // narrow down contents1 to its unique items
            foreach(var i in contents1)
            {
                firstHash.Add(i);
            }

            // narrow down contents2 to the unique items which are also contained in contents1
            foreach(var i in contents2)
            {
                if (firstHash.Contains(i))
                    secondHash.Add(i);
            }

            // lastly, search contents 3 for the item which is also in secondHash
            foreach(var i in contents3)
            {
                if (secondHash.Contains(i))
                    return i;
            }

            return '\0';
        }

        /// <summary>
        /// Converts an item to its priority value.
        /// a - z = 1 - 26
        /// A - Z = 27 - 52
        /// </summary>
        /// <param name="value">item value</param>
        /// <returns>priority value of the item</returns>
        public static int PriorityValue(char value)
        {
            // validate value
            if ((value >= 'a' && value <= 'z') || (value >= 'A' && value <= 'Z'))
            {
                // convert ASCII value 'a' - 'z' to 1 - 26
                var priority = (char.ToLower(value) - 'a') + 1;

                // if the value is upper case, add 26
                if (char.IsUpper(value))
                    priority += 26;

                return priority;
            }
            else
                return 0;
        }
    }
}
