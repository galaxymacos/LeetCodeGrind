using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

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
            var intervalsSorted = intervals.OrderBy(element => element[0]).ThenBy(element => element[1]).ToArray();
            // 先加上第一个空间
            List<int[]> spaces = new List<int[]> {intervalsSorted[0]};

            for (int i = 1; i < intervalsSorted.Length; i++)
            {
                // If the start index of the new space is bigger than the the end index of the old space 
                if (intervalsSorted[i][0] > spaces[spaces.Count - 1][1])
                {
                    spaces.Add(intervalsSorted[i]);
                }
                else if (spaces[spaces.Count - 1][1] < intervalsSorted[i][1]
                ) // 如果改空间和之前的区间（也就是spaces里最大的那个区间有重合） 且新区间的最大值大于旧区间的最大值
                {
                    spaces[spaces.Count - 1][1] = intervalsSorted[i][1]; // 更新区间的最大值（最小值不用更新，因为新区间的最小值不可能比旧区间小（已经排好序了）
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
                for (int j = i + 1; j < matrix[0].Length; j++)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[j][i];
                    matrix[j][i] = temp;
                }
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length / 2; j++)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[i][matrix[0].Length - 1 - j];
                    matrix[i][matrix[0].Length - 1 - j] = temp;
                }
            }
        }

        public static void LingJuZhen(int[][] matrix)
        {
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
                while (rowIndex < matrix.Length && colIndex >= 0)
                {
                    subArray.Add(matrix[rowIndex++][colIndex--]);
                }

                subArrays.Add(subArray);
            }

            for (int i = 1; i < matrix.Length; i++)
            {
                List<int> subArray = new List<int>();
                int rowIndex = i;
                int colIndex = matrix[0].Length - 1;
                while (rowIndex < matrix.Length && colIndex >= 0)
                {
                    subArray.Add(matrix[rowIndex++][colIndex--]);
                }

                subArrays.Add(subArray);
            }


            for (int i = 0; i < subArrays.Count; i += 2)
            {
                subArrays[i].Reverse();
            }

            for (int i = 0; i < subArrays.Count; i++)
            {
                Console.Write($"List {i + 1}: ");
                for (int j = 0; j < subArrays[i].Count; j++)
                {
                    Console.Write(subArrays[i][j] + " ");
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

            for (int i = 0; i < s.Length - 1; i++)
            {
                string potentialString = LongestLength(s, i, i + 1);
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
                return s.Substring(leftIndex + 1, (rightIndex - 1) - (leftIndex + 1) + 1);
            }

            return LongestLength(s, leftIndex - 1, rightIndex + 1);
        }


        public static string ReverseWords(string s)
        {
            List<string> words = new List<string>();
            bool foundSpace = false;
            List<char> currentWord = new List<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    foundSpace = true;
                }
                else
                {
                    if (foundSpace && currentWord.Count > 0)
                    {
                        words.Add(new string(currentWord.ToArray()));
                        currentWord.Clear();
                    }
                    foundSpace = false;
                    currentWord.Add(s[i]);

                }
            }

            if (currentWord.Count > 0)
            {
                words.Add(new string(currentWord.ToArray()));
            }

            words.Reverse();

            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
            {
                sb.Append(word);
                if (word != words[words.Count - 1])
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// Knuth–Morris–Pratt（KMP）算法是一种改进的字符串匹配算法，它的核心是利用匹配失败后的信息，尽量减少模式串与主串的匹配次数以达到快速匹配的目的。它的时间复杂度是 O(m+n)O(m + n)O(m+n)。
        /// 解释地址：https://leetcode-cn.com/leetbook/read/array-and-string/cpoo6/
        /// </summary>
        /// <param name="p">模式串</param>
        /// <param name="s">原字符串</param>
        /// <returns>在原字符串中匹配到的模式串的startIndex</returns>
        public static int KMPMatch(char[] p, char[] s)
        {
            int[] next = BuildNext(p);
            int i = 0;    // 文本串指针
            int j = 0;    // 模式串指针
            while (j < p.Length && i < s.Length)
            {
                // j<0的情况会出现在next数组第一位，接下来肯定要进行两个数组的下一个元素匹配
                if (j < 0 || s[i] == p[j])
                {
                    j++;
                    i++;
                }
                else
                {
                    j = next[j];
                }
            }

            return i - j;
        }

        public static int[] BuildNext(char[] p)
        {
            int[] next = new int[p.Length];

            for (int i = 0; i <= p.Length; i++)
            {
                char[] subCharArray = new string(p).Substring(0,i).ToCharArray();
                if (subCharArray.Length == 0)
                {
                    next[i] = -1;
                    continue;
                }
                List<string> commonPrefix = new List<string>();
                List<string> commonSuffix = new List<string>();
                List<char> curStr = new List<char>();
            
                // 创建前缀数组
                for (int k = 0; k < subCharArray.Length-1; k++)
                {
                    for (int j = 0; j <= k; j++)
                    {
                        curStr.Add(subCharArray[j]);
                    }
                    commonPrefix.Add(new string(curStr.ToArray()));
                    curStr.Clear();
                }

            
                // 创建后缀数组
                // i 为 整个数组的长度
                for (int k = 0; k < subCharArray.Length-1; k++)
                {
                    for (int j = subCharArray.Length-1-k; j <= subCharArray.Length-1; j++)
                    {
                        curStr.Add(subCharArray[j]);
                    }
                    commonSuffix.Add(new string(curStr.ToArray()));
                    curStr.Clear();
                }

                for (int h = commonPrefix.Count - 1; h >= 0; h--)
                {
                    if (commonPrefix[h] == commonSuffix[h])
                    {
                        next[i] = commonPrefix[h].Length;
                        break;
                    }
                }
                
            }



            return next;
        }

        /// <summary>
        /// Reverse string using two pointers, one starts from beginning, one starts from the end, and they meet in the middle
        /// </summary>
        /// <param name="s"></param>
        public static void ReverseString(char[] s)
        {
            int startP = 0;
            int endP = s.Length - 1;
            while (startP < endP)
            {
                char temp = s[startP];
                s[startP] = s[endP];
                s[endP] = temp;

                startP++;
                endP--;
            }
        }
        
        /// <summary>
        /// 数组拆分 I
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int ArrayPairSum(int[] nums)
        {
            // 排序法
            /*Array.Sort(nums);
            int sum = 0;
            for (int i = 1; i < nums.Length; i+=2)
            {
                sum += Math.Min(nums[i],nums[i - 1]);
            }

            return sum;*/
            
            // 暴力法
            List<List<int>> permutations = new List<List<int>>();
            FindOnePermutation(nums);
            foreach (List<int> permutation in permutations)
            { 
                for (int i = 0; i < permutation.Count; i++)
            {
                Console.Write(permutation[i]+" ");
            }
            
            Console.WriteLine();
            }

            return -1;
        }

        private static void FindOnePermutation(int[] list)
        {
            GetPer(list, 0, list.Length-1);
        }
        
        private static void Swap(ref int a, ref int b)
        {
            if (a == b) return;

            var temp = a;
            a = b;
            b = temp;
        }

        private static void GetPer(int[] list, int k, int m)
        {
            if (k == m)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    Console.Write(list[i]+" ");
                }

                Console.WriteLine();
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }

        public static int FindMaxConsecutiveOnes(int[] nums)
        {
            int fast = 0;
            int count = 0;
            int max = 0;
            while (fast < nums.Length)
            {
                if (nums[fast] == 1)
                {
                    count++;
                }
                else
                {
                    if (count > max)
                    {
                        max = count;
                    }
                    count = 0;
                }

                fast++;
            }
            if (count > max)
            {
                max = count;
            }

            return max;

        }

        /*
         * 1. 暴力法 遍历所有可能性
         * 2. 双指针 从头开始
         * 3. 计算从[0,1] 到 [0,n]的元素和数组，用二分法计算
         * https://leetcode-cn.com/problems/minimum-size-subarray-sum/solution/chang-du-zui-xiao-de-zi-shu-zu-by-leetcode-solutio/
         */
        public static int MinSubArrayLen(int s, int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            int ans = int.MaxValue;
            int[] sums = new int[nums.Length + 1];

            for (int i = 1; i <= nums.Length; i++)
            {
                sums[i] = sums[i - 1] + nums[i - 1];
            }

            for (int i = 1; i <= nums.Length; i++)
            {
                int target = s + sums[i - 1];
                int bound = LowerBound(sums, i, nums.Length, target);
                if (bound != -1)
                {
                    ans = Math.Min(ans, bound - i - 1);
                }
            }

            return ans == int.MaxValue ? 0 : ans;
        }

        private static int LowerBound(int[] sums, int l, int r, int target)
        {
            int mid = -1, originL = l, originR = r;
            while (l < r)
            {
                mid = (l + r) >> 1;
                if (sums[mid] < target)
                {
                    l = mid + 1;
                }
                else
                {
                    r = mid;
                }
            }

            return sums[l] >= target ? l : -1;
        }

        public static int MinSubArrayLen2(int s, int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int startP = 0;
            int endP = 0;
            int sum = 0;
            int minLen = int.MaxValue;
            while (endP < nums.Length)
            {
                sum += nums[endP];
                endP++;
                    
                if (sum >= s)
                {
                    while (sum - nums[startP] >= s)
                    {
                        sum -= nums[startP];
                        startP++;
                    }
                    minLen = Math.Min(minLen, endP - startP);

                }
                
            }

            while (sum - nums[startP] >= s)
            {
                sum -= nums[startP];
                startP++;
                minLen = Math.Min(nums[endP]-1 - startP,minLen);
            }


            return minLen == int.MaxValue?0:minLen;
        }

        public static List<int> GetRow(int rowIndex)
        {
            List<int> kRows = new List<int>(rowIndex + 1);
            for (int i = 0; i <= rowIndex; i++)
            {
                kRows[i] = 1;
                for (int j = i - 1; j > 1; j--)
                {
                    kRows[j - 1] = kRows[j - 2] + kRows[j - 1];
                }
            }

            return kRows;
        }
        /*
         * 输入："Let's take LeetCode contest"
         * 输出："s'teL ekat edoCteeL tsetnoc"
         */
        public static string ReverseWords2(string s)
        {
            var stringArray = s.Split(' ');
            for (int i = 0; i < stringArray.Length; i++)
            {
                
                var charArray = stringArray[i].ToCharArray();
                
                

                
                for (int j = 0; j < charArray.Length/2; j++)
                {
                    var temp = charArray[j];
                    charArray[j] = charArray[charArray.Length - 1 - j];
                    charArray[charArray.Length - 1 - j] = temp;
                }
                

               

                var sb = new StringBuilder();
                
                for (int j = 0; j < charArray.Length; j++)
                {
                    sb.Append(charArray[j]);
                }

                stringArray[i] = sb.ToString();
            }
            


            var result = string.Join(" ", stringArray);
            return result;
        }
        
        public static int FindMin(int[] nums)
        {
            int firstElement = nums[0];
            return FindMinBinary(nums, 1, nums.Length - 1, firstElement);
        }

        private static int FindMinBinary(int[] nums, int left, int right, int target )
        {
            // 如果如果第一位就是最大 或者数组干脆就只有一个元素
            if (nums[nums.Length - 1] > nums[0] || nums.Length == 1)
            {
                // 直接返回第一位元素
                return nums[0];
            }
            if (left > right)
                return right;
            int mid = (left + right) / 2;
            // 检查该位置是否是特殊的位置
            if (nums[mid - 1] > nums[mid])
            {
                return nums[mid];
            }
            return nums[mid] > target ? FindMinBinary(nums, mid + 1, right, target) : FindMinBinary(nums, left, mid - 1, target);
        }
        
        public static int MaxProfit(int[] prices)
        {
            int profit = 0;

            int i = 0;
            while(i<prices.Length-1)
            {
                while (i<prices.Length - 1 && prices[i + 1] <= prices[i])
                {
                    i++;
                }

                var valleyIndex = i;

                while (i<prices.Length-1 && prices[i + 1] > prices[i])
                {
                    i++;
                }

                var peakIndex = i;

                profit += prices[peakIndex] - prices[valleyIndex];
            }

            return profit;
        }
        /// <summary>
        /// 旋转数组
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        public static void Rotate1(int[] nums, int k)
        {
            if (k < 0)
            {
                k = -k;
                k = nums.Length-k % nums.Length;
            }
            int[] result = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                var newPos = (i + k) % nums.Length;
                result[newPos] = nums[i];
            }

            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = result[i];
            }

            foreach (int num in nums)
            {
                Console.Write(num+" ");
            }

            Console.WriteLine();
        }

        public static bool ContainsDuplicate(int[] nums){
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (set.Contains(nums[i]))
                    return true;
                set.Add(nums[i]);
            }

            return false;
        }

        public static int[] Intersect(int[] nums1, int[] nums2)
        {
            Array.Sort(nums1);
            Array.Sort(nums2);
            
            int i = 0;
            int j = 0;
            List<int> intersect = new List<int>();
            while (i < nums1.Length && j < nums2.Length)
            {
                if (nums1[i] < nums2[j])
                {
                    i++;
                }
                else if (nums1[i] > nums2[j])
                {
                    j++;
                }
                else
                {
                    intersect.Add(nums1[i]);
                    i++;
                    j++;
                }
                
            }

            return intersect.ToArray();

        } 
        
        public static bool IsValidSudoku(char[][] board) {
            var duplicatedSet = new HashSet<char>();
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (duplicatedSet.Contains(board[i][j]))
                    {
                        Console.WriteLine("?");
                        return false;
                    }

                    if (board[i][j] != '.')
                    {
                        duplicatedSet.Add(board[i][j]);
                        
                    }
                }
                duplicatedSet.Clear();

            }

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (duplicatedSet.Contains(board[j][i]))
                    {
                        Console.WriteLine("??");
                        return false;
                    }

                    if (board[j][i] != '.')
                    {
                        duplicatedSet.Add(board[j][i]);
                        
                    }
                }
                duplicatedSet.Clear();
            }

            for (int i = 0; i < 9; i+=3)
            {
                for (int j = 0; j < 9; j+=3)
                {
                    
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            if (duplicatedSet.Contains(board[i + k][j + l]))
                            {
                                Console.WriteLine("???");
                                return false;
                            }

                            if (board[i + k][j+l] != '.')
                            {
                                duplicatedSet.Add(board[i + k][j + l]);
                            }
                        }
                    }
                    
                    duplicatedSet.Clear();
                }
            }

            return true;
        }

        public static int FirstUniqChar(string s)
        {
            Dictionary<char,int> letterIndexDic = new Dictionary<char, int>();
            HashSet<char> outSet = new HashSet<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!letterIndexDic.ContainsKey(s[i]))
                {
                    letterIndexDic.Add(s[i], i);
                }
                else
                {
                    outSet.Add(s[i]);
                }
            }

            foreach (char c in outSet)
            {
                letterIndexDic.Remove(c);
            }

            if (letterIndexDic.Count == 0)
            {
                return -1;
            }

            int minIndex = int.MaxValue;
            char targetChar = '_';
            foreach (KeyValuePair<char,int> pair in letterIndexDic)
            {
                if (pair.Value < minIndex)
                {
                    minIndex = pair.Value;
                    targetChar = pair.Key;
                }
            }

            return minIndex;
        }

        public static bool IsAnagram(string s, string t)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (dic.ContainsKey(s[i]))
                {
                    dic[s[i]]++;
                }
                else
                {
                    dic.Add(s[i], 1);
                }
            }

            for (int i = 0; i < t.Length; i++)
            {
                if (dic.ContainsKey(t[i]))
                {
                    dic[t[i]]--;
                    if (dic[t[i]] == 0)
                    {
                        dic.Remove(t[i]);
                    }
                }
                else
                {
                    return false;
                }
            }

            return dic.Count == 0;
        }
         
        public static int MyAtoi(string str)
        {

            bool hasFoundSign = false;
            int negativeSignNum = 0;
            bool hasFoundNumber = false;
            bool hasFound1To9 = false;
            if (str.Length == 0)
            {
                return 0;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '+' || str[i] == '-')
                {
                    if (hasFoundNumber || hasFoundSign)
                    {
                        return StringToInt(sb.ToString(), negativeSignNum % 2 == 1);
                    }
                    else
                    {
                        hasFoundSign = true;
                        if (str[i] == '-')
                        {
                            negativeSignNum++;
                        }
                    }
                }
                else if (str[i] == '-')
                {
                    Console.WriteLine(i);
                    if (hasFoundSign == true)
                    {
                        return StringToInt(sb.ToString(), negativeSignNum % 2 == 1);
                    }
                    else if (hasFoundNumber == true)
                    {
                        return StringToInt(sb.ToString(), negativeSignNum % 2 == 1);
                    }
                    else
                    {
                        hasFoundSign = true;
                        sb.Append(str[i]);
                    }
                }
                else if (str[i] >= '0' && str[i] <= '9')
                {
                    if (str[i] >= '1' && str[i] <= '9')
                    {
                        hasFound1To9 = true;
                        sb.Append(str[i]);
                    }
                    if (str[i] == '0')
                    {
                        if (!hasFoundNumber)
                        {
                            hasFoundNumber = true;
                            continue;
                        }

                        if (hasFound1To9)
                        {
                            sb.Append(str[i]);

                        }
                    }
                    hasFoundNumber = true;
                }
                else if (str[i] == ' ')
                {
                    if (hasFoundSign)
                    {
                        return StringToInt(sb.ToString(), negativeSignNum % 2 == 1);
                    }
                    if (hasFoundNumber)
                    {
                        return StringToInt(sb.ToString(), negativeSignNum % 2 == 1);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (hasFoundNumber)
                    {
                        return StringToInt(sb.ToString(), negativeSignNum % 2 == 1);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            return StringToInt(sb.ToString(), negativeSignNum%2 == 1);
        }


        public static int StringToInt(string str, bool isNegative)
        {
            if (str.Length == 0)
                return 0;
            if (str.Length >= 11)
            {
                if (isNegative)
                {
                    return int.MinValue;
                }
                else
                {
                    return int.MaxValue;
                }
            }
            int time = 0;
            long result = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (isNegative)
                {
                    if (int.MinValue  > result - (str[i] - '0') * (long)Math.Pow(10, time))
                    {
                        return int.MinValue;
                    }
                    
                    result -= (str[i] - '0') * (long)Math.Pow(10, time);
                    
                }
                else
                {
                    
                    if (int.MaxValue - (str[i] - '0') * (long)Math.Pow(10, time) < result)
                    {
                        return int.MaxValue;
                    }
                    result += (str[i] - '0') * (long)Math.Pow(10, time);

                }

                time++;
            }

            if (result > int.MaxValue || result < int.MinValue)
            {
                return int.MinValue;
            }
            else
            {
                return (int)result;
            }

        }

        public static string CountAndSay(int n)
        {
            if (n == 1)
            {
                return "1";
            }

            return Scan(CountAndSay(n - 1));
        }

        public static string Scan(string str)
        {
            Console.WriteLine($"Str: "+str);
            StringBuilder sb = new StringBuilder();
            int count = 1;
            int currentNum = str[0]-'0';
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i]-'0' == currentNum)
                {
                    count++;
                    Console.WriteLine("Count = "+count);
                }
                else
                {
                        sb.Append(count);
                        sb.Append(currentNum);

                    currentNum = str[i]-'0';
                    count = 1;
                }
            }
            sb.Append(count);
            sb.Append(currentNum);
            return sb.ToString();
        }
    }
}