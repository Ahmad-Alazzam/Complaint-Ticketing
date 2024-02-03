using Common.DTOs;

namespace RepositoryLayer.Interfaces
{
    public interface IUserInterface
    {
        public void TryLogin(string username, string password);
        public void AddNewUser(string username, string password);
        public void UpdateUserInfo(UserDto user);
    }
}
