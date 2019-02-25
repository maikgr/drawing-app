using Autofac;

namespace Drawing.Dependencies {
    public class ModuleLoader {
        public static IContainer LoadModule() {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<AppModule>();

            return builder.Build();
        }
    }
}
