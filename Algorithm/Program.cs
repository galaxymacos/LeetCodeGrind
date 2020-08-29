using System;

namespace Algorithm
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // var longestPalindrome = StringAndArray.LongestPalindrome("babad");
            // Console.WriteLine(longestPalindrome);

            // char[] p = {'A', 'C', 'T', 'G', 'P', 'A', 'C', 'Y'};
            // char[] s = {'A', 'C', 'T', 'G', 'P', 'A', 'C', 'T', 'G', 'K', 'A', 'C', 'T', 'G', 'P', 'A', 'C', 'Y'};

            // int result = StringAndArray.KMPMatch(p,s);
            // Console.WriteLine($"Matching array start index: {result}");
            // Console.WriteLine(StringAndArray.ReverseWords2("Let's take LeetCode contest"));
            // Console.WriteLine(StringAndArray.MyAtoi("  0000000000012345678"));
            // DynamicProgramming dp = new DynamicProgramming();
            // Console.WriteLine(dp.MaxSubArray(new int[] {-2,1,-3,4,-1,2,1,-5,4}));

            Mathematics mathe = new Mathematics();
            mathe.IsPowerOfThree(3);

            int[][] matrix = 
            {
                new[]{0,2,3,4},
                new[]{5,6,7,8},
                new[]{9,10,0,12},
                new[]{13,14,15,16}
            };
            StringAndArray.SetZeroes(matrix);

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    Console.Write(matrix[i][j]+" ");
                }

                Console.WriteLine();
            }
        }
    }
}