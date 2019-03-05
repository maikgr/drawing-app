using Autofac;
using Drawing.Dependencies;
using System;
using Xunit;

namespace Drawing.Test {
    /// <summary>
    /// Dependency injection container for testing
    /// </summary>
    /// <typeparam name="T">The interface to test</typeparam>
    public class BaseTestContainer<T> : IDisposable {
        public T Service { get; private set; }
        private IContainer container;
        private ILifetimeScope scope;

        public BaseTestContainer() {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<AppModule>();
            container = builder.Build();
            scope = container.BeginLifetimeScope();
        }

        public void Dispose() {
            scope.Dispose();
            container.Dispose();
            Service = default(T);
        }
    }
}
