using AutoMapper;
using Common.Enum;
using DomainLayer.Models.Complaints;
using DomainLayer.Models.Demands;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppDbContexts;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Repository
{
    public class ComplaintTicketingRepo : IComplaintTicketingInterface
    {
        private readonly AppDbContext _db;
        public readonly IMapper _mapper;
        public UserContext _user;
        public ComplaintTicketingRepo(AppDbContext dbContext, IMapper mapper, UserContext userContext)
        {
            _db = dbContext;
            _mapper = mapper;
            _user = userContext;
        }

        public void TryLogin(string userName, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.UserName == userName && x.Password == password);

            if (user == null)
                throw new Exception("Invalid User Info!!");

            _user.CurrentUser = user;
        }

        public void Edit(ComplaintDto complaint)
        {
            if (complaint == null || complaint.ComplaintText.IsNullOrEmpty())
                throw new Exception("Invalid Data!!");

            var complaintItem = _db.Complaints.Where(c => c.Id == complaint.Id).SingleOrDefault();

            if(complaintItem == null)
                throw new Exception("Data was not found!!");

            if(complaintItem.UserId != _user.CurrentUser.Id)
                throw new Exception("You Don't Have Permetion!!");

            complaintItem.Demands = _mapper.Map<List<Demand>>(complaint.Demands);

            switch (complaint.SelectedLanguage)
            {
                case Common.Enum.Language.Arabic:
                    complaintItem.ComplaintTextAr = complaint.ComplaintTextAr;
                    break;
                case Common.Enum.Language.English:
                    complaintItem.ComplaintTextEn = complaint.ComplaintTextEn;
                    break;
            }

            _db.SaveChanges();
        }

        public List<ComplaintDto> GetComplaintById(int complaintId)
        {
            var result = _db.Complaints.Where(c => c.Id == complaintId).ToList();
            return _mapper.Map<List<ComplaintDto>>(result);
        }

        public List<ComplaintDto> GetByUserId(int userId)
        {
            var result = _db.Complaints.Where(c => c.UserId == userId).ToList();
            return _mapper.Map<List<ComplaintDto>>(result);
        }

        public void Add(ComplaintDto complaint)
        {
            var complaintItem = _mapper.Map<Complaint>(complaint);
            complaintItem.UserId = _user.CurrentUser.Id;
            _db.Complaints.Add(complaintItem);
            _db.SaveChanges();
        }

        public List<ComplaintDto> GetUsersComplaints()
        {
            if (_user == null || _user.CurrentUser.UserType != UserTypeEnum.Administrator)
                throw new Exception("You Don't have permetion!!");

            var result = _db.Complaints
                .Include(c => c.Demands)
                .ToList();

            return _mapper.Map<List<ComplaintDto>>(result); 
        }

        public void UpdateComplaintStatus(int complaintId, ComplaintStatus status)
        {
            if (_user.CurrentUser.UserType != UserTypeEnum.Administrator)
                throw new Exception("You Don't have permetion!!");

            var complain = _db.Complaints.Where(c => c.Id == complaintId).SingleOrDefault();

            if (complain == null)
                throw new Exception("Data was not found!!");

            complain.Status = status;

            _db.SaveChanges();
        }
    }
}
