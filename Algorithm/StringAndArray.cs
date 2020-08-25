using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm
{
    public static class StringAndArray
    {
        public static int SearchIndex(int[] nums, int target)
        {
            return BinarySearch(nums, 0, nums.Length - 1, target);
        }

        public static int[][] HeBingQujian(int[][] intervals)
        {
            if (intervals.Length == 0)
                return intervals;
            // 以区间的第一个元素和第二个元素从小到大排序
            var intervalsSorted = intervals.OrderBy(element => element[0] ).ThenBy(element => element[1]).ToArray();
            // 先加上第一个空间
            List<int[]> spaces = new List<int[]> {intervalsSorted[0]};
            
            for (int i = 1; i < intervalsSorted.Length; i++)
            {
                // If the start index of the new space is bigger than the the end index of the old space 
                if (intervalsSorted[i][0] > spaces[spaces.Count - 1][1])
                {
                    spaces.Add(intervalsSorted[i]);
                }
                else if (spaces[spaces.Count - 1][1] < intervalsSorted[i][1])    // 如果改空间和之前的区间（也就是spaces里最大的那个区间有重合） 且新区间的最大值大于旧区间的最大值
                {
                    spaces[spaces.Count - 1][1] = intervalsSorted[i][1];    // 更新区间的最大值（最小值不用更新，因为新区间的最小值不可能比旧区间小（已经排好序了）
                }
            }

            return spaces.ToArray();
        }


        private static int BinarySearch(int[] nums, int left, int right, int target)
        {
            if (right < left)
            {
                return left;
            }
            int center = (left + right) / 2;
            if (nums[center] == target)
            {
                while (center + 1 < nums.Length && nums[center + 1] == target)
                {
                    center++;
                }

                return center;
            }

            if (nums[center] < target)
            {
                return BinarySearch(nums, center + 1, right, target);
            }

            else
            {
                return BinarySearch(nums, left, center - 1, target);
            }
            
        }

        public static void RotateMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = i+1; j < matrix[0].Length; j++)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[j][i];
                    matrix[j][i] = temp;
                }
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length/2; j++)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[i][matrix[0].Length - 1 - j];
                    matrix[i][matrix[0].Length - 1 - j] = temp;
                }
            }
        }
        
        public static void LingJuZhen(int[][] matrix) {
            List<int> rows = new List<int>();
            List<int> cols = new List<int>();
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        rows.Add(i);
                        cols.Add(j);
                    }
                }
            }

            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    matrix[rows[i]][j] = 0;
                }
            }
            for (int i = 0; i < cols.Count; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    matrix[j][cols[i]] = 0;
                }
            }
            

        }

        public static int[] DuiJiaoXianbianLi(int[][] matrix)
        {
            if (matrix.Length == 0)
            {
                return new int[0];
            }
            List<List<int>> subArrays = new List<List<int>>();
            for (int i = 0; i < matrix[0].Length; i++)
            {
                List<int> subArray = new List<int>();
                int rowIndex = 0;
                int colIndex = i;
                Console.WriteLine($"Start from {rowIndex}, {colIndex}");
                while (rowIndex < matrix.Length && colIndex >=0)
                {
                    subArray.Add(matrix[rowIndex++][colIndex--]);
                }
                subArrays.Add(subArray);
            }

            for (int i = 1; i < matrix.Length; i++)
            {
                List<int> subArray = new List<int>();
                int rowIndex = i;
                int colIndex = matrix[0].Length-1;
                while (rowIndex < matrix.Length && colIndex >=0)
                {
                    subArray.Add(matrix[rowIndex++][colIndex--]);
                }
                subArrays.Add(subArray);
            }

            

            for (int i = 0; i < subArrays.Count; i+=2)
            {
                subArrays[i].Reverse();
            }
            
            for (int i = 0; i < subArrays.Count; i++)
            {
                Console.Write($"List {i+1}: ");
                for (int j = 0; j < subArrays[i].Count; j++)
                {
                    Console.Write(subArrays[i][j]+" ");
                }

                Console.WriteLine();
            }

            List<int> result = new List<int>();
            for (int i = 0; i < subArrays.Count; i++)
            {
                for (int j = 0; j < subArrays[i].Count; j++)
                {
                    result.Add(subArrays[i][j]);
                }
            }

            return result.ToArray();

        }

        public static string LongestPalindrome(string s)
        {
            string targetString = ""; 
            for (int i = 0; i < s.Length; i++)
            {
                string potentialString = LongestLength(s, i, i);
                if (potentialString.Length > targetString.Length)
                {
                    targetString = potentialString;
                }
            }
            for (int i = 0; i < s.Length-1; i++)
            {
                string potentialString = LongestLength(s, i, i+1);
                if (potentialString.Length > targetString.Length)
                {
                    targetString = potentialString;
                }
            }
            return targetString;
        }

        private static string LongestLength(string s, int leftIndex, int rightIndex)
        {
            if (leftIndex < 0 || rightIndex >= s.Length || s[leftIndex] != s[rightIndex])
            {
                return s.Substring(leftIndex+1, (rightIndex-1)-(leftIndex+1)+1);
            }
            return LongestLength(s, leftIndex-1, rightIndex+1);
        }

    }
}