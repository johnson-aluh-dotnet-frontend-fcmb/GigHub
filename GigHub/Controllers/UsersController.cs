using GigHub.Models;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class UsersController : Controller
    {
        public readonly ApplicationDbContext context;
        public UsersController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Users
        public ActionResult Index()
        {
            var user = context.Users.ToString();
            return View(user);
        }
    }
}