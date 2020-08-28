using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Algorithm
{
    public class BinaryTree
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int x)
            {
                val = x;
            }
        }
        
        public bool IsValidBST(TreeNode root)
        {
            return Recur(root, long.MinValue, long.MaxValue);



        }

        public bool Recur(TreeNode root, long min, long max)
        {
            bool leftSatisfied = false;
            bool rightSatisfied = false;
            if (root == null)
            {
                return true;
            }

            if (root.val <= min || root.val >= max)
            {
                Console.WriteLine(root);
                return false;
            }
            if (root.left != null)
            {
                leftSatisfied =  Recur(root.left, min, Math.Min(max, root.val));
            }
            else
            {
                leftSatisfied = true;
            }

            if (root.right != null)
            {
                rightSatisfied = Recur(root.right, Math.Max(min, root.val), max);
            }
            else
            {
                rightSatisfied = true;
            }

            return leftSatisfied && rightSatisfied;

        }
        
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            if (root == null)
            {
                return new List<IList<int>>();
            }
            IList<IList<int>> result = new List<IList<int>>();
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                IList<TreeNode> nodesCurrentLevel = new List<TreeNode>();
                while (queue.Count > 0)
                {
                    nodesCurrentLevel.Add(queue.Dequeue());
                }

                IList<int> currentLevelNum = new List<int>();
                foreach (TreeNode treeNode in nodesCurrentLevel)
                {
                    currentLevelNum.Add(treeNode.val);
                    if (treeNode.left != null)
                    {
                        queue.Enqueue(treeNode.left);
                    }

                    if (treeNode.right != null)
                    {
                        queue.Enqueue(treeNode.right);
                    }
                }
                result.Add(currentLevelNum);
            }
            return result;
        }
    }
}