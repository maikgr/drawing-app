using Drawing.Domain;

namespace Drawing.Modules {
    public interface ILineTool {
        /// <summary>
        /// Draw a line from one node to another node
        /// </summary>
        /// <param name="node1">Start node</param>
        /// <param name="node2">End node</param>
        void DrawLine(Node node1, Node node2);
    }
}
