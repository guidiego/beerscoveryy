using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using beerscovery.Models;

namespace beerscovery.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<beerscovery.Models.Beer> Beer { get; set; }
        public DbSet<beerscovery.Models.Score> Score { get; set; }
        public DbSet<beerscovery.Models.Place> Place { get; set; }
        public DbSet<beerscovery.Models.UserStats> UserStats { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
