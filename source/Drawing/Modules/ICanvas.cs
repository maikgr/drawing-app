namespace Drawing.Modules {
    public interface ICanvas {
        /// <summary>
        /// The canvas
        /// </summary>
        char[,] Canvas { get; set; }

        /// <summary>
        /// Render the canvas to display
        /// </summary>
        void Render();
    }
}
