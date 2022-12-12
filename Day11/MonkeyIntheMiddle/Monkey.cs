using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIntheMiddle
{
    public enum OperationType
    {
        Add = 1,
        Multiply = 2,
        Square = 3
    }

    public class Monkey
    {
        public int MonkeyId { get; set; }

        private Queue<UInt64> Items;
        public readonly OperationType OperationType;
        private int OperationValue;
        public int TestValue;
        private int MonkeyIdOnSuccess;
        private int MonkeyIdOnFail;

        public int LowestCommonDenominator;

        private int _TimesItemInspected;
        public int TimesItemsInspected { get { return _TimesItemInspected; } }

        public Monkey(string init)
        {
            Items = new Queue<UInt64>();
            _TimesItemInspected = 0;
            LowestCommonDenominator = 0;

            var lines = init.Split('\n');
            if (lines.Length != 7)
                throw new ArgumentException("Invalid number of lines");
            else
            {
                MonkeyId = lines[0][lines[0].Length - 2] - '0'; // convert ascii value to int

                // parse the starting items in the second line
                var items = lines[1].Split(' ');
                // start at index 4
                for(var i = 4; i < items.Length; i++)
                {
                    Items.Enqueue(UInt64.Parse(items[i].Replace(",", "")));
                }

                // parse the operation in the third line
                var operation = lines[2].Split(' ');
                if (operation[operation.Length - 2] == "*")
                    OperationType = OperationType.Multiply;
                else
                    OperationType = OperationType.Add;

                if (int.TryParse(operation[operation.Length - 1], out int opVal))
                    OperationValue = opVal;
                else
                {
                    OperationType = OperationType.Square;
                    OperationValue = 0;
                }

                // parse the test in the fourth line
                var test = lines[3].Split(' ');
                TestValue = int.Parse(test[test.Length - 1]);

                // find out which monkey ID on success with the fifth line
                MonkeyIdOnSuccess = lines[4][lines[4].Length - 1] - '0';

                // find out which monkey ID on fail with the sixth line
                MonkeyIdOnFail = lines[5][lines[5].Length - 1] - '0';
            }
        }
    
        public bool HasItems()
        {
            return Items.Count > 0;
        }

        public void PerformOperation(bool StillWorried, out int MonkeyId, out UInt64 Item)
        {
            // first, calculate the worry level of the current item
            var item = Items.Dequeue();
            UInt64 worryValue = item;

            switch(OperationType)
            {
                case OperationType.Add:
                    worryValue = item + (UInt64)OperationValue;
                    break;
                case OperationType.Multiply:
                    worryValue = item * (UInt64)OperationValue;
                    break;
                case OperationType.Square:
                    worryValue = item * item;
                    break;
                default:
                    throw new Exception("Invalid operation type");
            }

            if (worryValue < item)
                Console.WriteLine("help");
            // monkey inspects item
            _TimesItemInspected++;

            if (!StillWorried)
            {
                // worry value is divided by 3 after inspection
                worryValue /= 3;
            }
            else
            {
                worryValue %= (UInt64)LowestCommonDenominator;
            }

            // test the worry value
            if(worryValue % (UInt64)TestValue == 0)
            {
                MonkeyId = MonkeyIdOnSuccess;
            }
            else
            {
                MonkeyId = MonkeyIdOnFail;
            }

            Item = worryValue;
        }

        public void GiveItem(UInt64 item)
        {
            Items.Enqueue(item);
        }
    }
}
