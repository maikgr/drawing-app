using System;
using System.Collections.Generic;

namespace Drawing.Modules {
    public class CanvasBucketCommandModule : ICommandModule<ICanvas> {
        public string Command {
            get {
                return "B";
            }
        }

        public string[] Parameters {
            get {
                return new string[] { "x", "y", "c" };
            }
        }

        public string Description {
            get {
                return "Fill the entire area connected to (x,y) with \"colour\" c.";
            }
        }

        public ICanvas Execute(ICanvas data, string[] args) {
            if (!int.TryParse(args[0], out int x) || !int.TryParse(args[1], out int y)) {
                throw new ArgumentException("Coordinates must be numbers!");
            }

            if (!char.TryParse(args[2], out char c)) {
                throw new ArgumentException("Color must be a single character!");
            }

            if (data.Canvas == null || data.Canvas.Length == 0) {
                throw new ArgumentException("Canvas is not created yet");
            }

            int width = data.Canvas.GetLength(1);
            int height = data.Canvas.GetLength(0);
            x -= 1;
            y -= 1;

            if (x < 0 || y < 0 || x >= width || y >= height) {
                throw new ArgumentException("Cannot fill outside of canvas area");
            }

            if (data.Canvas[y, x].Equals(c)) {
                return data;
            }

            return Fill(data, x, y, c);
        }

        private ICanvas Fill(ICanvas data, int x, int y, char c) {
            int width = data.Canvas.GetLength(1);
            int height = data.Canvas.GetLength(0);
            int targetCurrentColor = data.Canvas[y, x];
            Queue<(int, int)> nodeQueue = new Queue<(int, int)>();
            nodeQueue.Enqueue((x, y));

            while (nodeQueue.Count > 0) {
                (int currentX, int currentY) = nodeQueue.Dequeue();
                if (currentX >= 0 && currentX < width &&
                    currentY >= 0 && currentY < height &&
                    data.Canvas[currentY, currentX] == targetCurrentColor) {
                    data.Canvas[currentY, currentX] = c;
                    nodeQueue.Enqueue((currentX - 1, currentY));
                    nodeQueue.Enqueue((currentX + 1, currentY));
                    nodeQueue.Enqueue((currentX, currentY - 1));
                    nodeQueue.Enqueue((currentX, currentY + 1));
                }
            }

            return data;
        }
    }
}
