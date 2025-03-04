﻿using Autofac;

namespace Drawing.Dependencies {
    public class AppModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterAssemblyTypes(typeof(AppModule).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
