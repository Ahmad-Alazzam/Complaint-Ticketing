using Common.Enum;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace Complaint_ticketing.Controllers
{
    [Route("api/ComplaintTicketing")]
    [ApiController]
    public class ComplaintTicketingController : ControllerBase
    {
        [HttpPost(nameof(AddComplaint))]
        public IActionResult AddComplaint(ComplaintDto complaint, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            try
            {
                complaintTicketingService.Add(complaint);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(EditComplaint))]
        public IActionResult EditComplaint(ComplaintDto complaint, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            try
            {
                complaintTicketingService.EditComplaint(complaint);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(DeleteComplaint))]
        public IActionResult DeleteComplaint(int complaintId, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            try
            {
                complaintTicketingService.DeleteComplaint(complaintId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(DeleteDemand))]
        public IActionResult DeleteDemand(int demandId, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            try
            {
                complaintTicketingService.DeleteDemand(demandId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(UpdateComplaintStatus))]
        public IActionResult UpdateComplaintStatus(int id, ComplaintStatus status, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            try
            {
                complaintTicketingService.UpdateComplaintStatus(id, status);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(GetUsersComplaints))]
        public ActionResult<List<ComplaintDto>> GetUsersComplaints([FromServices] ComplaintTicketingService complaintTicketingService)
        {
            try
            {
                return Ok(complaintTicketingService.GetUsersComplaints());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(GetByUserId))]
        public ActionResult<List<ComplaintDto>> GetByUserId(int userId, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            try
            {
                return Ok(complaintTicketingService.GetByUserId(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(GetComplaintById))]
        public ActionResult<List<ComplaintDto>> GetComplaintById(int complaintId, [FromServices] ComplaintTicketingService complaintTicketingService)
        {
            try
            {
                return Ok(complaintTicketingService.GetComplaintById(complaintId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}