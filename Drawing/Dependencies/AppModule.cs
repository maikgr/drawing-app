using Autofac;
using Drawing.Modules;

namespace Drawing.Dependencies {
    public class AppModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterAssemblyTypes(typeof(AppModule).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<ConsoleCanvas>()
                .As<ICanvas>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
