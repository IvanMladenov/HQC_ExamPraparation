namespace BuhtigIssueTracker
{
    using System.Globalization;
    using System.Threading;

    public class BuhtigIssueTrackerMain
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var engine = new Engine();
            engine.Run();
        }
    }
}