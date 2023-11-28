using Microsoft.EntityFrameworkCore;
using Restaurant_Management.Models;

namespace Restaurant_Management.HostedServices
{
    public class ApplyMigrationService
    {
        private readonly RestaurantDbContext db;
        public ApplyMigrationService(RestaurantDbContext db)
        {
            this.db = db;
        }
        public async Task ApplyMigrationAsync()
        {
            if((await db.Database.GetPendingMigrationsAsync()).Any())
            {
                await db.Database.MigrateAsync();
            }
        }
    }
}
