﻿using System;

namespace Drawing.Modules {
    public class CanvasRectangleCommandModule : ICommandModule<ICanvas> {
        public string Command {
            get {
                return "R";
            }
        }

        public string[] Parameters {
            get {
                return new string[] { "x1", "y1", "x2", "y2" };
            }
        }

        public string Description {
            get {
                return "Create a new rectangle, whose upper left corner is (x1,y1) and lower right corner is (x2, y2)";
            }
        }

        public ICanvas Execute(ICanvas data, string[] args) {
            if (int.TryParse(args[0], out int x1) || int.TryParse(args[1], out int y1)
                || int.TryParse(args[2], out int x2) || int.TryParse(args[3], out int y2)) {
                throw new ArgumentException("Coordinates must be numbers!");
            }

            if (data.Canvas == null || data.Canvas.Length == 0) {
                throw new ArgumentException("Canvas is not created yet");
            }

            int width = data.Canvas.GetLength(0);
            int height = data.Canvas.GetLength(1);

            if (x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0 ||
                x1 >= width || y1 >= height || x2 >= width || y2 >= height) {
                throw new ArgumentException("Cannot draw outside of canvas area");
            }

            ICanvas canvas = data;
            canvas = DrawLine(data, x1, y1, x2, y1);
            canvas = DrawLine(data, x1, y2, x1, y1);
            canvas = DrawLine(data, x1, y2, x2, y2);
            canvas = DrawLine(data, x2, y2, x2, y1);

            return data;
        }

        private ICanvas DrawLine(ICanvas data, int x1, int y1, int x2, int y2) {
            int xStart = Math.Min(x1, x2);
            int yStart = Math.Min(y1, y2);
            int xEnd = Math.Max(x1, x2);
            int yEnd = Math.Max(y1, y2);

            for (int i = yStart; i <= yEnd; ++i) {
                for (int j = xStart; j <= xEnd; ++j) {
                    data.Canvas[i, j] = 'x';
                }
            }

            return data;
        }
    }
}
