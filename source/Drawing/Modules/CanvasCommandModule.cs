using System;
using System.Collections.Generic;
using System.Text;

namespace Drawing.Modules {
    public abstract class CanvasCommandModule : ICommandModule<ICanvas>{
        protected ICanvas data;

        protected CanvasCommandModule(ICanvas data) {
            this.data = data;
        }

        public abstract string Command { get; }

        public abstract string[] Parameters { get; }

        public abstract string Description { get; }

        public abstract ICanvas Execute(string[] args);
    }
}
