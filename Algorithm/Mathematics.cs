using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    public class Mathematics
    {
        public IList<string> FizzBuzz(int n) {
            IList<string> result = new List<string>();
            for (int i = 1; i <= n; i++)
            {
                var sb = new StringBuilder();

                bool meetCondition = false;
                if (i % 3 == 0)
                {
                    sb.Append("Fizz");
                    meetCondition = true;
                }
                if (i % 5 == 0)
                {
                    sb.Append("Buzz");
                    meetCondition = true;
                }
                
                if(!meetCondition)
                {
                    sb.Append(i.ToString());
                }
                result.Add(sb.ToString());

            }

            return result;
        }
        
        public bool IsPowerOfThree(int n)
        {
            if (n < 1)
            {
                return false;
            }
            while (n > 1)
            {
                var yushu = n % 3;
                n = n / 3;
                if (yushu != 0)
                {
                    return false;
                }
            }

            return true;
        }
        
        public int HammingWeight(uint n)
        {
            int count = 0;
            while (n > 0)
            {
                if (n % 2 == 1)
                {
                    count++;
                }
                n /= 2;
            }

            return count;
        }
        
        public int HammingDistance(int x, int y) {
            int distance = 0;
            int xor = x^y;
            while(xor > 0){
                if((xor & 1) == 1){
                    distance++;
                }

                xor >>= 1;
            }
            return distance;
        }
        
        public uint reverseBits(uint n)
        {
            uint result = 0;
            int mask = 1;
            for(int i = 31;i>=0;i--)
            {
                if ((n & mask) != 0)
                {
                    result += 1 * (uint)Math.Pow(2, i);
                }

                Console.WriteLine(mask);
                mask <<= 1;
            }

            return result;
        }

        public int MissingNumber(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i <= nums.Length; i++)
            {
                set.Add(i);
            }

            foreach (int num in nums)
            {
                set.Remove(num);
            }

            foreach (int i in set)
            {
                return i;
            }

            return -1;
        }

        #region 拿硬币
        
        public int MinCount(int[] coins)
        {
            int count = 0;
            foreach (var coinHeap in coins)
            {
                count += (coinHeap + 1) / 2;
            }

            return count;
        }

        #endregion
    }
}