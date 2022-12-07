using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyStacks
{
    public class ContainerStackAction
    {
        public ContainerStackAction(string action)
        {
            var split = action.Split(" ");

            // initialize each value
            if(!Int32.TryParse(split[1], out int qty)
                || !Int32.TryParse(split[3], out int from)
                || !Int32.TryParse(split[5], out int to)
            )
            {
                throw new ArgumentException("Invalid input string");
            }
            else
            {
                Quantity = qty;
                MoveFrom = from;
                MoveTo = to;
            }
        }

        public int MoveFrom { get; set; }
        public int MoveTo { get; set; }
        public int Quantity { get; set; }
    }
}
