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
