namespace VehicleParkSystem.Contracts
{
    using System.Collections.Generic;

    public interface ICommand
    {
        string CommandName { get; }

        IDictionary<string, string> Parameters { get; }
    }
}
