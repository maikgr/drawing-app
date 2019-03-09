using Autofac;
using Drawing.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Drawing.Test {
    public class CanvasLineCommandTests : BaseCanvasCommandTests {
        protected override Type ClassToTest {
            get {
                return typeof(CanvasLineCommandModule);
            }
        }

        [Theory]
        [InlineData(1, 1, 5, 1)]
        [InlineData(5, 1, 1, 1)]
        public void ShouldCreateHorizontalLine(int x1, int y1, int x2, int y2) {
            int size = 5;
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                ICanvas canvas = container.Scope.Resolve<ICanvas>();
                canvas.Canvas = new char[size, size];

                canvas = container.Module.Execute(new string[] { x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString() });

                char lineMark = canvas.Canvas[y1 - 1, x1 - 1];
                int xPointer = Math.Min(x1, x2) - 1;
                int xEnd = Math.Max(x1, x2) - 1;
                int yLine = y1 - 1;
                for (int x = 0; x < size; ++x) {
                    for (int y = 0; y < size; ++y) {
                        if (x == xPointer && xPointer <= xEnd && y == yLine) {
                            Assert.True(lineMark == canvas.Canvas[y, x], $"Expected line mark {lineMark} on {x},{y}");
                            xPointer++;
                        }
                        else {
                            Assert.True(lineMark != canvas.Canvas[y, x], $"Unexpected line mark {lineMark} on {x}, {y}");
                        }
                    }
                }

            }
        }

        [Theory]
        [InlineData(1, 1, 1, 5)]
        [InlineData(1, 5, 1, 1)]
        public void ShouldCreateVerticalLine(int x1, int y1, int x2, int y2) {
            int size = 5;
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                ICanvas canvas = container.Scope.Resolve<ICanvas>();
                canvas.Canvas = new char[size, size];

                canvas = container.Module.Execute(new string[] { x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString() });

                char lineMark = canvas.Canvas[y1 - 1, x1 - 1];
                int yPointer = Math.Min(y1, y2) - 1;
                int yEnd = Math.Max(y1, y2) - 1;
                int xLine = x1 - 1;
                for (int x = 0; x < size; ++x) {
                    for (int y = 0; y < size; ++y) {
                        if (y == yPointer && yPointer <= yEnd && x == xLine) {
                            Assert.True(lineMark == canvas.Canvas[y, x], $"Expected line mark {lineMark} on {x},{y}");
                            yPointer++;
                        }
                        else {
                            Assert.True(lineMark != canvas.Canvas[y, x], $"Unexpected line mark {lineMark} on {x}, {y}");
                        }
                    }
                }
            }
        }

        [Theory]
        [InlineData(1, 1, 5, 5)]
        [InlineData(5, 5, 1, 1)]
        public void ShouldThrowArgumentExceptionOnDiagonalLine(int x1, int y1, int x2, int y2) {
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
                    container.Module.Execute(new string[] { x1, y1, x2, y2 });
                });
            }
        }
    }
}
