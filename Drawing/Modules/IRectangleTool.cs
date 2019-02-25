using Drawing.Domain;

namespace Drawing.Modules {
    public interface IRectangleTool {
        /// <summary>
        /// Draw a rectangle from a node to another node
        /// </summary>
        /// <param name="node1">Start node</param>
        /// <param name="node2">End node</param>
        void DrawRectangle(Node node1, Node node2);
    }
}
