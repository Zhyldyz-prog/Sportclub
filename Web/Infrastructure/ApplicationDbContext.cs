using Application.Interfaces.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Entities; // Пространство имен для модели Airpark

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Добавление DbSet для Airpark
        public DbSet<Airpark> Airparks { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }

    }
}
