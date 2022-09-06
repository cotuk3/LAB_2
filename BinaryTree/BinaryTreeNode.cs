using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBinaryTree
{
    public class BinaryTreeNode<TNode> : IComparable<TNode> where TNode : class, IComparable<TNode>
    {
        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }

        public TNode Value{ get; set; }
        public BinaryTreeNode<TNode> Left { get; set; }
        public BinaryTreeNode<TNode> Right { get; set; }

        #region IComparable<TNode>
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
        public int CompareToNodes(BinaryTreeNode<TNode> other)
        {
            return Value.CompareTo(other.Value);
        }
        #endregion
    }
}
