using System;
using System.Collections.Generic;

namespace Algorithm
{
    public class IndexClass:IComparable
    {
        private readonly Dictionary<int,int> map;

        public IndexClass(int index, Dictionary<int,int> map)
        {
            this.map = map;
            this.index = index;
        }
        public readonly int index;
        public int CompareTo(object obj)
        {
            if (map[index] > ((IndexClass) obj).map[((IndexClass) obj).index])
            {
                return 1;
            }

            if(map[index] < ((IndexClass) obj).map[((IndexClass) obj).index])
            {
                return -1;
            }

            return 0;
        }

        public override string ToString()
        {
            return index.ToString();
        }
    }
}