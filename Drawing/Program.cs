using Autofac;
using Drawing.Dependencies;
using Drawing.Modules;
using System;

namespace Drawing {
    class Program {
        static void Main(string[] input) {
            IContainer container = ModuleLoader.LoadModule();
            ILifetimeScope scope = container.BeginLifetimeScope();
            IConsoleModule<ICanvas> canvasModule = scope.Resolve<IConsoleModule<ICanvas>>();

            while (true) {
                Console.Write("Enter Command: ");
                string[] args = Console.ReadLine().Split(' ');

                if (args == null || args.Length == 0) {
                    Console.WriteLine("Parameter format is needed");
                }

                if (!canvasModule.Commands.ContainsKey(args[0])) {
                    Console.WriteLine("ERROR: Unrecognized parameter");
                    Console.WriteLine(canvasModule.CommandsHelp);
                }
                else {
                    try {
                        canvasModule.Commands[args[0]](args);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
