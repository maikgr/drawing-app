using System;

namespace Drawing.Modules {
    public class ConsoleCanvas : ICanvas {
        public char[,] Canvas { get; set; }

        public void Render() {
            if (Canvas == null || Canvas.Length == 0) {
                throw new ArgumentException("Canvas is not created yet");
            }

            int width = Canvas.GetLength(1);
            int height = Canvas.GetLength(0);

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
                        if (Canvas[y, x].Equals(default(char))) {
                            Console.Write(" ");
                        }
                        else {
                            Console.Write(Canvas[y, x]);
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
