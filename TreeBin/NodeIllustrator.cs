namespace MyTreeBin
{
    public class NodeIllustrator
    {
        public readonly string _data;

        public bool Left { get; internal set; }

        public bool Right { get; internal set; }

        public int X { get; }

        public int Y { get; internal set; }
       
        public NodeIllustrator(string data, int x)
        {
            X = x;
            _data = data;
        }
    }
}
