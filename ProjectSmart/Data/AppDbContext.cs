using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectSmart.Models;

namespace ProjectSmart.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public DbSet<ScholarUsers> Scholars { get; set; }
        public DbSet<AdminUsers> Admins { get; set; }
        public DbSet<Certified_Grades> Certified_Grades { get; set; }
        public DbSet<Registration_Form> Registration_Form { get; set; }
        public DbSet<Terminal_Report> Terminal_Report { get; set; }
        public DbSet<Gratitude_Letter> Gratitude_Letter { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<StudentUsers>().HasData(
                new StudentUsers()
                {

                });*/
        }

    }
}
