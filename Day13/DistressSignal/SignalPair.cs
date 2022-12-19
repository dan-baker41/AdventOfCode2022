using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistressSignal
{
    public class SignalPair
    {
        SignalListItem Left = new SignalListItem();
        SignalListItem Right = new SignalListItem();

        public SignalPair(string left, string right)
        {
            Left.Add(left);
            Right.Add(right);
        }

        public bool? SignalInRightOrder()
        {
            return Left.Compare(Right);
        }

        private void _Init(string source, List<List<int>> lists)
        {
            // ignore opening [ and closing ]
            for(var i = 1; i < source.Length - 1; i+=2)
            {
                var list = new List<int>();
                if (source[i] == '[')
                {
                    while (source[i] != ']')
                    {
                        i++;
                        if (source[i] == '[')
                            lists.Add(new List<int>());
                        else if (source[i] != ',')
                            list.Add((int)(source[i] - '0'));
                    }
                }
                else
                {
                    list.Add((int)(source[i] - '0'));
                }

                lists.Add(list);
            }
        }
    }
}
