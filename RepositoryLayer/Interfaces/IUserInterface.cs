using Common.DTOs;
using DomainLayer.Models.Users;

namespace RepositoryLayer.Interfaces
{
    public interface IUserInterface
    {
        public UserDto TryLogin(string username, string password);
        public void AddNewUser(UserDto user);
        public void UpdateUserInfo(UserExtendedDetails userInfo);
    }
}
