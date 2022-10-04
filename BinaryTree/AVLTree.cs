using System;
using System.Collections;
using System.Collections.Generic;

namespace MyTrees
{
    public class AVLTree<T> : IEnumerable<T>, ICollection<T> where T : IComparable<T> // TODO : finish testing and commit changes + merge 
    {
        public class AVLTreeNode : IComparable<T>
        {
            AVLTreeNode left;
            AVLTreeNode right;
            AVLTree<T> tree;
            int height = 0;

            public AVLTreeNode(T value, AVLTreeNode parent)
            {
                Value = value;
                Parent = parent;
            }
            public AVLTreeNode(T value, AVLTreeNode parent, AVLTree<T> tree)
            {
                Value = value;
                Parent = parent;
                this.tree = tree;
                MaxChildHeight(this);
            }

            #region Properties
            public T Value { get; set; }
            public AVLTreeNode Left
            {
                get => left;
                set
                {
                    left = value;
                    if (left != null)
                        left.Parent = this;
                }
            }
            public AVLTreeNode Right
            {
                get => right;
                set
                {
                    right = value;
                    if (right != null)
                        right.Parent = this;
                }
            }
            public AVLTreeNode Parent { get; set; }
            #endregion

            #region Compare
            public int CompareTo(T other)
            {
                return Value.CompareTo(other);
            }
            public int CompareNode(AVLTreeNode other)
            {
                return CompareTo(other.Value);
            }
            #endregion

            #region Balance
            enum Weight
            {
                Balance,
                LeftHeavily,
                RightHeavily
            }
            int MaxChildHeight(AVLTreeNode node)
            {
                if (node == null || node.Parent == null)
                {
                    height = 0;
                    return 0;
                }
                else
                {
                    height = 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
                    return height;
                }
            }
            int LeftHeight
            {
                get
                {
                    if (this.Left == null)
                        return 0;
                    return MaxChildHeight(this.Left);
                }

            }
            int RightHeight
            {
                get
                {
                    if (this.Right == null)
                        return 0;
                    return MaxChildHeight(this.Right);
                }

            }
            Weight State
            {
                get
                {
                    //if (LeftHeight == 0 && RightHeight != 0)
                    //    return Weight.RightHeavily;
                    //else if (RightHeight == 0 && LeftHeight != 0)
                    //    return Weight.LeftHeavily;

                    if (LeftHeight - RightHeight > 1)
                        return Weight.LeftHeavily;
                    else if (RightHeight - LeftHeight > 1)
                        return Weight.RightHeavily;
                    else
                        return Weight.Balance;
                }
            }

            int BalanceFactor()
            {
                return RightHeight - LeftHeight;
            }

            public void Balance()
            {
                if (this.State == Weight.RightHeavily)
                {
                    if (this.BalanceFactor() < 1)
                        this.RightLeftRotate();
                    else
                        LeftRotate();
                }
                else if (this.State == Weight.LeftHeavily)
                {
                    if (this.BalanceFactor() > 1)
                        this.LeftRightRotate();
                    else
                        this.RightRotate();
                }
                tree.root.MaxChildHeight(tree.root);

                calculateheight(tree.root);
            }
            void calculateheight(AVLTreeNode root)
            {
                if (root.Left != null)
                {
                    root.Left.MaxChildHeight(root.Left);
                    calculateheight(root.Left);
                }

                if (root.Right != null)
                {
                    root.Right.MaxChildHeight(root.Right);
                    calculateheight(root.Right);
                }
            }
            void ReplaceRoot(AVLTreeNode node)
            {
                if (this.Parent != null)
                {
                    if (this.Parent.Left == this)
                        this.Parent.Left = node;
                    else
                        this.Parent.Right = node;
                }
                else
                {
                    this.tree.root = node;
                }
                node.Parent = this.Parent;
                this.Parent = node;
            }
            void LeftRotate()
            {
                AVLTreeNode root = this.Right;
                ReplaceRoot(root);
                this.Right = root.Left;
                root.Left = this;
            }
            void RightRotate()
            {
                AVLTreeNode root = this.Left;
                ReplaceRoot(root);
                this.Left = root.Right;
                root.Right = this;
            }
            void RightLeftRotate()
            {
                this.Right.RightRotate();
                this.LeftRotate();
            }
            void LeftRightRotate()
            {
                this.Left.LeftRotate();
                this.RightRotate();
            }
            #endregion
        }

