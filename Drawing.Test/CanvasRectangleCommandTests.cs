using Autofac;
using Drawing.Modules;
using System;
using System.Collections.Generic;
using Xunit;

namespace Drawing.Test {
    public class CanvasRectangleCommandTests : BaseCanvasCommandTests {
        protected override Type ClassToTest {
            get {
                return typeof(CanvasRectangleCommandModule);
            }
        }

        [Theory]
        [InlineData(1, 1, 4, 4)]
        [InlineData(3, 2, 1, 1)]
        [InlineData(2, 1, 5, 1)]
        [InlineData(5, 2, 3, 2)]
        public void ShouldCreateRectangle(int x1, int y1, int x2, int y2) {
            int size = 5;
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                ICanvas canvas = container.Scope.Resolve<ICanvas>();
                canvas.Canvas = new char[size, size];

                canvas = container.Module.Execute(new string[] { x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString() });

                HashSet<(int, int)> rectNodes = GetRectNodes(x1, y1, x2, y2);
                Assert.NotNull(canvas.Canvas);

                for (int x = 0; x < size; ++x) {
                    for (int y = 0; y < size; ++y) {
                        if (rectNodes.Contains((x, y))) {
                            Assert.True(canvas.Canvas[y, x] != default(char), $"Expected rectangle mark on {x}, {y}");
                        }
                        else {
                            Assert.True(canvas.Canvas[y, x] == default(char), $"Unexpected rectangle mark on {x}, {y}");
                        }
                    }
                }
            }
        }

        [Theory]
        [InlineData("-1", "-1", "5", "5")]
        [InlineData("5", "4", "-5", "-5")]
        [InlineData("0", "0", "0", "0")]
        [InlineData("999", "999", "999", "999")]
        [InlineData("a", "b", "1", "1")]
        [InlineData("1", "a", "1", "b")]
        public void ShouldThrowArgumentExceptionOnInvalidInput(string x1, string y1, string x2, string y2) {
            int size = 5;
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                ICanvas canvas = container.Scope.Resolve<ICanvas>();
                canvas.Canvas = new char[size, size];

                Assert.Throws<ArgumentException>(() =>
                {
                    container.Module.Execute(new string[] { x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString() });
                });
            }
        }

        private HashSet<(int, int)> GetRectNodes(int x1, int y1, int x2, int y2) {
            HashSet<(int, int)> rectLineSet = GetLineNodes(x1 - 1, y1 - 1, x2 - 1, y1 - 1);
            rectLineSet.UnionWith(GetLineNodes(x1 - 1, y2 - 1, x1 - 1, y1 - 1));
            rectLineSet.UnionWith(GetLineNodes(x1 - 1, y2 - 1, x2 - 1, y2 - 1));
            rectLineSet.UnionWith(GetLineNodes(x2 - 1, y2 - 1, x2 - 1, y1 - 1));

            return rectLineSet;
        }

        private HashSet<(int, int)> GetLineNodes(int x1, int y1, int x2, int y2) {
            int xStart = Math.Min(x1, x2);
            int yStart = Math.Min(y1, y2);
            int xEnd = Math.Max(x1, x2);
            int yEnd = Math.Max(y1, y2);
            HashSet<(int, int)> lineNodes = new HashSet<(int, int)>();

            for (int i = yStart; i <= yEnd; ++i) {
                for (int j = xStart; j <= xEnd; ++j) {
                    lineNodes.Add((j, i));
                }
            }

            return lineNodes;
        }
    }
}
