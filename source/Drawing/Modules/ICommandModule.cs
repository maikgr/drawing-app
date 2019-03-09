namespace Drawing.Modules {
    public interface ICommandModule<T> {

        /// <summary>
        /// The command to trigger this cmodule
        /// </summary>
        string Command { get; }

        /// <summary>
        /// The required parameters of this module
        /// </summary>
        string[] Parameters { get; }

        /// <summary>
        /// The description of this module
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Execute this module
        /// </summary>
        /// <param name="args">The arguments or parameters given to this module</param>
        /// <returns>The data result</returns>
        T Execute(string[] args);
    }
}
