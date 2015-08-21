namespace BuhtigIssueTracker
{
    using System;

    using BuhtigIssueTracker.Contracts;

    public class Dispatcher
    {
        public Dispatcher(IIssueTracker tracker)
        {
            this.Tracker = tracker;
        }

        public Dispatcher()
            : this(new IssueTracker())
        {
        }

        private IIssueTracker Tracker { get; set; }

        public string DispatchAction(IInputHandler inputHandler)
        {
            switch (inputHandler.ActionName)
            {
                case "RegisterUser":
                    return this.Tracker.RegisterUser(
                        inputHandler.Parameters["username"], 
                        inputHandler.Parameters["password"], 
                        inputHandler.Parameters["confirmPassword"]);
                case "LoginUser":
                    return this.Tracker.LoginUser(inputHandler.Parameters["username"], inputHandler.Parameters["password"]);
                case "LogoutUser":
                    return this.Tracker.LogoutUser();
                case "CreateIssue":
                    return this.Tracker.CreateIssue(
                        inputHandler.Parameters["title"], 
                        inputHandler.Parameters["description"], 
                        (IssuePriority)Enum.Parse(typeof(IssuePriority), inputHandler.Parameters["priority"], true), 
                        inputHandler.Parameters["tags"].Split('|'));
                case "RemoveIssue":
                    return this.Tracker.RemoveIssue(int.Parse(inputHandler.Parameters["id"]));
                case "AddComment":
                    return this.Tracker.AddComment(int.Parse(inputHandler.Parameters["id"]), inputHandler.Parameters["text"]);
                case "MyIssues":
                    return this.Tracker.GetMyIssues();
                case "MyComments":
                    return this.Tracker.GetMyComments();
                case "Search":
                    return this.Tracker.SearchForIssues(inputHandler.Parameters["tags"].Split('|'));
                default:
                    return string.Format("Invalid action: {0}", inputHandler.ActionName);
            }
        }
    }
}