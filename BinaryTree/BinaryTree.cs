using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        
        #region ICollection<T>
        public int Count => count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if(root == null)
                root = new BinaryTreeNode<T>(item);
            else
                addto(root, item);

            count++;
        }
        void addto(BinaryTreeNode<T> root, T value)
        {
            if (value.CompareTo(root.Value) < 0)
            {
                if (root.Left != null)
                    addto(root.Left, value);
                else
                    root.Left = new BinaryTreeNode<T>(value);
            }
            else
            {
                if (root.Right != null)
                    addto(root.Right, value);
                else
                    root.Right = new BinaryTreeNode<T>(value);
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
        (BinaryTreeNode<T> node, BinaryTreeNode<T> root) FindNodeWithParent(T Value)
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
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            if (Contains(item))
            {
                count--;
                BinaryTreeNode<T> node = FindNodeWithParent(item).node;
                BinaryTreeNode<T> parent = FindNodeWithParent(item).root;

                if (node.Right == null)
                {
                    if (node == root)
                        root = node.Left;
                    else
                    {
                        
                    }
                }
            }
            return false;
        }
        #endregion

        #region IEnumerator<T>
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Post Order Traversal
        public List<T> PostOrderTraversal()
        {
            return PostOrderTraversal(root);
        }
        public List<T> PostOrderTraversal(BinaryTreeNode<T> root)
        {

            return null;
        }

        #endregion

    }
}
