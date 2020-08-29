using System;

namespace Algorithm
{
    public class Solution
    {

        private readonly int[] _nums;
        private readonly int[] _shuffle;
        private Random random = new Random();

        public Solution(int[] nums)
        {
            _nums = new int[nums.Length];
            _shuffle = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                _nums[i] = nums[i];
                _shuffle[i] = nums[i];
            }
        }
    
        /** Resets the array to its original configuration and return it. */
        public int[] Reset()
        {
            return _nums;
        }
    
        /** Returns a random shuffling of the array. */
        public int[] Shuffle()
        {
            for (int i = _nums.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i+1);
                int temp = _shuffle[i];
                _shuffle[i] = _shuffle[j];
                _shuffle[j] = temp;
            }

            // for (int i = 0; i < _nums.Length; i++)
            // {
                // var index = random.Next(_nums.Length);
                // int temp = _shuffle[i];
                // _shuffle[i] = _shuffle[index];
                // _shuffle[index] = temp;
            // }

            return _shuffle;
        }
    }
}