using System;

namespace MyTreeBin
{
    public class Vizualizator
    {
        public static void ShowTree<T>(IllustratorBox<T> illustrators)
             where T : IComparable<T>
        {
            foreach (NodeIllustrator item in illustrators)
            {
                Console.SetCursorPosition(item.X, item.Y);

                Console.Write(item._data);

                if (item.Left)
                {
                    Console.SetCursorPosition(item.X - 1, item.Y + 1);
                    Console.Write('/');
                }

                if (item.Right)
                {
                    Console.SetCursorPosition
                            (item.X + item._data.Length, item.Y + 1);

                    Console.Write('\\');
                }
            }

            Console.ReadKey();

            Console.Clear();
        }
    }
}
