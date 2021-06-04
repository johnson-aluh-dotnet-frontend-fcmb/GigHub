using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        public readonly ApplicationDbContext context;
        public GigsController()
        {
            context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Genre)
                .Include(a => a.Artist)
                .ToList();
            var gigViewModel = new GigViewModel
            {
                UpcomingGigs = gigs,
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Gigs i'm attending"

            };
            //return View(gigViewModel);
            return View("Gigs", gigViewModel);

        }
        // GET: Default
        public ActionResult Create()
        {
            var genre = new GigFormViewModel
            {
                Genres = context.Genres.ToList()
            };
            return View(genre);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel gigFormViewModel)
        {
            //var artistId = User.Identity.GetUserId();
            //var artist = context.Users.SingleOrDefault(u => u.Id == artistId);
            //var genre = context.Genres.SingleOrDefault(g => g.Id == gigFormViewModel.Genre);
            if (!ModelState.IsValid)
            {
                gigFormViewModel.AlertMsg = "All fieds reuired must be filled";
                ViewBag.AlertMsg = gigFormViewModel.AlertMsg;

                gigFormViewModel.Genres = context.Genres.ToList();
                return View("Create", gigFormViewModel);
            }

            var gig = new Gig
            {
                //Artist = artist,
                ArtistId = User.Identity.GetUserId(),
                DateTime = gigFormViewModel.GetDateTime(),
                //Genre = genre,
                GenreId = gigFormViewModel.Genre,
                Venue = gigFormViewModel.Venue
            };

            context.Gigs.Add(gig);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}