using Autofac;
using Drawing.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Drawing.Test {
    public class CanvasBucketCommandTests : BaseCanvasCommandTests {
        protected override Type ClassToTest {
            get {
                return typeof(CanvasBucketCommandModule);
            }
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(20, 4)]
        [InlineData(100, 100)]
        [InlineData(1000, 1000)]
        public void ShouldFillAllNodesOnEmptyCanvas(int width, int height) {
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                ICanvas canvas = container.Scope.Resolve<ICanvas>();
                canvas.Canvas = new char[height, width];

                canvas = container.Module.Execute(new string[] { "1", "1", "c" });

                for (int x = 0; x < width; ++x) {
                    for (int y = 0; y < height; ++y) {
                        Assert.True(canvas.Canvas[y, x] != default(char), $"Expected fill mark on {x}, {y}");
                    }
                }
            }
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 4)]
        [InlineData(1, 8)]
        public void ShouldOnlyFillNodesOnTheSameArea(int bucketX, int bucketY) {
            int width = 5;
            int height = 8;
            char color = 'c';
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                ICanvas canvas = container.Scope.Resolve<ICanvas>();
                canvas.Canvas = new char[height, width];
                canvas = DrawStraightLine(canvas, 2, 0, 2, 2);
                canvas = DrawStraightLine(canvas, 0, 3, 1, 3);
                canvas = DrawStraightLine(canvas, 0, 6, 4, 6);
                HashSet<(int, int)> fillNodes = GetFillNodes(canvas.Canvas, bucketX - 1, bucketY - 1);

                canvas = container.Module.Execute(new string[] { bucketX.ToString(), bucketY.ToString(), color.ToString() });

                for (int x = 0; x < width; ++x) {
                    for (int y = 0; y < height; ++y) {
                        if (fillNodes.Contains((x, y))) {
                            Assert.True(canvas.Canvas[y, x] == color, $"Expected fill mark on {x}, {y}");
                        }
                        else {
                            Assert.False(canvas.Canvas[y, x] == color, $"Unexpected fill mark on {x}, {y}");
                        }
                    }
                }
            }
        }

        [Theory]
        [InlineData("-1", "-1", "")]
        [InlineData("5", "1", "%%%%")]
        [InlineData("0", "0", " ")]
        [InlineData("999", "999", "c")]
        [InlineData("a", "b", "c")]
        [InlineData("1", "a", "1")]
        public void ShouldThrowArgumentExceptionOnInvalidInput(string x, string y, string color) {
            int size = 5;
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                ICanvas canvas = container.Scope.Resolve<ICanvas>();
                canvas.Canvas = new char[size, size];

                Assert.Throws<ArgumentException>(() =>
                {
                    container.Module.Execute(new string[] { x.ToString(), y.ToString(), color.ToString() });
                });
            }
        }

        private ICanvas DrawStraightLine(ICanvas data, int x1, int y1, int x2, int y2) {
            int xStart = Math.Min(x1, x2);
            int yStart = Math.Min(y1, y2);
            int xEnd = Math.Max(x1, x2);
            int yEnd = Math.Max(y1, y2);

            for (int i = yStart; i <= yEnd; ++i) {
                for (int j = xStart; j <= xEnd; ++j) {
                    data.Canvas[i, j] = 'x';
                }
            }

            return data;
        }

        private HashSet<(int, int)> GetFillNodes(char[,] nodes, int x, int y) {
            HashSet<(int, int)> fillNodes = new HashSet<(int, int)>();

            int width = nodes.GetLength(1);
            int height = nodes.GetLength(0);
            Queue<(int, int)> nodeQueue = new Queue<(int, int)>();
            nodeQueue.Enqueue((x, y));

            while (nodeQueue.Count > 0) {
                (int currentX, int currentY) = nodeQueue.Dequeue();
                if (currentX >= 0 && currentX < width &&
                    currentY >= 0 && currentY < height &&
                    nodes[currentY, currentX] == default(char) &&
                    !fillNodes.Contains((currentX, currentY))) {
                    fillNodes.Add((currentX, currentY));
                    nodeQueue.Enqueue((currentX - 1, currentY));
                    nodeQueue.Enqueue((currentX + 1, currentY));
                    nodeQueue.Enqueue((currentX, currentY - 1));
                    nodeQueue.Enqueue((currentX, currentY + 1));
                }
            }

            return fillNodes;
        }
    }
}
