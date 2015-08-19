namespace VehicleParkSystem
{
    using System;

    using VehicleParkSystem.Contracts;

    public class Engine : IEngine
    {
        private readonly CommandExecutioner executioner;

        public Engine(CommandExecutioner executioner)
        {
            this.executioner = executioner;
        }

        public Engine()
            : this(new CommandExecutioner())
        {
        }

        public void Run()
        {
            while (true)
            {
                string commandLine = Console.ReadLine();
                if (commandLine == null)
                {
                    break;
                }

                commandLine = commandLine.Trim();

                if (!string.IsNullOrEmpty(commandLine))
                {
                    try
                    {
                        var command = new Command(commandLine);
                        string commandResult = this.executioner.Execute(command);
                        Console.WriteLine(commandResult);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }
        }
    }
}
