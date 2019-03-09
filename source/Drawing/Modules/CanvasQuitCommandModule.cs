using System;

namespace Drawing.Modules {
    public class CanvasQuitCommandModule : CanvasCommandModule {
        public override string Command {
            get {
                return "Q";
            }
        }

        public override string[] Parameters {
            get {
                return new string[0];
            }
        }

        public override string Description {
            get {
                return "Quit the program";
            }
        }

        public CanvasQuitCommandModule(ICanvas canvas) : base(canvas) {
        }

        public override ICanvas Execute(string[] args) {
            Environment.Exit(0);

            return data;
        }
    }
}
