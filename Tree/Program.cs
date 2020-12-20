using System;
using System.Collections;

using MyTreeBin;

namespace BinTree
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CHAR

            //Tree<char> myTree = new Tree<char>();

            //myTree.Add('A');
            //myTree.Add('B');
            //myTree.Add('C');

            //IllustratorBox<char> illustrator = new IllustratorBox<char>(myTree);
            //Vizualizator.ShowTree(illustrator.GetIllustrator());

            //myTree.Add('G');

            //Vizualizator.ShowTree(illustrator.GetIllustrator());

            //myTree.Add('W');

            //Vizualizator.ShowTree(illustrator.GetIllustrator());

            //myTree.Add('F');

            //Vizualizator.ShowTree(illustrator.GetIllustrator());

            #endregion

            #region INT

            Tree<int> myTree = new Tree<int>();

            myTree.Add(20);
            myTree.Add(10);
            myTree.Add(5);

            IllustratorBox<int> illustrator = new IllustratorBox<int>(myTree);
            Vizualizator.ShowTree(illustrator.GetIllustrator());

            myTree.Add(15);
            myTree.Add(16);

            Vizualizator.ShowTree(illustrator.GetIllustrator());

            myTree.Add(12);
            myTree.Add(13);

            Vizualizator.ShowTree(illustrator.GetIllustrator());

            myTree.Add(22);
            myTree.Add(23);
            myTree.Add(21);

            Vizualizator.ShowTree(illustrator.GetIllustrator());

            myTree.Add(24);
            myTree.Add(25);

            Vizualizator.ShowTree(illustrator.GetIllustrator());

            myTree.Add(4);
            myTree.Add(3);
            myTree.Add(2);
            myTree.Add(1);
            myTree.Add(0);

            Vizualizator.ShowTree(illustrator.GetIllustrator());

            myTree.Remove(2);
            myTree.Remove(4);
            myTree.Remove(5);

            Vizualizator.ShowTree(illustrator.GetIllustrator());

            #endregion

            //myTree.Clear();

            //myTree.CopyTo(arr, 2);

            foreach (int item in myTree)
            {
                Console.Write("{0} ", item);
            }

            //Console.WriteLine();

            //foreach (object item in (IEnumerable)myTree)
            //{
            //    Console.Write("{0} ", item);
            //}

            Console.ReadKey();
        }    
    }
}
