// 回溯算法

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Algorithm
{
    public class HuiSuSuanFa
    {
        #region 电话号码的字母组合

        private Dictionary<int, string> dic;
        private IList<string> result;

        public IList<string> LetterCombinations(string digits)
        {
            result = new List<string>();

            if (string.IsNullOrEmpty(digits))
            {
                return result;
            }

            dic = new Dictionary<int, string>();
            dic.Add(2, "abc");
            dic.Add(3, "def");
            dic.Add(4, "ghi");
            dic.Add(5, "jkl");
            dic.Add(6, "mno");
            dic.Add(7, "pqrs");
            dic.Add(8, "tuv");
            dic.Add(9, "wxyz");

            Queue<int> digitsInInt = new Queue<int>();
            for (int i = 0; i < digits.Length; i++)
            {
                digitsInInt.Enqueue(digits[i] - '0');
            }

            Helper(digitsInInt, "");
            return result;
        }

        public void Helper(Queue<int> queue, string curStr)
        {
            if (queue.Count == 0)
            {
                result.Add(curStr);
                return;
            }

            int num = queue.Peek();
            string letters = dic[num];
            for (int i = 0; i < letters.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(curStr);
                sb.Append(letters[i]);
                var newQueue = new Queue<int>(queue);
                newQueue.Dequeue();
                Helper(newQueue, sb.ToString());
            }
        }

        #endregion

        #region 括号生成

        private List<string>[] cache = new List<string>[100];

        public IList<string> GenerateParenthesis(int n)
        {
            return Generate(n);
        }

        public List<string> Generate(int n)
        {
            if (cache[n] != null)
            {
                return cache[n];
            }

            List<string> ans = new List<string>();
            if (n == 0)
            {
                ans.Add("");
            }
            else
            {
                for (int c = 0; c < n; c++)
                {
                    foreach (string left in Generate(c))
                    {
                        foreach (string right in Generate(n - 1 - c))
                        {
                            ans.Add($"({left}){right}");
                        }
                    }
                }
            }

            cache[n] = ans;
            return ans;
        }

        #endregion

        #region 全排列

        private IList<IList<int>> allPermutations;

        public IList<IList<int>> Permute(int[] nums)
        {
            allPermutations = new List<IList<int>>();
            HeapPermutation(nums, nums.Length, nums.Length);
            return allPermutations;
        }

        public void HeapPermutation(int[] a, int size, int n)
        {
            if (size == 1)
            {
                List<int> curPermutation = new List<int>();
                for (int i = 0; i < n; i++)
                {
                    curPermutation.Add(a[i]);
                }

                allPermutations.Add(curPermutation);
            }

            for (int i = 0; i < size; i++)
            {
                HeapPermutation(a, size - 1, n);

                // if size is odd, swap first and last element
                if (size % 2 == 1)
                {
                    int temp = a[0];
                    a[0] = a[size - 1];
                    a[size - 1] = temp;
                }
                else
                {
                    int temp = a[i];
                    a[i] = a[size - 1];
                    a[size - 1] = temp;
                }
            }
        }

        #endregion

        #region 子集

        #region 递归

        public IList<IList<int>> Subsets(int[] nums)
        {
            IList<IList<int>> output = new List<IList<int>>();
            output.Add(new List<int>());

            foreach (var num in nums)
            {
                IList<List<int>> newSubsets = new List<List<int>>();
                foreach (IList<int> curr in output)
                {
                    var newList = new List<int>(curr);
                    newList.Add(num);
                    newSubsets.Add(newList);
                }

                foreach (List<int> curr in newSubsets)
                {
                    output.Add(curr);
                }
            }

            return output;
        }

        #endregion

        #region 回溯

        List<List<int>> output = new List<List<int>>();
        private int n, k;

        public List<List<int>> Subsets2(int[] nums)
        {
            n = nums.Length;
            for (k = 0; k < n+1; k++)
            {
                BackTrack(0, new List<int>(), nums );
            }

            return output;
        }

        public void BackTrack(int first, List<int> curr, int[] nums)
        {
            // if the combination is done
            if (curr.Count == k)
            {
                output.Add(new List<int>(curr));
            }

            for (int i = first; i < n; i++)
            {
                // add i into the current combination
                curr.Add(nums[i]);
                // Use next integers to complete the combination
                BackTrack(i + 1, curr, nums);
                // backtrack
                curr.RemoveAt(curr.Count - 1);
            }
        }
        

        #endregion

        #region 字典排序（二进制排序）子集

        public List<List<int>> Subsets3(int[] nums)
        {
            List<List<int>> output = new List<List<int>>();
            int n = nums.Length;

            for (int i = (int)Math.Pow(2, n); i < (int)Math.Pow(2, n+1); i++)
            {
                String bitmask = Convert.ToString(i, 2).Substring(1);
                
                List<int> curr = new List<int>();
                for (int j = 0; j < bitmask.Length; j++)
                {
                    if (bitmask[j] == '1')
                    {
                        curr.Add(nums[j]);
                    }
                }
                output.Add(curr);
            }

            return output;
        }

        #endregion

        #region 单词搜索

        private bool hasFoundWord;

        public bool Exist(char[][] board, string word)
        {
            Queue<(int,int)> origins = new Queue<(int,int)>();
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length; j++)
                {
                    if (board[i][j] == word[0])
                    {
                        origins.Enqueue((i,j));
                    }
                }
            }

            while (origins.Count > 0)
            {
                var origin = origins.Dequeue();
                if (word.Length == 1)
                {
                    return true;
                }
                FindAround(board, origin, word, 1, new HashSet<(int,int)>());
                if (hasFoundWord)
                {
                    return true;
                }
                
            }

            return false;


        }

        public void FindAround(char[][] board,(int, int) origin, string target, int currentIndex, HashSet<(int,int)> set)
        {
            if (hasFoundWord || set.Contains(origin))
            {
                return;
            }
            if (currentIndex >= target.Length)
            {
                hasFoundWord = true;
                return;
            }
            

            set.Add(origin);
            // Check top
            var topPos = (origin.Item1 - 1, origin.Item2);
            // Check bottom
            var bottomPos = (origin.Item1 + 1, origin.Item2);
            // Check left
            var leftPos = (origin.Item1, origin.Item2 - 1);
            // Check right
            var rightPos = (origin.Item1, origin.Item2 + 1);

            if (topPos.Item1 >= 0 && board[topPos.Item1][topPos.Item2] == target[currentIndex] )
            {
                FindAround(board, topPos, target, currentIndex+1, new HashSet<(int, int)>(set));
            }
            if (bottomPos.Item1 < board.Length && board[bottomPos.Item1][bottomPos.Item2] == target[currentIndex] )
            {
                FindAround(board, bottomPos, target, currentIndex+1, new HashSet<(int, int)>(set));
            }
            if (leftPos.Item2 >= 0 && board[leftPos.Item1][leftPos.Item2] == target[currentIndex] )
            {
                FindAround(board, leftPos, target, currentIndex+1, new HashSet<(int, int)>(set));
            }
            if (rightPos.Item2 < board[0].Length && board[rightPos.Item1][rightPos.Item2] == target[currentIndex] )
            {
                FindAround(board, rightPos, target, currentIndex+1, new HashSet<(int, int)>(set));
            }

        }

        #endregion

        #endregion
    }
}