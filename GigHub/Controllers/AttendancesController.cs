using GigHub.DTOs;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext context;
        public AttendancesController()
        {
            context = new ApplicationDbContext();
        }
        [HttpPost]
        //public IHttpActionResult Attend([FromBody] int gigId)
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exist = context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);
            //var exist = context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigId);
            if (exist)
                return BadRequest("The Attendee Already exist!!");
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                //GigId = gigId,
                AttendeeId = User.Identity.GetUserId()
            };
            context.Attendances.Add(attendance);
            context.SaveChanges();

            return Ok();
        }
    }
}
