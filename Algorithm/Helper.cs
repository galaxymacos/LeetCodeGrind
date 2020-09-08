using System;

namespace Algorithm
{
    public class Helper
    {
        public void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length-1);
        }

        private void Sort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                int m = (l + r) / 2;
                Sort(arr, l, m);
                Sort(arr, m+1, r);
                Merge(arr, l, m, r);
            }
        }

        private void Merge(int[] arr, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;
            int[] L = new int[n1];
            int[] R = new int[n2];

            for (int index = 0; index < n1; index++)
            {
                L[index] = arr[l+index];
            }

            for (int index2 = 0; index2 < n2; index2++)
            {
                R[index2] = arr[m + 1 + index2];
            }

            int i = 0, j = 0;

            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k++] = L[i++];
                }
                else
                {
                    arr[k++] = R[j++];
                }
            }

            while (i < n1)
            {
                arr[k++] = L[i++];
            }

            while (j < n2)
            {
                arr[j++] = R[j++];
            }

        }

        public static int BinarySearch(int[] nums, int left, int right, int target)
        {
            if (left > right)
            {
                return -1;
            }

            int center = (left + right) / 2;
            if (nums[center] == target)
            {
                return center;
            }

            if (target > nums[center])
            {
                return BinarySearch(nums, center+1, right, target);
            }

            return BinarySearch(nums, left, center - 1, target);
        }
    }
}