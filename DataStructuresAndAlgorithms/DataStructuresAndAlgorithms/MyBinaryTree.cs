using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms {
    class MyBinaryTreeNode<TNode> : IComparable<TNode> where TNode : IComparable<TNode> {

        public MyBinaryTreeNode(TNode value) {
            Value = value;
        }
        public MyBinaryTreeNode<TNode> Left { get; set; }
        public MyBinaryTreeNode<TNode> Right { get; set; }
        public TNode Value { get; private set; }
        public int CompareTo(TNode other) {
            return Value.CompareTo(other);
        }
    }
    class MyBinaryTree<T> : IEnumerable<T> where T : IComparable<T> {
        private MyBinaryTreeNode<T> _head;
        private int _count;

        public void Add(T value) {
            if (_head == null) {
                _head = new MyBinaryTreeNode<T>(value);
            }
            else {
                AddTo(_head, value);
            }
            _count++;
        }
        private void AddTo(MyBinaryTreeNode<T> node, T value) {
            if (value.CompareTo(node.Value) > 0) {
                if (node.Left == null) {
                    node.Left = new MyBinaryTreeNode<T>(value);
                }
                else {
                    AddTo(node.Left, value);
                }
            }
            else {
                if (node.Right == null) {
                    node.Right = new MyBinaryTreeNode<T>(value);
                }
                else {
                    AddTo(node.Right, value);
                }
            }
        }
        private MyBinaryTreeNode<T> FindWithParent(T value, out MyBinaryTreeNode<T> parent) {
            MyBinaryTreeNode<T> current = _head;
            parent = null;

            while (current != null) {
                int result = current.CompareTo(value);

                if (result > 0) {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0) {
                    parent = current;
                    current = current.Right;
                }
                else {
                    break;
                }
            }
            return current;
        }
        public bool Contains(T value) {
            MyBinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }
        public bool Remove(T value) {
            MyBinaryTreeNode<T> current, parent;

            // find the node to remove
            current = FindWithParent(value, out parent);
            if (current == null) {
                return false;
            }
            _count--;

            // if current has no right chile, current's left replace current
            if (current.Right == null) {
                if (parent == null) {
                    _head = current.Left;
                }
                else {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0) {
                        // make current left child a left child of parent
                        parent.Left = current.Left;
                    }
                    if (result < 0) {
                        // make current left child a right child of parent
                        parent.Right = current.Left;
                    }
                }
            }
            // if current's right child has no left child, current's right child replaces current
            else if (current.Right.Left == null) {
                current.Right.Left = current.Left;
                if (parent == null) {
                    _head = current.Right;
                }
                else {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0) {
                        // make current right child a left child of parent
                        parent.Left = current.Right;
                    }
                    else if (result < 0) {
                        // make current right child a right child of parent
                        parent.Right = current.Right;
                    }
                }
            }
            // if current's right child has a left child, replace current with current's right child's left-most child
            else {
                // find the right's left-most child
                MyBinaryTreeNode<T> leftMost = current.Right.Left;
                MyBinaryTreeNode<T> leftMostParent = current.Right;

                while (leftMost.Left != null) {
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }
                // the parent's left subtree becomes the left-most's right subtree
                leftMostParent.Left = leftMost.Right;

                // assign left-most's left and right to current's left and right children
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;

                if (parent == null) {
                    _head = leftMost;
                }
                else {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0) {
                        // make left-most the parent's left child
                        parent.Left = leftMost;
                    }
                    else if (result < 0) {
                        // make left-most the parent's right child
                        parent.Right = leftMost;
                    }
                }
            }
            return true;
        }
        public void PreOrderTraversal(Action<T> action) {
            PreOrderTraversal(action, _head);
        }
        private void PreOrderTraversal(Action<T> action, MyBinaryTreeNode<T> node) {
            if (node != null) {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }
        public void PostOrderTraversal(Action<T> action) {
            PostOrderTraversal(action, _head);
        }
        private void PostOrderTraversal(Action<T> action, MyBinaryTreeNode<T> node) {
            if (node != null) {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }
        public void InOrderTraversal(Action<T> action) {
            InOrderTraversal(action, _head);
        }
        private void InOrderTraversal(Action<T> action, MyBinaryTreeNode<T> node) {
            if (node != null) {
                InOrderTraversal(action, node.Left);
                action(node.Value);
                InOrderTraversal(action, node.Right);
            }
        }
        public IEnumerator<T> InOrderTraversal() {
            if (_head != null) {
                Stack<MyBinaryTreeNode<T>> stack = new Stack<MyBinaryTreeNode<T>>();
                MyBinaryTreeNode<T> current = _head;
                bool goLeftNext = true;
                stack.Push(current);

                while (stack.Count > 0) {
                    if (goLeftNext) {
                        while (current.Left != null) {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }
                    yield return current.Value; 

                    if (current.Right != null) {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }
        public int Count {
            get { return _count; }
        }
        public void Clear() {
            _head = null;
            _count = 0;
        }
        public IEnumerator<T> GetEnumerator() {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
