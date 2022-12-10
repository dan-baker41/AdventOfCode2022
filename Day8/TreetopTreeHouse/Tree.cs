using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreetopTreeHouse
{
    public class Tree
    {
        public Tree(int size) 
        {
            Size = size;
            VisibleDown = false;
            VisibleLeft = false;
            VisibleRight = false;
            VisibleUp = false;
            ScenicDown = 0;
            ScenicLeft = 0;
            ScenicRight = 0;
            ScenicUp = 0;
        }

        public int Size { get; set; }
        public bool VisibleUp { get; set; }
        public bool VisibleDown { get; set; }
        public bool VisibleLeft { get; set; }
        public bool VisibleRight { get; set; }

        public int ScenicUp { get; set; }
        public int ScenicDown { get; set; }
        public int ScenicLeft { get; set; }
        public int ScenicRight { get; set; }

        public bool IsVisible { get { return VisibleLeft || VisibleRight || VisibleUp || VisibleDown; } }
        public int ScenicScore { get { return ScenicLeft * ScenicRight * ScenicUp * ScenicDown; } }
    }
}
