namespace CarAdverts.Core.Interfaces
{
    public interface IAppLogger<T>
    {
        void LogWarning(string message, params object[] args);
        void LogInformation(string message, params object[] args);
        void LogError(string message, params object[] args);
    }
}