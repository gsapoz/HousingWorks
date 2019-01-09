using HWCodeAssgn.Models;
using Microsoft.EntityFrameworkCore;

namespace HWCodeAssgn.Data
{
    public class SiteContext : DbContext
    {
        public SiteContext(DbContextOptions<SiteContext> options) : base(options){}

        /* Creates a DbSet Property for "Profile" Entity Set (Profiles Table) */
        public DbSet<Profile> Profile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Overrides default behavior so table will be initialized as "Profile" instead of "Profiles" */
            modelBuilder.Entity<Profile>().ToTable("Profile"); 
        }
    }


}


