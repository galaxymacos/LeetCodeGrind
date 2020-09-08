using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

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

        public IList<int> InorderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode cur = root;
            while (cur != null || stack.Count>0)
            {
                while (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.left;
                }

                cur = stack.Pop();
                result.Add(cur.val);
                cur = cur.right;
            }
            return result;

        }
        
        // 二叉树的锯齿形层次遍历
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            
            IList<IList<int>> result = new List<IList<int>>();
            if (root == null)
            {
                return result;
            }
            bool movingRight = true;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                List<TreeNode> nodeInCurLevel = new List<TreeNode>();
                
                while (queue.Count > 0)
                {
                    TreeNode curNode = queue.Dequeue();
                    nodeInCurLevel.Add(curNode);
                    
                }
                if (movingRight)
                {
                    result.Add(nodeInCurLevel.Select(a => a.val).ToList());
                    movingRight = false;
                }
                else
                {
                    result.Add(nodeInCurLevel.Select(a=>a.val).Reverse().ToList());
                    movingRight = true;
                }

                for (int i = 0; i < nodeInCurLevel.Count; i++)
                {
                    
                    if (nodeInCurLevel[i].left != null)
                    {
                        queue.Enqueue(nodeInCurLevel[i].left);
                    }

                    if (nodeInCurLevel[i].right != null)
                    {
                        queue.Enqueue(nodeInCurLevel[i].right);
                    }
                }
            }

            return result;
        }

        private Dictionary<int, int> map;

        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            int n = preorder.Length;
            map = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                map.Add(inorder[i],i);
            }

            return MyBuildTree(preorder, inorder, 0, n - 1, 0, n - 1);
        }
        
        public TreeNode MyBuildTree(int[] preorder, int[] inorder, int preorder_left, int preorder_right,
            int inorder_left, int inorder_right)
        {
            
            if (preorder_left > preorder_right) {
                return null;
            }
            int preorder_root = preorder_left;
            int inorder_root = map[preorder[preorder_root]];
            
            TreeNode root = new TreeNode(preorder[preorder_root]);
            int size_left_subtree = inorder_root - inorder_left;
            root.left = MyBuildTree(preorder, inorder, preorder_left + 1, preorder_left + size_left_subtree,
                inorder_left, inorder_root - 1);
            root.right = MyBuildTree(preorder, inorder, preorder_left + size_left_subtree + 1, preorder_right,
                inorder_root + 1, inorder_right);
            return root;
        }

        #region 填充每个节点的下一个右侧节点指针

        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node next;
        }
        public Node Connect(Node root) {
            if (root == null)
            {
                return null;
            }
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count>0)
            {
                var nodeCurLvl = new List<Node>();
                while (queue.Count > 0)
                {
                    nodeCurLvl.Add(queue.Dequeue());
                }

                for (int i = 0; i < nodeCurLvl.Count; i++)
                {
                    nodeCurLvl[i].next = i + 1 < nodeCurLvl.Count ? nodeCurLvl[i + 1] : null;
                    if (nodeCurLvl[i].left != null)
                    {
                        queue.Enqueue(nodeCurLvl[i].left);
                    }

                    if (nodeCurLvl[i].right != null)
                    {
                        queue.Enqueue(nodeCurLvl[i].right);
                    }
                }
            }

            return root;
        }

        #endregion

        #region 二叉搜索树中第k小的元素

        public int KthSmallest(TreeNode root, int k)
        {
            for (int i = 0; i < k; i++)
            {
                TreeNode smallest = root;
                TreeNode smallestParent = root;
                while (smallest.left != null)
                {
                    smallestParent = smallest;
                    smallest = smallest.left;
                }

                if (i == k - 1)
                {
                    return smallest.val;
                }
                smallestParent.left = smallest.right;
                
            }

            return -1;
        }

        #endregion

        #region 岛屿数量

        private HashSet<(int, int)> landPoses;
        
        /// <summary>
        /// 给你一个由'1'（陆地）和 '0'（水）组成的的二维网格，请你计算网格中岛屿的数量。
        /// 岛屿总是被水包围，并且每座岛屿只能由水平方向或竖直方向上相邻的陆地连接形成。
        ///
        /// 此外，你可以假设该网格的四条边均被水包围。
        ///
        /// 作者：力扣 (LeetCode)
        /// 链接：https://leetcode-cn.com/leetbook/read/top-interview-questions-medium/xvtsnm/
        /// 来源：力扣（LeetCode）
        /// 著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int NumIslands(char[][] grid)
        {
            int landNum = 0;
            landPoses = new HashSet<(int, int)>();
            
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        landPoses.Add((i, j));
                    }
                }
            }
            
            while (landPoses.Count > 0)
            {
                foreach ((int, int) origin in landPoses)
                {
                    RemoveLandByOrigin(grid, origin);
                    landNum++;
                    break;
                }
                
            }

            return landNum;
        }
 
        private void RemoveLandByOrigin(char[][] grid, (int, int) origin)
        {
            landPoses.Remove(origin);
            // Check Left
            if (origin.Item2 - 1 >= 0)
            {
                if (landPoses.Contains((origin.Item1, origin.Item2-1)))
                {
                    RemoveLandByOrigin(grid, (origin.Item1, origin.Item2-1));
                }
            }

            // Check Right
            if (origin.Item2 + 1 < grid[0].Length)
            {
                if (landPoses.Contains((origin.Item1, origin.Item2+1)))
                {
                    RemoveLandByOrigin(grid, (origin.Item1, origin.Item2+1));
                }
            }
            
            // Check Top
            if (origin.Item1 - 1 >= 0)
            {
                if (landPoses.Contains((origin.Item1-1, origin.Item2)))
                {
                    RemoveLandByOrigin(grid, (origin.Item1-1, origin.Item2));
                }
            }

            // Check Bottom
            if (origin.Item1 + 1 < grid.Length)
            {
                if (landPoses.Contains((origin.Item1+1, origin.Item2)))
                {
                    RemoveLandByOrigin(grid, (origin.Item1+1, origin.Item2));
                }
            }

        }



        #endregion
    }
}