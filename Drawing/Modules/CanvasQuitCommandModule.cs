using System;

namespace Drawing.Modules {
    public class CanvasQuitCommandModule : ICommandModule<ICanvas> {
        public string Command {
            get {
                return "Q";
            }
        }

        public string[] Parameters {
            get {
                return new string[0];
            }
        }

        public string Description {
            get {
                return "Quit the program";
            }
        }

        public ICanvas Execute(ICanvas data, string[] args) {
            Environment.Exit(0);

            return data;
        }
    }
}
