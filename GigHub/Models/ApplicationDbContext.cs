using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("GigHubConn", throwIfV1Schema: false)
        {
        }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //To enable WillCascadeOnDelete on Related Entities
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>().HasRequired(a => a.Gig).WithMany().WillCascadeOnDelete(false);

            //
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Followers).WithRequired(f => f.Followee).WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Followees).WithRequired(f => f.Follower).WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}