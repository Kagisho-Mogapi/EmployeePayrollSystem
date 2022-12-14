using EmployeePayrollSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeePayrollSystem.Models;

namespace EmployeePayrollSystem.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<EmployeePayrollSystem.Models.AddLevel>? AddLevel { get; set; }

    public DbSet<EmployeePayrollSystem.Models.AddEmployee>? AddEmployee { get; set; }

    public DbSet<EmployeePayrollSystem.Models.ApplyForLeave>? ApplyForLeave { get; set; }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(s => s.FirstName).HasMaxLength(30);
        builder.Property(s => s.LastName).HasMaxLength(30);
    }
}