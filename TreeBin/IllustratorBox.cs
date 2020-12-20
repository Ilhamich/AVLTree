using System;
using System.Collections;

namespace MyTreeBin
{
    public class IllustratorBox<T> : IEnumerable
        where T : IComparable<T>
    {
        string[] _dataArr;
        NodeIllustrator[] _illustrator;
        Tree<T> _owner;

        public IllustratorBox(Tree<T> owner)
        {
            _owner = owner;
        }

        private void InitIDataArrey(int count)
        {
            T[] arrey = new T[count];

            _dataArr = new string[count];

            int index = 0;

            _owner.CopyTo(arrey, index);

            for (int i = 0; i < arrey.Length; i++)
            {
                _dataArr[i] = arrey[i].ToString();
            }
        }

        public IllustratorBox<T> GetIllustrator()
        {
            _illustrator = new NodeIllustrator[_owner.Count];

            InitIDataArrey(_owner.Count);

            CreateIllustrator();

            int countForIllstrator = 0;

            InitIllustrator(_owner._root, ref countForIllstrator);

            return this;
        }

        private void CreateIllustrator(int position = 0)
        {
            if (position == 0)
            {
                _illustrator[position] = new NodeIllustrator
                        (_dataArr[position], position);

                position++;
            }
            else
            {
                _illustrator[position] = new NodeIllustrator
                       (_dataArr[position], _illustrator[position - 1].X
                       + _dataArr[position - 1].Length + 1);

                position++;
            }

            if (position < _owner.Count)
            {
                CreateIllustrator(position);
            }
        }

        private void InitIllustrator
                (Tree<T>.NodeTree node, ref int index, int depth = 0)
        {
            if (node._left != null)
            {
                InitIllustrator(node._left, ref index, depth + 1);
            }

            _illustrator[index].Y = depth + depth;
            index++;

            if (node._left != null)
            {
                _illustrator[index - 1].Left = true;
            }

            if (node._right != null)
            {
                _illustrator[index - 1].Right = true;

                InitIllustrator(node._right, ref index, depth + 1);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in _illustrator)
            {
                yield return item;
            }
        }
    }
}
