namespace BangaloreUniversityLearningSystem.Contracts
{
    public interface IView
    {
        object Model { get; }

        string Display();
    }
}
