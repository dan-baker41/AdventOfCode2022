using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistressSignal
{
    public class SignalListItem
    {
        public SignalListItem() { }
        public SignalListItem(string signal) {
            Add(signal);
        }
        int Number = -1;
        //List<int> Numbers = new List<int>();
        List<SignalListItem?> SignalListItems = new List<SignalListItem?>();

        public void Add(string list)
        {
            // make sure the list is at least length 2
            if (list.Length < 2)
                return;

            // remove open and closing brackets
            list = list.Substring(1, list.Length - 2);

            // if length is 0, it's an empty number list
            if(list.Length == 0)
            {
                SignalListItems.Add(null);
            }
            else
            {
                for (int i = 0; i < list.Length; i++)
                {
                    // if another list, find the closing bracket and add it as a signal list item
                    if (list[i] == '[')
                    {
                        var end = findEndBracket(list, i);

                        var item = new SignalListItem();
                        item.Add(list.Substring(i, (end - i) + 1));
                        SignalListItems.Add(item);
                        i = end + 1;
                    }
                    else if (list[i] != ',')
                    {
                        string number = "";
                        while (i < list.Length && list[i] != ',' && list[i] != '[' && list[i] != ']')
                        {
                            number += list[i];
                            i++;
                        }
                        var item = new SignalListItem();
                        item.Number = int.Parse(number);
                        SignalListItems.Add(item);
                    }
                }
            }
        }

        private int findEndBracket(string source, int startFrom)
        {
            var nextEndBracket = source.IndexOf(']', startFrom + 1);
            var nextStartBracket = source.IndexOf('[', startFrom + 1);

            while(nextStartBracket != -1 && nextEndBracket > nextStartBracket)
            {
                nextEndBracket = source.IndexOf(']', nextEndBracket + 1);
                nextStartBracket = source.IndexOf('[', nextStartBracket + 1);
            }

            return nextEndBracket;
        }
    
        public bool? Compare(SignalListItem item)
        {
            if (SignalListItems.Count == 0 && item.SignalListItems.Count == 0)
            {
                if (Number < item.Number)
                    return true;
                else if (Number > item.Number)
                    return false;
                else
                    return null;
            }
            else
            {
                // in this case, I know at least one of the two sides is a list. If either or the other
                // sides is NOT a list, convert it into a list
                List<SignalListItem> leftList;
                List<SignalListItem> rightList;
                if (Number >= 0)
                {
                    leftList = new List<SignalListItem>();
                    var newItem = new SignalListItem();
                    newItem.Number = Number;
                    leftList.Add(newItem);
                }
                else
                {
                    leftList = SignalListItems!;
                }


                if (item.Number >= 0)
                {
                    rightList= new List<SignalListItem>();
                    var newItem = new SignalListItem();
                    newItem.Number = item.Number;
                    rightList.Add(newItem);
                }
                else
                {
                    rightList = item.SignalListItems!;
                }

                for (var i = 0; i < leftList.Count && i < rightList.Count; i++)
                {

                    var left = leftList[i];
                    var right = rightList[i];

                    if (left == null && right != null)
                        return true;
                    else if (left != null && right == null)
                        return false;
                    else if (right != null && left != null)
                    {
                        var result = left!.Compare(right!);

                        if (result != null)
                            return result;
                    }
                }

                if (leftList.Count == rightList.Count)
                    return null;

                return rightList.Count > leftList.Count;
            }
        }
    }
}
