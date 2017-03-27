using System.Data.Entity;
using System.Threading.Tasks;

namespace LawnMower.Domain.Entity
{
    public class LawnMowerContext:DbContext
    {
        public LawnMowerContext():base("LawnMowerContext")
        {
            Database.SetInitializer<LawnMowerContext>(null);
        }
        public DbSet<SmartLawnMower> SmartLawnMowers { get; set; }
        public DbSet<Lawn> Lawns { get; set; }
        public DbSet<Log> Logs { get; set; }

        public override async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
