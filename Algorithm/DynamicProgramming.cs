using System;
using System.Collections.Generic;

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

        #region 跳跃游戏

        private Dictionary<int, bool> dic;
        
        public bool CanJump(int[] nums)
        {

            int farthestIndex = nums[0];
            int currentIndex = 0;
            while(currentIndex < farthestIndex)
            {
                if (currentIndex+1+nums[currentIndex] > farthestIndex)
                {
                    farthestIndex = nums[currentIndex]+1+currentIndex;
                    if (farthestIndex >= nums.Length - 1)
                    {
                        return true;
                    }
                }

                currentIndex++;
            }
            return false;

        }

        private Dictionary<(int, int), int> roadCount;
        public int UniquePaths(int m, int n)
        {
            roadCount = new Dictionary<(int, int), int>();
            roadCount.Add((m-1,n),1);
            roadCount.Add((m,n),1);
            roadCount.Add((m,n-1),1);
            return UniquePathsSearch(m, n, 1, 1);
            
        }

        public int UniquePathsSearch(int m, int n, int i, int j)
        {
            if (roadCount.ContainsKey((i, j)))
            {
                return roadCount[(i, j)];
            }
            if (i > m)
                return 0;
            if (j > n)
            {
                return 0;
            }
            roadCount.Add((i,j),UniquePathsSearch(m, n, i + 1, j)+UniquePathsSearch(m,n,i, j+1));
            return roadCount[(i, j)];
        }
        #endregion

        #region 零钱兑换

        private Dictionary<int, int> coinDic;
        public int CoinChange(int[] coins, int amount)
        {
            coinDic = new Dictionary<int, int>();
            for (int i = 0; i < coins.Length; i++)
            {
                coinDic.Add(coins[i],1);
            }

            CoinChangeRecur(coins, amount);
            return coinDic[amount];
        }

        public void CoinChangeRecur(int[] coins, int curMoney)
        {
            if (curMoney <= 0)
            {
                return;
            }
            if (coinDic.ContainsKey(curMoney))
            {
                return;
            }

            int minCoinNeeded = int.MaxValue;
            foreach (var coinValue in coins)
            {
                if (coinDic.ContainsKey(curMoney - coinValue))
                {
                    if (coinDic[curMoney - coinValue] + 1 < minCoinNeeded)
                    {
                        minCoinNeeded = coinDic[curMoney - coinValue] + 1;
                    }
                }
                else
                {
                    Console.WriteLine(curMoney - coinValue);
                    CoinChangeRecur(coins, curMoney - coinValue);
                    if (!coinDic.ContainsKey(curMoney - coinValue))
                    {
                        continue;
                    }
                    int test = coinDic[curMoney - coinValue] + 1;
                    if (test != -1 && test+1<minCoinNeeded)
                    {
                        minCoinNeeded = test + 1;
                    }
                }
            }

            if (minCoinNeeded != int.MaxValue)
            {
                coinDic.Add(curMoney, minCoinNeeded);
            }
        }

        #endregion
    }
}