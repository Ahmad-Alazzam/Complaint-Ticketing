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

        public void Update(ComplaintDto complaint)
        {
            if (complaint == null || complaint.ComplaintText.IsNullOrEmpty())
                throw new Exception("Invalid Data!!");

            var complaintItem = _db.Complaints.SingleOrDefault(c => c.Id == complaint.Id);

            if(complaintItem == null)
                throw new Exception("Data was not found!!");

            if(complaintItem.UserId != _user.CurrentUser.Id)
                throw new Exception("You Don't Have Permetion!!");

            complaintItem.Demands = _mapper.Map<List<Demand>>(complaint.Demands);

            switch (complaint.SelectedLanguage)
            {
                case Language.Arabic:
                    complaintItem.ComplaintTextAr = complaint.ComplaintTextAr;
                    break;
                case Language.English:
                    complaintItem.ComplaintTextEn = complaint.ComplaintTextEn;
                    break;
            }

            _db.SaveChanges();
        }

        public void UpdateDemand(DemandDto demand)
        {
            if (demand == null || demand.DemandText.IsNullOrEmpty())
                throw new Exception("Invalid Data!!");

            var demandItem = _db.Demands.SingleOrDefault(c => c.Id == demand.Id && c.IsDeleted == false);

            if (demand == null)
                throw new Exception("Data was not found!!");

            var complaintItem = _db.Complaints.FirstOrDefault(d => d.Id == demandItem.ComplaintId);

            if (complaintItem == null)
                throw new Exception("Data was not found!!");

            if (complaintItem.UserId != _user.CurrentUser.Id)
                throw new Exception("You Don't Have Permetion!!");

            switch (demand.SelectedLanguage)
            {
                case Language.Arabic:
                    demandItem.DemandTextAr = demand.DemandTextAr;
                    break;
                case Language.English:
                    demandItem.DemandTextEn = demand.DemandTextEn;
                    break;
            }

            _db.SaveChanges();
        }

        public void DeleteComplaint(int complaintId)
        {
            var complaintItem = _db.Complaints.SingleOrDefault(c => c.Id == complaintId);

            if (complaintItem == null)
                throw new Exception("Data was not found!!");

            if (complaintItem.UserId != _user.CurrentUser.Id)
                throw new Exception("You Don't Have Permetion!!");

            _db.Complaints.Attach(complaintItem);

            complaintItem.IsDeleted = true;

            _db.Entry(complaintItem).Property(d => d.IsDeleted).IsModified = true;

            _db.SaveChanges();
        }

        public void DeleteDemand(int demandId)
        {
            var demandsItem = _db.Demands
                .Where(c => c.Id == demandId)
                .Select(d => new Demand()
                {
                    Id = d.Id,
                    IsDeleted = d.IsDeleted
                }).FirstOrDefault();

            if (demandsItem == null)
                throw new Exception("Data was not found!!");

            _db.Demands.Attach(demandsItem);

            demandsItem.IsDeleted = true;

            _db.Entry(demandsItem).Property(d => d.IsDeleted).IsModified = true;

            _db.SaveChanges();
        }

        public List<ComplaintDto> GetComplaintById(int complaintId)
        {
            var result = _db.Complaints.Include(d => d.Demands)
                         .FirstOrDefault(c => c.Id == complaintId && c.IsDeleted == false);


            if (result == null)
                throw new Exception("Data was not found!!");

            return _mapper.Map<List<ComplaintDto>>(result);
        }

        public List<ComplaintDto> GetByUserId(int userId)
        {
            var result = _db.Complaints.Include(d => d.Demands)
                        .Where(c => c.UserId == userId && c.IsDeleted == false)
                        .ToList();

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
            if (_user.CurrentUser == null || _user.CurrentUser.UserType != UserTypeEnum.Administrator)
                throw new Exception("You Don't have permetion!!");

            var result = _db.Complaints
                .Include(c => c.Demands)
                .Where(c => c.IsDeleted == false)
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