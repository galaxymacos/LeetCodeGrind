using System;

namespace Algorithm
{
    public class DynamicProgramming
    {
        public int MaxProfit(int[] prices) {
            int lowestPrice = prices[0];
            int maxProfit = int.MinValue;
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] < lowestPrice)
                {
                    lowestPrice = prices[i];
                }
                else if (prices[i] - lowestPrice > Math.Max(0,maxProfit))
                {
                    maxProfit = prices[i] - lowestPrice;
                }

                
            }
            
            return maxProfit == int.MinValue ? 0 : maxProfit;
        }
        
        /// <summary>
        /// 最大子序和
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MaxSubArray(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int maxSum = int.MinValue;
            int curSum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                curSum += nums[i];
                if (curSum > maxSum)
                {
                    maxSum = curSum;
                }
                if (curSum < 0)
                {
                    curSum = 0;
                }
            }
            return maxSum;
        }
    }
}