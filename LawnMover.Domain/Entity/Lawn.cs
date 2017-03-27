using LawnMower.Infrastructure.Repository;

namespace LawnMower.Domain.Entity
{
    public class Lawn:IEntityBase
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
    }
}
