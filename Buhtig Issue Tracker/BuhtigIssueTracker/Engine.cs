namespace BuhtigIssueTracker
{
    using System;

    using BuhtigIssueTracker.Contracts;

    public class Engine : IEngine
    {
        private readonly Dispatcher dispatcher;

        public Engine(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public Engine()
            : this(new Dispatcher())
        {
        }

        public void Run()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    break;
                }

                input = input.Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    try
                    {
                        var handledInput = new InputHandler(input);
                        string viewResult = this.dispatcher.DispatchAction(handledInput);
                        Console.WriteLine(viewResult);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
