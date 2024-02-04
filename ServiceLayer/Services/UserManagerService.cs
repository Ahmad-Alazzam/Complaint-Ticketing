using Common.DTOs;
using DomainLayer.Models.Users;
using RepositoryLayer.Repository;

namespace ServiceLayer.Services
{
    public class UserManagerService
    {
        private readonly UserRepo _userRepo;

        public UserManagerService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public UserDto Login(string username, string password)
        {
            return _userRepo.TryLogin(username, password);
        }

        public void AddNewUser(UserDto user)
        {
            _userRepo.AddNewUser(user);
        }

        public void UpdateUserInfo(UserExtendedDetails userInfo)
        {
            _userRepo.UpdateUserInfo(userInfo);
        }
    }
}
