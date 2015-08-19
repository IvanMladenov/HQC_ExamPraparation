namespace VehicleParkSystem
{
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    using VehicleParkSystem.Contracts;

    public class Command : ICommand
    {
        public Command(string commandLine)
        {
            this.CommandName = commandLine.Substring(0, commandLine.IndexOf(' '));
            this.Parameters =
                new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(
                    commandLine.Substring(commandLine.IndexOf(' ') + 1));
        }

        public string CommandName { get; private set; }

        public IDictionary<string, string> Parameters { get; private set; }
    }
}