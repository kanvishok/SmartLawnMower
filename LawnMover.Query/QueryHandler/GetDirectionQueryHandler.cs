using System.Data.Entity;
using System.Threading.Tasks;
using LawnMover.Query.Queries;
using LawnMower.Domain;
using LawnMower.Infrastructure.Query;
using LawnMower.Infrastructure.Repository;
using LawnMower.Shared.Model;

namespace LawnMover.Query.QueryHandler
{
    public class GetDirectionQueryHandler : IQueryHandler<GetDirectionQuery, Direction>
    {
        private readonly IGenericRepository<SmartLawnMower> _smartLawnMowerRepository;

        public GetDirectionQueryHandler(IGenericRepository<SmartLawnMower> smartLawnMowerRepository)
        {
            _smartLawnMowerRepository = smartLawnMowerRepository;
        }

        public async Task<Direction> RetriveAsync(GetDirectionQuery query)
        {
            var activeMower = await _smartLawnMowerRepository.FindBy(x => x.IsActive).FirstOrDefaultAsync();
            return activeMower.Direction;
        }

    }
}
