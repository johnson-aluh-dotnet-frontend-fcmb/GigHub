using GigHub.DTOs;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext context;
        public FollowingsController()
        {
            context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        //public IHttpActionResult Follow([FromBody] string followeeId)
        {
            var userId = User.Identity.GetUserId();
            //var exist = context.Followings.Any(a => a.FolloweeId == userId && a.FolloweeId == followeeId);
            var exist = context.Followings.Any(a => a.FolloweeId == userId && a.FolloweeId == dto.FolloweeId);
            if (exist)
                return BadRequest("The Attendee Already exist!!");
            var following = new Following
            {
                FollowerId = userId,
                //FolloweeId = followeeId
                FolloweeId = dto.FolloweeId
            };
            context.Followings.Add(following);
            context.SaveChanges();

            return Ok();
        }
        [HttpGet]
        public IHttpActionResult AllFollower()
        {
            return Ok(context.Followings.ToList());
        }
    }
}
