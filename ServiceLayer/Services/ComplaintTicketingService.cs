using Common.Enum;
using RepositoryLayer.Repository;

namespace ServiceLayer.Services
{
    public class ComplaintTicketingService
    {
        private readonly ComplaintTicketingRepo _complaintTicketingRepo;

        public ComplaintTicketingService(ComplaintTicketingRepo complaintTicketingRepo)
        {
            _complaintTicketingRepo = complaintTicketingRepo;
        }

        public void Login(string username, string password)
        {
           _complaintTicketingRepo.TryLogin(username, password);
        }

        public void EditComplaint(ComplaintDto complaint)
        {
            _complaintTicketingRepo.Edit(complaint);
        }

        public List<ComplaintDto> GetByComplaintId(int complaintId)
        {
            return _complaintTicketingRepo.GetComplaintById(complaintId);
        }

        public List<ComplaintDto> GetByUserId(int userId)
        {
            return _complaintTicketingRepo.GetByUserId(userId);
        }

        public void Add(ComplaintDto complaint)
        {
            _complaintTicketingRepo.Add(complaint);
        }

        public void UpdateComplaintStatus(int complaintId, ComplaintStatus status)
        {
            _complaintTicketingRepo.UpdateComplaintStatus(complaintId, status);
        }

        public List<ComplaintDto> GetUsersComplaints()
        {
            return _complaintTicketingRepo.GetUsersComplaints();
        }

        public List<ComplaintDto> GetComplaintById(int complaintId)
        {
            return _complaintTicketingRepo.GetComplaintById(complaintId);
        }
    }
}
