using System;

namespace MyBinaryTree
{
    public class BinaryTreeNode<TNode> : IComparable<TNode> where TNode : class, IComparable<TNode>
    {
        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }
        public TNode Value { get; private set; }
        public BinaryTreeNode<TNode> Left { get; set; }
        public BinaryTreeNode<TNode> Right { get; set; }

        #region IComparable<TNode>
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
        public int CompareNode(BinaryTreeNode<TNode> other)
        {
            return Value.CompareTo(other.Value);
        }
        #endregion
    }
}
