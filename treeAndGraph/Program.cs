using System;
using System.Collections;
using System.Collections.Generic;

namespace treeAndGraph
{
    class Program
    {
        static void Main1(string[] args)
        {
            var tree = new Tree(25);
            tree.Add(15);
            tree.Add(50);
            tree.Add(10);
            tree.Add(22);
            tree.Add(35);
            tree.Add(70);
            tree.Add(4);
            tree.Add(12);
            tree.Add(18);
            tree.Add(24);
            tree.Add(31);
            tree.Add(44);
            tree.Add(66);
            tree.Add(90);

           
           tree.printInorder();
           Console.WriteLine();
           tree.printPreorder();
           Console.WriteLine();
           tree.printPostorder();
           Console.WriteLine();
           tree.printInBreadthFirst();  
           Console.WriteLine();
           tree.pringInBreadthRec();
           Console.WriteLine();
           tree.printInDepthFirst();
        }
    }

    class TreeNode
    {
        public int value;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int value, TreeNode left, TreeNode right)
        {
            this.value = value;
            this.left = left;
            this.right = right;
        }

        public TreeNode(int value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
        }
    }

    class Tree: IComparable
    {
        TreeNode root;
        public Tree(int value)
        {
            root = new TreeNode(value);
        }

        public void addLeft(int value)
        {
            var current = root;
            while (current.left != null)
            {
                current = current.left;
            }
            current.left = new TreeNode(value);
        }

        private TreeNode Add(TreeNode current, int value)
        {
            if (current == null)
            {
                return new TreeNode(value);
            }

            if (value < current.value)
            {
                current.left = Add(current.left, value);
            }
            else
            {
                current.right = Add(current.right, value);
            }

            return current;
        }

        private bool contains(TreeNode current, int value)
        {
            if (current == null)
            {
                return false;
            }

            if (value == current.value)
            {
                return true;
            }

            return value < current.value
                ? contains(current.left, value)
                : contains(current.right, value);

        }

        public bool contains(int value)
        {
            return contains(root, value);
        }

        public TreeNode Add(int value)
        {
            return Add(root, value);
        }

        private TreeNode delete(TreeNode current, int value)
        {
            if (current == null)
            {
                return null;
            }

            if (value == current.value)
            {
                if (current.left == null && current.right == null)
                {
                    return null;
                }
                if (current.left == null)
                {
                    return current.right;
                }
                if (current.right == null)
                {
                    return current.left;
                }
                int element = smallest(current.right);
                current.value = element;
                current.right = delete(current.right, element);
                return current;
            }

            

            if (value<current.value)
            {
                current.left = delete(current.left, value);
                return current;
            }

            current.right = delete(current.right, value);
            return current;
        }

        public TreeNode delete(int value)
        {
            return delete(root, value);
        }

        private int smallest(TreeNode node)
        {
            return node.left == null ? node.value : smallest(node.left);
        }

        private void printInorder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            printInorder(node.left);
            Console.Write(node.value + " ");
            printInorder(node.right);
           
        }
        private void printPreorder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            Console.Write(node.value + " ");
            printPreorder(node.left);
            printPreorder(node.right);
           
        }
        private void printPostorder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            printPostorder(node.left);
            printPostorder(node.right);
            Console.Write(node.value + " ");

        }

        public void printInorder()
        {
            printInorder(this.root);
        }
        public void printPreorder()
        {
            printPreorder(this.root);
        }
        public void printPostorder()
        {
            printPostorder(this.root);
        }

        public void printInBreadthFirst()
        {
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var temp = queue.Dequeue();
                Console.Write(temp.value+" ");
                if (temp.left != null)
                {
                    queue.Enqueue(temp.left);
                }
                if (temp.right != null)
                {
                    queue.Enqueue(temp.right);
                }
            }
        }
        public void printInDepthFirst()
        {
            var queue = new Stack<TreeNode>();
            queue.Push(root);
            while (queue.Count != 0)
            {
                var temp = queue.Pop();
                Console.Write(temp.value+" ");
                if (temp.right != null)
                {
                    queue.Push(temp.right);
                }
                if (temp.left != null)
                {
                    queue.Push(temp.left);
                }
               
            }
        }
        
        public void pringInBreadthRec()
        {
            int h = height(root);
            for (int i = 0; i <= h; i++)
            {
                pringInBreadthRec(root, i);
            }
        }

        private int height(TreeNode treeNode)
        {
            if (treeNode == null)
            {
                return 0;
            }
            int lheight = height(treeNode.left);
            int rheight = height(treeNode.right);

            return lheight > rheight
                ? lheight + 1
                : rheight + 1;
        }

        public void pringInBreadthRec(TreeNode treeNode, int lvl)
        {
            if (root == null)
            {
                return;
            }

            if (lvl == 1)
            {
                Console.Write(treeNode.value + " ");
            }
            else if (lvl > 1)
            {
                pringInBreadthRec(treeNode.left, lvl-1);
                pringInBreadthRec(treeNode.right, lvl-1);
            }
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }
    }

    class Trees : IEnumerable<Tree>
    {

        public Tree[] trees;

        public void sort()
        {
        }

        public IEnumerator<Tree> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}