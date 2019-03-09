using System;
using Xunit;

namespace Drawing.Test {
    public abstract class BaseCanvasCommandTests {
        protected abstract Type ClassToTest { get; }

        [Fact]
        public void CommandIsNotNull() {
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                Assert.NotNull(container.Module.Command);
            }
        }

        [Fact]
        public void ParametersIsNotNull() {
            using (CommandTestContainer container = new CommandTestContainer(ClassToTest)) {
                Assert.NotNull(container.Module.Parameters);
            }
        }
    }
}
