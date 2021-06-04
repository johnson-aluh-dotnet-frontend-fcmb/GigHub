using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModel
{
    public class HomeViewModel
    {
        public List<Gig> UpcomingGigs { get; internal set; }
        public bool ShowAction { get; internal set; }
    }
}