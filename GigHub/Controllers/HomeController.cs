using GigHub.Models;
using GigHub.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext context;
        public HomeController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcommingGigs = context.Gigs
                .Include(g => g.Genre)
                .Include(a => a.Artist)
                .Where(d => d.DateTime > DateTime.Now).ToList();

            //var homeViewModel = new HomeViewModel
            var homeViewModel = new GigViewModel

            {
                UpcomingGigs = upcommingGigs,
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Upcoming gigs"

            };

            //return View(homeViewModel);
            return View("Gigs", homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}