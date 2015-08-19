namespace VehicleParkSystem
{
    using System.Globalization;
    using System.Threading;

    public class VehicleParkMain
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var engine = new Engine();
            engine.Run();
        }
    }
}