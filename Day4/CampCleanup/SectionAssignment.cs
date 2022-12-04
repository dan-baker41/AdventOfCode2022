﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampCleanup
{
    public class SectionAssignment
    {
        public SectionAssignment(int start, int end)
        {
            _Start = start;
            _End = end;
        }

        /// <summary>
        /// Checks if the passed assignment is fully contained by this assignment
        /// </summary>
        /// <param name="assignment">Assignment to check if fully contained</param>
        /// <returns>True if the passed assignment is fully contained</returns>
        public bool DoesFullyContain(SectionAssignment assignment)
        {
            return Start <= assignment.Start && End >= assignment.End;
        }

        // beginning of the assignment range
        private int _Start;
        public int Start { get { return _Start; } }

        // end of the assignment range
        private int _End;
        public int End { get { return _End; } }
    }
}