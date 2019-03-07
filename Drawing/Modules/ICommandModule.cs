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
        /// <param name="data">The data to execute against</param>
        /// <param name="args">The arguemnts or parameters given to this module</param>
        /// <returns>The processed data</returns>
        T Execute(string[] args);
    }
}
