using Drawing.Domain;
using System;
using System.Collections.Generic;

namespace Drawing.Modules {
    public class ConsoleCanvas : ICanvas {
        private int[,] canvas;
        private int width;
        private int height;
        IDictionary<char, int> colorMap;
        IDictionary<int, char> colorNumberMap;
        private int currentColorNum = 1;

        public ConsoleCanvas() {
            colorMap = new Dictionary<char, int>();
            colorNumberMap = new Dictionary<int, char>();
        }

        public void Create(int w, int h) {
            if (w <= 0 || h <= 0) {
                throw new ArgumentException("Width and Height must be more than 0!");
            }
            width = w;
            height = h;
            canvas = new int[h, w];
            Render();
        }

        public void DrawLine(Node node1, Node node2) {
            if (canvas == null || canvas.Length == 0) {
                throw new ArgumentException("Canvas is not created yet");
            }

            if (node1.X < 0 || node1.Y < 0 || node2.X < 0 || node2.Y < 0 ||
                node1.X >= width || node1.Y >= height || node2.X >= width || node2.Y >= height) {
                throw new ArgumentException("Cannot draw outside of canvas area");
            }

            if (!node1.X.Equals(node2.X) && !node1.Y.Equals(node2.Y)) {
                throw new ArgumentException("Cannot draw diagonal line");
            }

            DrawStraightLine(node1.X, node1.Y, node2.X, node2.Y);
            Render();
        }

        public void DrawRectangle(Node node1, Node node2) {
            if (canvas == null || canvas.Length == 0) {
                throw new ArgumentException("Canvas is not created yet");
            }

            if (node1.X < 0 || node1.Y < 0 || node2.X < 0 || node2.Y < 0 ||
                node1.X >= width || node1.Y >= height || node2.X >= width || node2.Y >= height) {
                throw new ArgumentException("Cannot draw outside of canvas area");
            }

            DrawStraightLine(node1.X, node1.Y, node2.X, node1.Y);
            DrawStraightLine(node2.X, node2.Y, node1.X, node2.Y);
            DrawStraightLine(node2.X, node1.Y, node2.X, node2.Y);
            DrawStraightLine(node1.X, node1.Y, node1.X, node2.Y);
            Render();
        }

        public void Fill(Node node, char colorCode) {
            if (canvas == null || canvas.Length == 0) {
                throw new ArgumentException("Canvas is not created yet");
            }

            if (node.X < 0 || node.Y < 0 || node.X >= width || node.Y >= height) {
                throw new ArgumentException("Cannot fill outside of canvas area");
            }

            if (canvas[node.Y, node.X] == -1) {
                return;
            }

            if (!colorMap.ContainsKey(colorCode)) {
                colorMap.Add(colorCode, currentColorNum);
                colorNumberMap.Add(currentColorNum, colorCode);
                ++currentColorNum;
            }

            int targetCurrentColor = canvas[node.Y, node.X];
            Queue<Node> nodeQueue = new Queue<Node>();
            nodeQueue.Enqueue(node);

            while (nodeQueue.Count > 0) {
                Node current = nodeQueue.Dequeue();
                if (current.X >= 0 && current.X < width &&
                    current.Y >= 0 && current.Y < height &&
                    canvas[current.Y, current.X] == targetCurrentColor) {
                    canvas[current.Y, current.X] = colorMap[colorCode];
                    nodeQueue.Enqueue(new Node(current.X - 1, current.Y));
                    nodeQueue.Enqueue(new Node(current.X + 1, current.Y));
                    nodeQueue.Enqueue(new Node(current.X, current.Y - 1));
                    nodeQueue.Enqueue(new Node(current.X, current.Y + 1));
                }
            }

            Render();
        }

        private void DrawStraightLine(int x1, int y1, int x2, int y2) {
            int xStart = Math.Min(x1, x2);
            int yStart = Math.Min(y1, y2);
            int xEnd = Math.Max(x1, x2);
            int yEnd = Math.Max(y1, y2);

            for (int i = yStart; i <= yEnd; ++i) {
                for (int j = xStart; j <= xEnd; ++j) {
                    canvas[i, j] = -1;
                }
            }
        }

        private void Render() {
            for (int y = -1; y <= height; ++y) {
                for (int x = -1; x <= width; ++x) {
                    if (y.Equals(-1) || y.Equals(height)) {
                        Console.Write("-");
                        continue;
                    }
                    else if (x.Equals(-1) || x.Equals(width)) {
                        Console.Write("|");
                        continue;
                    }

                    if (x > -1 && x < width && y > -1 && y < height) {
                        if (canvas[y, x] < 0) {
                            Console.Write("x");
                        }
                        else if (canvas[y, x] > 0) {
                            Console.Write(colorNumberMap[canvas[y, x]]);
                        }
                        else {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
