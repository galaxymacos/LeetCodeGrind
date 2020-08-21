using System;

namespace Algorithm
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // int[,] testMatrix =
            // {
            //     {1, 4, 7, 11, 15},
            //     {2, 5, 8, 12, 19},
            //     {3, 6, 9, 16, 27},
            //     {10, 13, 14, 17, 24},
            //     {18, 21, 23, 26, 30}
            // };
            //
            // bool hasFound = JianzhiOffer.SearchMatrix(testMatrix, 9);
            // Console.WriteLine(hasFound);

            string resultString = JianzhiOffer.ReplaceSpace("We are happy.");
            
        }
    }

    public static class JianzhiOffer
    {
        public static bool SearchMatrix(int[,] matrix, int target)
        {
            #region First Solution ------- Find the top right corner
            
            /*int rowIndex = 0;
            int colIndex = matrix.GetLength(1) - 1;

            do
            {
                if (target == matrix[rowIndex, colIndex])
                {
                    return true;
                }
                else if (target > matrix[rowIndex, colIndex])
                {
                    rowIndex++;
                    if (rowIndex >= matrix.GetLength(0))
                    {
                        return false;
                    }
                }
                else if (target < matrix[rowIndex, colIndex])
                {
                    colIndex--;
                    if (colIndex < 0)
                    {
                        return false;
                    }
                }
            } while (true);*/
            #endregion

            #region Second Solution ----- Find the bottom left corner

            int rowIndex = matrix.GetLength(0)-1;
            int colIndex = 0;
            do
            {
                if (matrix[rowIndex, colIndex] == target)
                    return true;
                else if (target < matrix[rowIndex, colIndex])
                {
                    rowIndex--;
                    if (rowIndex < 0)
                        return false;
                }
                else if (target > matrix[rowIndex, colIndex])
                {
                    colIndex++;
                    if (colIndex >= matrix.GetLength(1))
                    {
                        return false;
                    }
                }
            } while (true);
            

            #endregion

            

            
            return false;
        }

        public static string ReplaceSpace(string s)
        {
            Console.WriteLine($"Original Length: {s.Length}");
            int count = 0;
            foreach (char c in s)
            {
                if (c == ' ')
                {
                    count++;
                }
            }

            char[] resultArr = new char[s.Length + count*3];
            int j = resultArr.Length - 1;
            
            for (int i = s.Length- 1; i >= 0; i--)
            {
                if (s[i] == ' ')
                {
                    resultArr[j--] = '0';
                    resultArr[j--] = '2';
                    resultArr[j--] = '%';
                }
                else
                {
                    Console.WriteLine($"i = {i}, j = {j}");
                    resultArr[j--] = s[i];
                }
                
                
            }
            Console.WriteLine($"Length: {resultArr.Length}");
            Console.WriteLine($"Result string is: {string.Join("", resultArr)}");
            return resultArr.ToString();
        }
    }
}