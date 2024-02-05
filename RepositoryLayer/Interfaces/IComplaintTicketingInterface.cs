using Common.Enum;

namespace RepositoryLayer.Interfaces
{
    public interface IComplaintTicketingInterface
    {
        void Add(ComplaintDto complaint);
        void Edit(ComplaintDto complaint);
        void DeleteComplaint(int complaintId);
        void DeleteDemand(int demandId);
        void UpdateComplaintStatus(int complaintId, ComplaintStatus status);
        List<ComplaintDto> GetUsersComplaints();
        List<ComplaintDto> GetComplaintById(int complaintId);
        List<ComplaintDto> GetByUserId(int userId);
    }
}
