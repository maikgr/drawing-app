namespace Drawing.Modules {
    public interface ICanvas : IFillTool, ILineTool, IRectangleTool {
        /// <summary>
        /// Create canvas
        /// </summary>
        /// <param name="w">The width of the canvas</param>
        /// <param name="h">The height of the canvas</param>
        /// <returns></returns>
        void Create(int w, int h);
    }
}
