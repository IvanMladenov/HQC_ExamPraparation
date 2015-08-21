namespace BuhtigIssueTracker
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using BuhtigIssueTracker.Contracts;

    public class InputHandler : IInputHandler
    {
        public InputHandler(string input)
        {
            int questionMark = input.IndexOf('?');
            if (questionMark != -1)
            {
                this.ActionName = input.Substring(0, questionMark);
                var pairs =
                    input.Substring(questionMark + 1)
                        .Split('&')
                        .Select(x => x.Split('=').Select(xx => WebUtility.UrlDecode(xx)).ToArray());
                var parameters = new Dictionary<string, string>();
                foreach (var pair in pairs)
                {
                    parameters.Add(pair[0], pair[1]);
                }

                this.Parameters = parameters;
            }
            else
            {
                this.ActionName = input;
            }
        }

        public string ActionName { get; set; }

        public IDictionary<string, string> Parameters { get; set; }
    }
}
