using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModel
{
    public class GigViewModel
    {
        public List<Gig> UpcomingGigs { get; internal set; }
        public bool ShowAction { get; internal set; }
        public string Heading { get; set; }
    }
}