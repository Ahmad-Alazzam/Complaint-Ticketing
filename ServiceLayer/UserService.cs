using Common.DTOs;
using RepositoryLayer.Repository;

namespace ServiceLayer
{
    public class UserService
    {
        private readonly UserRepo _userRepo;

        public UserService(UserRepo userRepo)
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
