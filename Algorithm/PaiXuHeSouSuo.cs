using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm
{
    public class PaiXuHeSouSuo
    {
        public void SortColors(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add(0, 0);
            dic.Add(1, 0);
            dic.Add(2, 0);
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], 1);
                }
                else
                {
                    dic[nums[i]]++;
                }
            }

            int count = 0;
            foreach (var keyValue in dic)
            {
                for (int i = 0; i < keyValue.Value; i++)
                {
                    nums[count++] = keyValue.Key;
                }
            }
        }

        public int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach (var num in nums)
            {
                if (map.ContainsKey(num))
                {
                    map[num]++;
                }
                else
                {
                    map.Add(num, 1);
                }
            }


            Heap<IndexClass> pq = new Heap<IndexClass>(k, false);
            foreach (int key in map.Keys)
            {
                if (pq.Count < k)
                {
                    Console.WriteLine($"Insert {new IndexClass(key, map)}");
                    pq.Insert(new IndexClass(key, map));
                }
                else if (map[key] > map[pq.Peek().index])
                {
                    var itemtoremove = pq.Remove();
                    Console.WriteLine($"remove {itemtoremove}");
                    Console.WriteLine($"Then insert {new IndexClass(key, map)}");

                    pq.Insert(new IndexClass(key, map));
                }
            }

            List<int> res = new List<int>();
            while (pq.Count > 0)
            {
                res.Add(pq.Remove().index);
            }

            return res.ToArray().Reverse().ToArray();
        }

        public int FindKthLargest(int[] nums, int k)
        {
            Heap<int> heap = new Heap<int>(15, true);
            foreach (var num in nums)
            {
                heap.Insert(num);
            }

            for (int i = 0; i < k - 1; i++)
            {
                heap.Remove();
            }

            return heap.Remove();
        }

        public int FindPeakElement(int[] nums)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i + 1] < nums[i])
                {
                    return i;
                }
            }

            return nums.Length - 1;
        }

        public int[] SearchRange(int[] nums, int target)
        {
            int[] result = new int[2] {-1, -1};
            SearchRangeHelper(nums, target, 0, nums.Length - 1, result);
            if (result[1] < result[0])
            {
                result[1] = result[0];
            }
            return result;
        }

        private void SearchRangeHelper(int[] nums, int target, int left, int right, int[] result)
        {
            bool shouldEnd = true;
            foreach (int i in result)
            {
                if (i == -1)
                {
                    shouldEnd = false;
                }
            }

            if (shouldEnd)
            {
                return;
            }
            if (left > right)
            {
                return;
            }


            int center = (left + right) / 2;
            // is left target, only search the right side
            Console.WriteLine(center);
            if (nums[center] == target && (center - 1 < 0 || nums[center - 1] != target))
            {
                result[0] = center;
                SearchRangeHelper(nums, target, center + 1, right, result);
                return;
            }

            // is right target, only search the left side
            if (nums[center] == target && (center + 1 >= nums.Length || nums[center + 1] != target))
            {
                result[1] = center;
                SearchRangeHelper(nums, target, left, center-1, result);
                return;
            }

            if (nums[center] == target)
            {
                SearchRangeHelper(nums, target, left, center-1, result);
                SearchRangeHelper(nums, target, center+1, right, result);
                return;
            }
            
            if (nums[center] > target)
            {
                SearchRangeHelper(nums, target, left, center-1, result);
                return;


            }

            if (nums[center] < target)
            {
                SearchRangeHelper(nums, target, center + 1, right, result);
                return;

            }
            

        }

        #region 搜索旋转排序数组

        public int Search(int[] nums, int target)
        {
            int turnPoint = SearchHelper(nums, nums[0], 0, nums.Length - 1);

            return SearchHelper2(nums, turnPoint, target);
        }

        private int SearchHelper(int[] nums, int min, int left, int right)
        {
            if (left > right)
            {
                return -1;
            }
            int center = (left + right) / 2;
            if (nums[center] < min && nums[center - 1] >= min)
            {
                return center;
            }

            if (nums[center] >= min)
            {
                return SearchHelper(nums, min, center + 1, right);
            }

            return SearchHelper(nums, min, left, center - 1);

        }

        private int SearchHelper2(int[] nums, int turnPoint, int target)
        {
            if (turnPoint == -1)
            {
                return Helper.BinarySearch(nums, 0, nums.Length - 1, target);
            }
            if (target >= nums[0])
            {
                return Helper.BinarySearch(nums, 0, turnPoint-1, target);
            }
            else
            {
                return Helper.BinarySearch(nums,turnPoint, nums.Length - 1, target);
            }
        }
        
        

        #endregion
    }
}