using ApartmentManagmentBlazorAppCopy.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagmentBlazorAppCopy
{
    public class ApartmentsContext : DbContext
    {
        public ApartmentsContext(DbContextOptions<ApartmentsContext> options)
        : base(options) { }

        public DbSet<Apartment> Apartments => Set<Apartment>();
        public DbSet<Rent> Rents => Set<Rent>();
        public DbSet<Roommate> Roommates => Set<Roommate>();
        public DbSet<ExploitationCost> ExploitationCosts => Set<ExploitationCost>();
        public DbSet<Meter> Meters => Set<Meter>();
        public DbSet<Models.File> Files => Set<Models.File>();
        public DbSet<PurchaseRecord> PurchaseRecords => Set<PurchaseRecord>();
        public DbSet<Chore> Chores => Set<Chore>();
    }
}
