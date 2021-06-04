using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace GigHub.Controllers
{
    [Authorize]
    public class Followings1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Followings1
        public IQueryable<Following> GetFollowings()
        {
            return db.Followings;
        }

        // GET: api/Followings1/5
        [ResponseType(typeof(Following))]
        public IHttpActionResult GetFollowing(string id)
        {
            Following following = db.Followings.Find(id);
            if (following == null)
            {
                return NotFound();
            }

            return Ok(following);
        }

        // PUT: api/Followings1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFollowing(string id, Following following)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != following.FollowerId)
            {
                return BadRequest();
            }

            db.Entry(following).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Followings1
        [ResponseType(typeof(Following))]
        public IHttpActionResult PostFollowing([FromBody] string followeeId)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var userId = User.Identity.GetUserId();
            var exist = db.Followings.Any(a => a.FolloweeId == userId && a.FolloweeId == followeeId);
            if (exist)
                return BadRequest("The Attendee Already exist!!");
            var following = new Following
            {
                FollowerId = User.Identity.GetUserId(),
                FolloweeId = followeeId
            };

            db.Followings.Add(following);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FollowingExists(following.FollowerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = following.FollowerId }, following);
        }

        // DELETE: api/Followings1/5
        [ResponseType(typeof(Following))]
        public IHttpActionResult DeleteFollowing(string id)
        {
            Following following = db.Followings.Find(id);
            if (following == null)
            {
                return NotFound();
            }

            db.Followings.Remove(following);
            db.SaveChanges();

            return Ok(following);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FollowingExists(string id)
        {
            return db.Followings.Count(e => e.FollowerId == id) > 0;
        }
    }
}