using System;
using System.Collections.Generic;
using System.Text;

namespace Drawing.Modules {
    public interface ICommandModule<T> {
        string Command { get; }
        string[] Parameters { get; }
        string Description { get; }
        T Execute(T data, string[] args);
    }
}
