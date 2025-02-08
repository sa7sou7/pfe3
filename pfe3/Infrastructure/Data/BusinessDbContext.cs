using Microsoft.EntityFrameworkCore;
using pfe3.Core.Entities;

namespace pfe3.Data
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions<BusinessDbContext> options)
            : base(options) { }

        public DbSet<Clients> Clients { get; set; }
        public DbSet<Contactes> Contactes { get; set; }
    }
}
