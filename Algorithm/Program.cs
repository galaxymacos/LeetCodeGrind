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
            Automaton automaton = new Automaton();
            string target = " 0000000123456";
            for (int i = 0; i < target.Length; i++)
            {
                automaton.Get(target[i]);
            }

            Console.WriteLine(automaton.sign * automaton.ans);
        }
    }
}