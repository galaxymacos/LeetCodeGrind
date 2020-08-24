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


            int[][] array2D =
            {
                new[] {3},
                new[] {2}
            };
            
            

            // Console.WriteLine(StringAndArray.AreSpaceEqual(new []{0,2,2}, new[] {0,2}));
            var result = StringAndArray.DuiJiaoXianbianLi(array2D);
            

        }
    }
}