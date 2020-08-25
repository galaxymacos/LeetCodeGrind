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

            Console.WriteLine(StringAndArray.FindMin(new []{2,1}));
        }
    }
}