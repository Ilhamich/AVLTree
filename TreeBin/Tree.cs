using System;
using System.Collections;
using System.Collections.Generic;

namespace MyTreeBin
{
    public class Tree<T> : ICollection<T>
        where T : IComparable<T>
    {
        internal NodeTree _root;

        public Tree()
        {
            _root = null;
            Count = 0;
        }

        public int Count { get; private set; }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #region Member functions

        public void Add(T item)
        {
            bool addition = false;

            AddRecursion(item, ref addition, ref _root);

            if (addition)
            {
                Count++;
            }
        }

        public void Clear()
        {
            ClearTree(ref _root);
            Count = 0;
        }

        public bool Contains(T item)
        {
            bool resolt = false;

            Search(item, _root, ref resolt);

            return resolt;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex - Count >= 0)
            {
                CopyToArrey(array, _root, ref arrayIndex);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public bool Remove(T item)
        {
            bool resolt = false;

            Delete(item, ref _root, ref resolt);

            if (resolt)
            {
                Count--;
            }

            return resolt;
        }

        #endregion

        #region Static function

        private static void AddRecursion
           (T item, ref bool addition, ref NodeTree SomeNode)
        {
            if (SomeNode == null)
            {
                SomeNode = new NodeTree(item);
                addition = true;
            }
            else
            {
                if (item.CompareTo(SomeNode.Value) < 0)
                {
                    AddRecursion(item, ref addition, ref SomeNode._left);
                }
                else
                {
                    if (item.CompareTo(SomeNode.Value) > 0)
                    {
                        AddRecursion(item, ref addition, ref SomeNode._right);
                    }
                }
            }

            if (addition)
            {
                Balance(ref SomeNode);
            }
        }

        private static void ClearTree(ref NodeTree node)
        {
            if (node._left != null)
            {
                ClearTree(ref node._left);
            }

            if (node._right != null)
            {
                ClearTree(ref node._right);
            }

            node = null;
        }

        private static void Search(T item, NodeTree node, ref bool resolt)
        {
            if (resolt)
            {
                return;
            }

            if (item.CompareTo(node.Value) == 0)
            {
                resolt = true;
                return;
            }
            else
            {
                if (item.CompareTo(node.Value) < 0)
                {
                    Search(item, node._left, ref resolt);
                }
                else
                {
                    if (item.CompareTo(node.Value) > 0)
                    {
                        Search(item, node._right, ref resolt);
                    }
                }
            }
        }

        private static void CopyToArrey(T[] array, NodeTree node, ref int arrayIndex)
        {
            if (node._left != null)
            {
                CopyToArrey(array, node._left, ref arrayIndex);
            }

            array[arrayIndex] = node.Value;
            arrayIndex++;

            if (node._right != null)
            {
                CopyToArrey(array, node._right, ref arrayIndex);
            }
        }

        private static void Delete(T item, ref NodeTree node, ref bool resolt)
        {
            if (item.CompareTo(node.Value) == 0)
            {
                if (node._left == null && node._right == null)
                {
                    node = null;

                    resolt = true;
                }
                else
                {
                    DeleteWithBranch(ref node, ref resolt);
                }
            }
            else
            {
                if (item.CompareTo(node.Value) < 0)
                {
                    Delete(item, ref node._left, ref resolt);
                }
                else
                {
                    Delete(item, ref node._right, ref resolt);
                }
            }

            if (resolt && node != null)
            {
                Balance(ref node);
            }
        }

        private static void DeleteWithBranch(ref NodeTree node, ref bool resolt)
        {
            if (node._left != null && node._right == null)
            {
                node = node._left;

                resolt = true;
            }
            else
            {
                if (node._left == null && node._right != null)
                {
                    node = node._right;

                    resolt = true;
                }
                else
                {
                    int leftBranch = 0;

                    CountNodes(node._left, ref leftBranch);

                    int rightBranch = 0;

                    CountNodes(node._right, ref rightBranch);

                    if (leftBranch > rightBranch)
                    {
                        ComplexDelete(true, ref node, ref node._left);

                        resolt = true;

                        if (resolt)
                        {
                            Balance(ref node._left);
                        }
                    }
                    else
                    {
                        ComplexDelete(false, ref node, ref node._right);

                        resolt = true;

                        if (resolt)
                        {
                            Balance(ref node._right);
                        }
                    }
                }
            }
        }

        private static void ComplexDelete
                (bool leftOrNot, ref NodeTree nodeFDel, ref NodeTree node)
        {
            T tmp;

            if (leftOrNot)
            {
                if (node._right == null)
                {
                    tmp = node.Value;

                    if (node._left != null)
                    {
                        node = node._left;
                    }
                    else
                    {
                        node = null;
                    }

                    nodeFDel.Value = tmp;
                }
                else
                {
                    ComplexDelete(true, ref nodeFDel, ref node._right);
                }
            }
            else
            {
                if (node._left == null)
                {
                    tmp = node.Value;

                    if (node._right != null)
                    {
                        node = node._right;
                    }
                    else
                    {
                        node = null;
                    }

                    nodeFDel.Value = tmp;
                }
                else
                {
                    ComplexDelete(false, ref nodeFDel, ref node._left);
                }
            }
        }

        private static void CountNodes(NodeTree branch, ref int count)
        {
            count++;

            if (branch._left != null)
            {
                CountNodes(branch._left, ref count);
            }

            if (branch._right != null)
            {
                CountNodes(branch._right, ref count);
            }
        }

        private static void Balance(ref NodeTree node)
        {
            int resoltLeft = 0;
            int resoltRight = 0;

            if (node._left != null)
            {
                CountDepth(node._left, ref resoltLeft);
            }

            if (node._right != null)
            {
                CountDepth(node._right, ref resoltRight);
            }

            int secondRLeft = 0;
            int secondRRight = 0;

            if (resoltLeft - resoltRight < -1)
            {
                if (node._right._left != null)
                {
                    CountDepth(node._right._left, ref secondRLeft);
                }

                if (node._right._right != null)
                {
                    CountDepth(node._right._right, ref secondRRight);
                }

                if (secondRLeft <= secondRRight)
                {
                    TurnLeft(ref node);
                }
                else
                {
                    TurnRight(ref node._right);
                    TurnLeft(ref node);
                }
            }
            else
            {
                if (resoltLeft - resoltRight > 1)
                {
                    if (node._left._left != null)
                    {
                        CountDepth(node._left._left, ref secondRLeft);
                    }

                    if (node._left._right != null)
                    {
                        CountDepth(node._left._right, ref secondRRight);
                    }

                    if (secondRRight <= secondRLeft)
                    {
                        TurnRight(ref node);
                    }
                    else
                    {
                        TurnLeft(ref node._left);
                        TurnRight(ref node);
                    }
                }
            }
        }

        private static void TurnRight(ref NodeTree root)
        {
            NodeTree nLRoot = root._left;

            root._left = nLRoot._right;
            nLRoot._right = root;
            root = nLRoot;
        }

        private static void TurnLeft(ref NodeTree root)
        {
            NodeTree nRRoot = root._right;

            root._right = nRRoot._left;
            nRRoot._left = root;
            root = nRRoot;
        }

        private static void CountDepth
                   (NodeTree node, ref int resoltCount, int count = 1)
        {
            if (node._left != null)
            {
                CountDepth(node._left, ref resoltCount, count + 1);
            }

            if (node._right != null)
            {
                CountDepth(node._right, ref resoltCount, count + 1);
            }

            if (count > resoltCount)
            {
                resoltCount = count;
            }
        }

        #endregion

        #region IEnumerable & IEnumerator 

        public IEnumerator<T> GetEnumerator()
        {
            return new TreeIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TreeIterator(this);
        }

        private struct TreeIterator : IEnumerator<T>
        {
            private Tree<T> _tree;
            private NodeTree _currentNode;
            private int _possition;

            public TreeIterator(Tree<T> thatTree)
            {
                _tree = thatTree;
                _currentNode = thatTree._root;
                _possition = 0;
            }

            public T Current
            {
                get
                {
                    return _currentNode.Value;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return _currentNode.Value;
                }
            }

            public void Dispose()
            {
                if (_tree._root != null)
                {
                    ReleaseIteratorData(_tree._root);
                }
            }

            private static void ReleaseIteratorData(NodeTree node)
            {
                if (node._left != null)
                {
                    ReleaseIteratorData(node._left);
                }

                if (node._right != null)
                {
                    ReleaseIteratorData(node._right);
                }

                node._parent = null;
                node._iteration = false;
            }

            public bool MoveNext()
            {
                bool resolt = false;

                if (_currentNode != null && _possition == 0)
                {
                    _possition++;
                    _currentNode._iteration = true;
                    resolt = true;
                }
                else
                {
                    if (_possition < _tree.Count)
                    {
                        if (_currentNode._left != null
                               && !_currentNode._left._iteration)
                        {
                            MoveLeft(ref resolt);
                        }
                        else
                        {
                            if (_currentNode._right != null
                                    && !_currentNode._right._iteration)
                            {
                                MoveRight(ref resolt);
                            }
                            else
                            {
                                MoveBack();
                                resolt = MoveNext();
                            }
                        }
                    }
                }

                return resolt;
            }

            private void MoveLeft(ref bool resolt)
            {
                _currentNode._left._parent = _currentNode;
                _currentNode = _currentNode._left;
                _currentNode._iteration = true;

                _possition++;
                resolt = true;
            }

            private void MoveRight(ref bool resolt)
            {
                _currentNode._right._parent = _currentNode;
                _currentNode = _currentNode._right;
                _currentNode._iteration = true;

                _possition++;
                resolt = true;
            }

            private void MoveBack()
            {
                if (_currentNode._parent != null)
                {
                    _currentNode = _currentNode._parent;
                }
            }

            public void Reset()
            {
                _currentNode = _tree._root;
                _possition = 0;
            }
        }

        #endregion

        internal class NodeTree
        {
            public T Value { get; set; }

            public NodeTree _parent;
            public NodeTree _left;
            public NodeTree _right;

            public bool _iteration;

            public NodeTree(T value)
            {
                Value = value;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}
