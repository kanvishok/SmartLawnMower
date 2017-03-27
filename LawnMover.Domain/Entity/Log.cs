using LawnMower.Infrastructure.Repository;

namespace LawnMower.Domain.Entity
{
    public class Log:IEntityBase
    {
        public int Id { get; set; }

        public int LawnMoverId { get; set; }
        public string LogMessage { get; set; }
    }
}
