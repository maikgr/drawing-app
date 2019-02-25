using System;
using System.Collections.Generic;

namespace Drawing.Modules {
    public interface IConsoleModule<T> {
        T Module { get; }

        IDictionary<string, Action<string[]>> Commands { get; }

        string CommandsHelp { get; }
    }
}
