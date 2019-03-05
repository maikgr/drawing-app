using System;

namespace Drawing.Modules {
    public class CanvasCreateCommandModule : ICommandModule<ICanvas> {
        public string Command {
            get {
                return "C";
            }
        }

        public string[] Parameters {
            get {
                return new string[] { "w", "h" };
            }
        }

        public string Description {
            get {
                return "Create a new canvas of width w and height h";
            }
        }

        public ICanvas Execute(ICanvas data, string[] args) {
            if (!int.TryParse(args[0], out int width) || !int.TryParse(args[1], out int height)) {
                throw new ArgumentException("Width and height must be numbers!");
            }
            if (width <= 0 || height <= 0) {
                throw new ArgumentException("Width and Height must be more than 0!");
            }

            data.Canvas = new char[height, width];

            return data;
        }
    }
}
