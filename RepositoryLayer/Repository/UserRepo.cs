using AutoMapper;
using Common.DTOs;
using Common.Enum;
using DomainLayer.Models.Users;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppDbContexts;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Repository
{
    public class UserRepo : IUserInterface
    {
        private readonly AppDbContext _db;
        public readonly IMapper _mapper;
        public UserContext _user;

        public UserRepo(AppDbContext dbContext, IMapper mapper, UserContext userContext)
        {
            _db = dbContext;
            _mapper = mapper;
            _user = userContext;
        }

        public void AddNewUser(string username, string password)
        {
            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
                throw new Exception("Invalid Data!!");

            var userNameIsTaken = _db.Users.Any(u => u.UserName == username);

            if (userNameIsTaken)
                throw new Exception("User Name is Taken, Please try another!!");

            var newUser = new User()
            {
                UserName = username,
                Password = password,
                UserType = UserTypeEnum.User
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();
        }

        public void UpdateUserInfo(UserDto user)
        {
            if (user.Id != _user.CurrentUser.Id)
                throw new Exception("You Don't Have Permetion!!");

            var userItem = _db.Users.SingleOrDefault(u => u.Id == _user.CurrentUser.Id);

            if (userItem == null)
                throw new Exception("User Was Not Found!!");

            userItem.UserDetails = _mapper.Map<UserExtendedDetails>(user.UserDetails);
            _db.SaveChanges();
        }

        public void TryLogin(string userName, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.UserName == userName && x.Password == password);

            if (user == null)
                throw new Exception("Invalid User Info!!");

            _user.CurrentUser = user;
        }
    }
}
