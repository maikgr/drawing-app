using Autofac;
using Drawing.Dependencies;
using Drawing.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drawing.Test {
    public class CommandTestContainer : IDisposable {
        public ICommandModule<ICanvas> Module { get; private set; }
        public ILifetimeScope Scope { get; set; }
        private IContainer Container { get; set; }

        public CommandTestContainer(Type classToTest) {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<AppModule>();
            Container = builder.Build();
            Scope = Container.BeginLifetimeScope();
            var commands = Scope.Resolve<IEnumerable<ICommandModule<ICanvas>>>();
            Module = commands.FirstOrDefault(com => com.GetType().Equals(classToTest));
        }

        public void Dispose() {
            Module = null;
            Scope.Dispose();
            Container.Dispose();
        }
    }
}
