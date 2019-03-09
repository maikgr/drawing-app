using System;
using System.Collections.Generic;
using System.Text;
using Drawing.Modules;
using Xunit;

namespace Drawing.Test {
    public class CanvasCreateCommandTests : BaseCanvasCommandTests {

        protected override Type ClassToTest {
            get {
                return typeof(CanvasCreateCommandModule);
            }
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(20, 5)]
        [InlineData(5, 30)]
        [InlineData(100, 100)]
        [InlineData(1000, 1000)]
        public void ShouldCreateArrays(int width, int height) {
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                ICanvas canvas = container.Module.Execute(new string[] { width.ToString(), height.ToString() });

                Assert.NotNull(canvas.Canvas);
                Assert.Equal(width, canvas.Canvas.GetLength(1));
                Assert.Equal(height, canvas.Canvas.GetLength(0));
            }
        }

        [Theory]
        [InlineData(20, 4, 50, 50)]
        [InlineData(100, 100, 1, 1)]
        public void ShouldRecreateArraysWithNewDimension(int oldWidth, int oldHeight, int newWidth, int newHeight) {
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                container.Module.Execute(new string[] { oldWidth.ToString(), oldHeight.ToString() });
                ICanvas canvas = container.Module.Execute(new string[] { newWidth.ToString(), newHeight.ToString() });

                Assert.NotNull(canvas.Canvas);
                Assert.Equal(newWidth, canvas.Canvas.GetLength(1));
                Assert.Equal(newHeight, canvas.Canvas.GetLength(0));
            }
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "20")]
        [InlineData("20", null)]
        [InlineData("-10", "-3")]
        [InlineData("0", "0")]
        [InlineData("a", "b")]
        public void ShouldThrowArgumentExceptionOnInvalidInput(string width, string height) {
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                Assert.Throws<ArgumentException>(() =>
                {
                    container.Module.Execute(new string[] { width, height });
                });
            }
        }
    }
}
