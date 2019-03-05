namespace Drawing.Modules {
    public interface ICanvas {
        char[,] Canvas { get; set; }
        void Render();
    }
}
