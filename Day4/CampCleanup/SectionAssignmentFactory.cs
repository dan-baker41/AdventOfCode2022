using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampCleanup
{
    public class SectionAssignmentFactory
    {
        /// <summary>
        /// Creates a Section Assignment object from an input string
        /// </summary>
        /// <param name="range">Specifies the section assignment range. Format must be [start]-[end]</param>
        /// <returns>Created Section Assignment object</returns>
        /// <exception cref="FormatException">Thrown if the range string is formatted incorrectly</exception>
        public static SectionAssignment Create(string range)
        {
            const string formatErrorMessage = "Input string format is incorrect";
            const string intParseErrorMessage = "@value could not be parsed";
            if (range == null)
                throw new FormatException(formatErrorMessage);

            // split start and end numbers
            var split = range.Split("-");
            if (split.Length != 2)
                throw new FormatException(formatErrorMessage);

            // parse the integer values
            var success = Int32.TryParse(split[0], out int start);
            if (!success)
                throw new FormatException(intParseErrorMessage.Replace("@value", "Start"));

            success = Int32.TryParse(split[1], out int end);
            if (!success)
                throw new FormatException(intParseErrorMessage.Replace("@value", "End"));

            // create the section assigment object and return it
            return new SectionAssignment(start, end);
        }
    }
}