        AVLTreeNode root;

        public AVLTreeNode Root
        {
            get => root;
        }

        public AVLTree()
        {

        }
        public AVLTree(IEnumerable<T> values)
        {
            foreach (var value in values)
                Add(value);
        }

        #region ICollection<T>
        public void Add(T item)
        {

            if (root == null)
                root = new AVLTreeNode(item, null, this);
            else
                add(item, root);
            Count++;
        }
        void add(T item, AVLTreeNode parent)
        {
            int res = item.CompareTo(parent.Value);
            if (res < 0)
            {
                if (parent.Left != null)
                    add(item, parent.Left);
                else
                    parent.Left = new AVLTreeNode(item, parent, this);
            }
            else
            {
                if (parent.Right != null)
                    add(item, parent.Right);
                else
                    parent.Right = new AVLTreeNode(item, parent, this);
            }
            parent.Balance();
        }
        public void Clear()
        {
            root = null;
            Count = 0;
        }
        public bool Contains(T item)
        {
            return Search(item) != null ? true : false;
        }
        AVLTreeNode Search(T item)
        {
            AVLTreeNode node = root;

            while (node != null)
            {
                int res = item.CompareTo(node.Value);

                if (res < 0)
                    node = node.Left;
                else if (node.Value.Equals(item))
                    break;
                else
                    node = node.Right;
            }
            return node;
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
                AVLTreeNode node = Search(item);

                if (node == null)
                    return false;

                AVLTreeNode parent = node.Parent;

                Count--;
                int res;
                if (node.Right == null)
                {
                    if (node.Parent == null)
                    {
                        root = node.Left;
                        if (root != null)
                            root.Parent = null;
                    }
                    else
                    {
                        res = node.CompareNode(node.Parent);

                        if (res < 0)
                            node.Parent.Left = node.Left;
                        else
                            node.Parent.Right = node.Left;

                        if (node.Left != null)
                            node.Left.Parent = node.Parent;
                    }
                }
                else if (node.Right.Left == null)
                {
                    node.Right.Left = node.Left;
                    node.Left.Parent = node.Right;
                    if (node.Parent == null)
                    {
                        root = node.Right;
                    }
                    else
                    {
                        res = node.CompareNode(node.Parent);

                        if (res < 0)
                            node.Parent.Left = node.Right;
                        else
                            node.Parent.Right = node.Right;

                        if (node.Right != null)
                            node.Right.Parent = node.Parent;
                    }
                }
                else
                {
                    AVLTreeNode mostLeft = node.Right.Left;

                    while (mostLeft.Left != null)
                        mostLeft = mostLeft.Left;

                    mostLeft.Parent.Left = mostLeft.Right;
                    mostLeft.Left = node.Left;
                    mostLeft.Right = node.Right;

                    if (node.Parent == null)
                        root = mostLeft;
                    else
                    {
                        res = node.CompareNode(node.Parent);

                        if (res < 0)
                            node.Parent.Left = mostLeft;
                        else
                            node.Parent.Right = mostLeft;

                        mostLeft.Parent = node.Parent;
                    }
                }

                if (parent != null)
                    parent.Balance();
                else if (root != null)
                    root.Balance();
                return true;
            }
            return false;
        }
        public int Count { get; private set; } = 0;
        public bool IsReadOnly => false;

        #endregion

        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal().GetEnumerator();
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
        List<T> PostOrderTraversal(AVLTreeNode root, ref List<T> list)
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
        List<T> PreOrderTraversal(AVLTreeNode root, ref List<T> list)
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
        public static List<T> InOrderTraversal(AVLTreeNode root, ref List<T> list)
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

        public T this[int index]
        {
            get
            {
                return InOrderTraversal()[index];
            }
            set
            {
                List<T> values = InOrderTraversal();
                values[index] = value;
                Clear();
                values.Sort();
                foreach (T a in values)
                {
                    Add(a);
                }
            }
        }
    } // TODO: ask about course work 
}
