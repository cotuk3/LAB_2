using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyBinaryTree
{
    public class BinaryTree<T> : IEnumerable<T>, ICollection<T> where T: class, IComparable<T>
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
            if(root == null)
                root = new BinaryTreeNode<T>(item);
            else
                add(root, item);

            count++;
        }
        void add(BinaryTreeNode<T> root, T value)
        {
            BinaryTreeNode<T> current = new BinaryTreeNode<T>(value);
            int res = current.CompareNode(root);
            if (res < 0)
            {
                if (root.Left != null)
                    add(root.Left, value);
                else
                    root.Left = current;
            }
            else if(res > 0)
            {
                if (root.Right != null)
                    add(root.Right, value);
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
            return PostOrderTraversal().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        #endregion

        #region Post Order Traversal
        public List<T> PostOrderTraversal()
        {
            List<T> list = new List<T>();
            return PostOrderTraversal(root,ref list);
        }
        List<T> PostOrderTraversal(BinaryTreeNode<T> root,ref List<T> list)
        {
            if (root == null)
                throw new Exception("Tree is empty");

            if(root.Left != null)
                PostOrderTraversal(root.Left,ref list);

            if (root.Right != null)
                PostOrderTraversal(root.Right, ref list);

            list.Add(root.Value);

            return list;
        }

        #endregion

    }
}
