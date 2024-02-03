using Common.Enum;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace Complaint_ticketing.Controllers
{
    [Route("api/ComplaintTicketing")]
    [ApiController]
    public class ComplaintTicketingController : ControllerBase
    {
        [HttpPost(nameof(Login))]
        public IActionResult Login(string userName, string password, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            complaintTicketingService.Login(userName, password);
            return Ok();
        }

        [HttpPost(nameof(AddComplaint))]
        public IActionResult AddComplaint(ComplaintDto complaint, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            complaintTicketingService.Add(complaint);
            return Ok();
        }

        [HttpPost(nameof(EditComplaint))]
        public IActionResult EditComplaint(ComplaintDto complaint, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            complaintTicketingService.EditComplaint(complaint);
            return Ok();
        }

        [HttpPost(nameof(UpdateComplaintStatus))]
        public IActionResult UpdateComplaintStatus(int id, ComplaintStatus status, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            complaintTicketingService.UpdateComplaintStatus(id, status);
            return Ok();
        }

        [HttpGet(nameof(GetUsersComplaints))]
        public List<ComplaintDto> GetUsersComplaints([FromServices] ComplaintTicketingService complaintTicketingService)
        {
            return complaintTicketingService.GetUsersComplaints();
        }

        [HttpGet(nameof(GetByUserId))]
        public List<ComplaintDto> GetByUserId(int userId, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            return complaintTicketingService.GetByUserId(userId);
        }

        [HttpGet(nameof(GetComplaintById))]
        public List<ComplaintDto> GetComplaintById(int complaintId, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            return complaintTicketingService.GetComplaintById(complaintId);
        }
    }
}
