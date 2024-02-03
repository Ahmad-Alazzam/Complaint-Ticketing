using Common.Enum;
using DomainLayer.Models.Complaints;

namespace RepositoryLayer.Interfaces
{
    public interface IComplaintTicketingInterface
    {
        public void TryLogin(string username, string password);
        void Add(ComplaintDto complaint);
        void Edit(ComplaintDto complaint);
        void UpdateComplaintStatus(int complaintId, ComplaintStatus status);
        List<ComplaintDto> GetUsersComplaints();
        List<ComplaintDto> GetComplaintById(int complaintId);
        List<ComplaintDto> GetByUserId(int userId);
    }
}
