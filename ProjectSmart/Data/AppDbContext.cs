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
        public DbSet<Connect> Connects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AdminUsers>().HasData(
                new AdminUsers()
                {
                    AdminId = 1,
                    AdminFirstName = "adminFN",
                    AdminMiddleName = "adminMN",
                    AdminLastName = "adminLN",
                    AdminEmail = "admin.sei@dost.gov.ph",
                    AdminRole = "Admin",
                    AdminContactNumber = "0912-345-6789",
                    AdminDateOfBirth = "12/03/2023",
                    AdminAddress1 = "Sample Address 1",
                    AdminAddress2 = "Sample Address 2",
                    AdminCity = "Taguig City",
                    AdminRegion = "NCR - National Capital Region",
                    AdminPosition = "Director",
                    AdminBranch = "NCR - National Capital Region"
                });
        }

    }
}