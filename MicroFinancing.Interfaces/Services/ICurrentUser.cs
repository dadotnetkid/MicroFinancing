namespace MicroFinancing.Interfaces.Services;

public interface ICurrentUser
{
    string UserId { get; }
    string FullName { get; }
}