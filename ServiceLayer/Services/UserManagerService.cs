using Common.DTOs;
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

        public void Login(string username, string password)
        {
            _userRepo.TryLogin(username, password);
        }

        public void AddNewUser(string username, string password)
        {
            _userRepo.AddNewUser(username, password);
        }

        public void UpdateUserInfo(UserDto user)
        {
            _userRepo.UpdateUserInfo(user);
        }
    }
}
