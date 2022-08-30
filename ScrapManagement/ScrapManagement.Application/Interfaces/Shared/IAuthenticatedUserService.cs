namespace ScrapManagement.Application.Interfaces.Shared
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        public string Username { get; }
    }
}