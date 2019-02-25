using Drawing.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drawing.Modules {
    public class ConsoleCanvasModule : IConsoleModule<ICanvas> {
        public ICanvas Module { get; }

        public IDictionary<string, Action<string[]>> Commands { get; }

        public string CommandsHelp { get; }

        public ConsoleCanvasModule(ICanvas canvas) {
            this.Module = canvas;
            this.Commands = InitializeCommands();
            this.CommandsHelp = GetCommandsHelp();
        }

        private IDictionary<string, Action<string[]>> InitializeCommands() {
            IDictionary<string, Action<string[]>> commandMap = new Dictionary<string, Action<string[]>>
            {
                { "C", new Action<string[]>(CreateCanvas) },
                { "L", new Action<string[]>(DrawLine) },
                { "R", new Action<string[]>(DrawRectangle) },
                { "B", new Action<string[]>(Fill) },
                { "Q", new Action<string[]>(Quit) }
            };

            return commandMap;
        }

        private string GetCommandsHelp() {
            StringBuilder builder = new StringBuilder();
            builder.Append("C w h\t\t\t Create a new canvas of width w and height h.");
            builder.Append(Environment.NewLine);
            builder.Append("L x1 y1 x2 y2\t\t Create a new line from (x1,y1) to (x2,y2).");
            builder.Append(Environment.NewLine);
            builder.Append("R x1 y1 x2 y2\t\t Create a new rectangle, whose upper left corner is (x1,y1) and lower right corner is (x2, y2).");
            builder.Append(Environment.NewLine);
            builder.Append("B x y c\t\t\t Fill the entire area connected to (x,y) with \"colour\" c.");
            builder.Append(Environment.NewLine);
            builder.Append("Q\t\t\t Quit the program.");

            return builder.ToString();
        }


        private void CreateCanvas(string[] args) {
            if (args.Length < 3 || !args[0].Equals("C") || !int.TryParse(args[1], out int width) || !int.TryParse(args[2], out int height)) {
                throw new Exception("Incorrect Commany Syntax. Usage: C [width] [height]");
            }

            Module.Create(width, height);
        }

        private void DrawLine(string[] args) {
            if (args.Length < 5 || !args[0].Equals("L") || !int.TryParse(args[1], out int x1) || !int.TryParse(args[2], out int y1)
                 || !int.TryParse(args[3], out int x2) || !int.TryParse(args[4], out int y2)) {
                throw new Exception("Incorrect Commany Syntax. Usage: L [x1] [y1] [x2] [y2]");
            }

            Module.DrawLine(new Node(x1 - 1, y1 - 1), new Node(x2 - 1, y2 - 1));
        }

        private void DrawRectangle(string[] args) {
            if (args.Length < 5 || !args[0].Equals("R") || !int.TryParse(args[1], out int x1) || !int.TryParse(args[2], out int y1)
                 || !int.TryParse(args[3], out int x2) || !int.TryParse(args[4], out int y2)) {
                throw new Exception("Incorrect Commany Syntax. Usage: R [x1] [y1] [x2] [y2]");
            }

            Module.DrawRectangle(new Node(x1 - 1, y1 - 1), new Node(x2 - 1, y2 - 1));
        }

        private void Fill(string[] args) {
            if (args.Length < 4 || !args[0].Equals("B") || !int.TryParse(args[1], out int x) || !int.TryParse(args[2], out int y)) {
                throw new Exception("Incorrect Commany Syntax. Usage: B [x] [y] [c]");
            }

            Module.Fill(new Node(x - 1, y - 1), args[3][0]);
        }

        private static void Quit(string[] args) {
            Environment.Exit(0);
        }
    }
}
