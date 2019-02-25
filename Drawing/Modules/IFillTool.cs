using Drawing.Domain;

namespace Drawing.Modules {
    public interface IFillTool {
        /// <summary>
        /// Fill an area around point x, y
        /// </summary>
        /// <param name="node">The node in an area to fill</param>
        /// <param name="colorCode">Code of the color to fill</param>
        void Fill(Node node, char colorCode);
    }
}
