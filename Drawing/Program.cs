using Autofac;
using Drawing.Dependencies;
using Drawing.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drawing {
    class Program {
        static void Main(string[] input) {
            IContainer container = ModuleLoader.LoadModule();
            ILifetimeScope scope = container.BeginLifetimeScope();
            ICanvas canvas = scope.Resolve<ICanvas>();
            IEnumerable<ICommandModule<ICanvas>> commands = scope.Resolve<IEnumerable<ICommandModule<ICanvas>>>();
            IDictionary<string, ICommandModule<ICanvas>> commandMap = new Dictionary<string, ICommandModule<ICanvas>>();
            
            foreach(ICommandModule<ICanvas> command in commands) {
                commandMap.Add(command.Command.ToUpper(), command);
            }

            while (true) {
                Console.Write("Enter Command: ");
                string[] args = Console.ReadLine().Split(' ');

                if (args == null || args.Length == 0) {
                    Console.WriteLine("Parameter format is needed");
                }

                if (!commandMap.ContainsKey(args[0])) {
                    Console.WriteLine("ERROR: Unrecognized command");
                    foreach(ICommandModule<ICanvas> command in commands) {
                        Console.WriteLine(command.Command + " " + string.Join(" ", command.Parameters));
                        Console.WriteLine("\t" + command.Description);
                    }
                }
                else {
                    try {
                        ICommandModule<ICanvas> command = commandMap[args[0].ToUpper()];
                        string[] param = args.Skip(1).Take(args.Length - 1).ToArray();

                        if (param.Length != command.Parameters.Length) {
                            Console.WriteLine("ERROR: Invalid command syntax");
                            Console.WriteLine($"Command syntax: {command.Command} {string.Join(" ", command.Parameters)}");
                        }
                        else {
                            command.Execute(canvas, args);
                        }
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
