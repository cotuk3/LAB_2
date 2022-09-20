using System;
using System.Collections;
using System.Collections.Generic;

namespace MyBinaryTree
{
    public class BinaryTree<T> : IEnumerable<T>, ICollection<T> where T : class, IComparable<T>
    {
        BinaryTreeNode<T> root;
        int count;

        public BinaryTree()
        {
            root = null;
            count = 0;
        }
        public BinaryTree(IEnumerable storage)
        {
            foreach (T item in storage)
            {
                Add(item);
            }
        }

        #region ICollection<T>
        public int Count => count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            BinaryTreeNode<T> current = new BinaryTreeNode<T>(item);
            if (root == null)
                root = current;
            else
                add(root, current);

            count++;
        }
        void add(BinaryTreeNode<T> root, BinaryTreeNode<T> current)
        {

            int res = current.CompareNode(root);
            if (res < 0)
            {
                if (root.Left != null)
                    add(root.Left, current);
                else
                    root.Left = current;
            }
            else
            {
                if (root.Right != null)
                    add(root.Right, current);
                else
                    root.Right = current;
            }
        }

        public void Clear()
        {
            root = null;
            count = 0;
        }

        public bool Contains(T item)
        {
            return FindNodeWithParent(item).node != null;
        }
        (BinaryTreeNode<T> node, BinaryTreeNode<T> parent) FindNodeWithParent(T Value)
        {
            BinaryTreeNode<T> node = root;
            BinaryTreeNode<T> parent = null;

            while (node != null)
            {
                int res = Value.CompareTo(node.Value);
                if (res > 0)
                {
                    parent = node;
                    node = node.Right;
                }
                else if (res < 0)
                {
                    parent = node;
                    node = node.Left;
                }
                else
                    break;
            }
            return (node, parent);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = PostOrderTraversal()[i];
            }
        }

        public bool Remove(T item)
        {
            if (Contains(item))
            {
                count--;
                BinaryTreeNode<T> node = FindNodeWithParent(item).node;
                BinaryTreeNode<T> parent = FindNodeWithParent(item).parent;

                if (node.Right == null)
                {
                    if (node == root)
                        root = node.Left;
                    else
                    {
                        int res = node.CompareNode(parent);
                        if (res > 0)
                            parent.Right = node.Left;
                        else if (res < 0)
                            parent.Left = node.Left;
                    }

                }
                else if (node.Right.Left == null)
                {
                    node.Right.Left = node.Left;

                    if (node == root)
                        root = node.Right;
                    else
                    {
                        int res = node.CompareNode(parent);
                        if (res > 0)
                            parent.Right = node.Right;
                        else if (res < 0)
                            parent.Left = node.Right;
                    }
                }
                else
                {
                    BinaryTreeNode<T> mostLeft = node.Right.Left;
                    BinaryTreeNode<T> mostLeftParent = node.Right;

                    while (mostLeft != null)
                    {
                        mostLeftParent = mostLeft;
                        mostLeft = mostLeft.Left;
                    }

                    mostLeftParent.Left = mostLeft.Right;
                    mostLeft.Left = node.Left;
                    mostLeft.Right = node.Right;


                    if (node == root)
                        root = mostLeft;
                    else
                    {
                        int res = node.CompareNode(parent);

                        if (res > 0)
                            parent.Right = mostLeft;
                        else if (res < 0)
                            parent.Left = mostLeft;


                    }

                }
                return true;
            }
            return false;
        }
        #endregion

        #region IEnumerator<T>
        public IEnumerator<T> GetEnumerator()
        {
            //return PreOrderTraversal().GetEnumerator();
            return PostOrderTraversal().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Post Order Traversal (left right root)
        public List<T> PostOrderTraversal()
        {
            List<T> list = new List<T>();
            return PostOrderTraversal(root, ref list);
        }
        List<T> PostOrderTraversal(BinaryTreeNode<T> root, ref List<T> list)
        {
            if (root == null)
                throw new Exception("Tree is empty");

            if (root.Left != null)
                PostOrderTraversal(root.Left, ref list);

            if (root.Right != null)
                PostOrderTraversal(root.Right, ref list);

            list.Add(root.Value);

            return list;
        }
        #endregion 

        #region Pre Order Traversal (root left right)
        public List<T> PreOrderTraversal()
        {
            List<T> list = new List<T>();
            return PreOrderTraversal(root, ref list);
        }
        List<T> PreOrderTraversal(BinaryTreeNode<T> root, ref List<T> list)
        {
            if (root == null)
                throw new Exception("Tree is empty");

            list.Add(root.Value);

            if (root.Left != null)
                PreOrderTraversal(root.Left, ref list);

            if (root.Right != null)
                PreOrderTraversal(root.Right, ref list);

            return list;
        }
        #endregion 

        #region In Order Traversal (left root right)
        public List<T> InOrderTraversal()
        {
            List<T> list = new List<T>();
            return InOrderTraversal(root, ref list);
        }
        List<T> InOrderTraversal(BinaryTreeNode<T> root, ref List<T> list)
        {
            if (root == null)
                throw new Exception("Tree is empty");

            if (root.Left != null)
                InOrderTraversal(root.Left, ref list);

            list.Add(root.Value);

            if (root.Right != null)
                InOrderTraversal(root.Right, ref list);

            return list;
        }

        #endregion
    }
}
