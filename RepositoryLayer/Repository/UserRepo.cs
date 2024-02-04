using AutoMapper;
using Common.DTOs;
using Common.Enum;
using DomainLayer.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppDbContexts;
using RepositoryLayer.Interfaces;
using System.Text.RegularExpressions;

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

        public void AddNewUser(UserDto user)
        {
            if (InvalidUserInput(user))
                throw new Exception("Invalid Data!!");

            var userNameIsTaken = _db.Users.Any(u => u.UserName == user.UserName);

            if (userNameIsTaken)
                throw new Exception("User Name is Taken, Please try another!!");

            var newUser = new User()
            {
                UserName = user.UserName,
                Password = user.Password,
                UserType = UserTypeEnum.User,
                UserDetails = new UserExtendedDetails()
                {
                    DateOfBirth = user.UserDetails.DateOfBirth,
                    Email = user.UserDetails.Email,
                    Name = user.UserDetails.Name,
                    PhoneNumber = user.UserDetails.PhoneNumber,
                }
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();
        }

        public void UpdateUserInfo(UserExtendedDetails userInfo)
        {
            if (userInfo.UserId != _user.CurrentUser.Id)
                throw new Exception("You Don't Have Permetion!!");

            var userItem = _db.Users.SingleOrDefault(u => u.Id == _user.CurrentUser.Id);

            if (userItem == null)
                throw new Exception("User Was Not Found!!");

            userItem.UserDetails = _mapper.Map<UserExtendedDetails>(userInfo);
            _db.SaveChanges();
        }

        public UserDto TryLogin(string userName, string password)
        {
            var user = _db.Users.Include(e => e.UserDetails).SingleOrDefault(x => x.UserName == userName && x.Password == password);

            if (user == null)
                throw new Exception("Invalid User Info!!");

            _user.CurrentUser = user;

            return _mapper.Map<UserDto>(user);
        }

        public bool InvalidUserInput(UserDto user)
        {
            return user.UserName.IsNullOrEmpty() || user.Password.IsNullOrEmpty() ||
                user.UserDetails.PhoneNumber.Length > 10 ||
                IsValidEmail(user.UserDetails.Email) == false ||
                ValidateName(user.UserDetails.Name) == false;
        }

        public bool IsValidEmail(string email)
        {
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            return validateEmailRegex.IsMatch(email);
        }

        static bool ValidateName(string name)
        {
            Regex validateNameRegex = new Regex("^[A-Za-z\\s]+$");
            return validateNameRegex.IsMatch(name);
        }
    }
}
