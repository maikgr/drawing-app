using System;

namespace Drawing.Domain {
    public class Node : IComparable<Node> {
        public int X { get; }
        public int Y { get; }

        public Node(int x, int y) {
            X = x;
            Y = y;
        }

        public int CompareTo(Node other) {
            if (this.X.CompareTo(other.X).Equals(0)) {
                return this.Y.CompareTo(other.Y);
            }
            return this.X.CompareTo(other.X);
        }
    }
}
