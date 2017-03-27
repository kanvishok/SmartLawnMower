using System.Data.Entity;
using System.Threading.Tasks;
using LawnMover.Query.Queries;
using LawnMower.Domain;
using LawnMower.Domain.Entity;
using LawnMower.Infrastructure.Query;
using LawnMower.Infrastructure.Repository;

namespace LawnMover.Query.QueryHandler
{
    public class GetLawnQueryHandler: IQueryHandler<GetLawnQuery, Lawn>
    {
        private readonly IGenericRepository<SmartLawnMower> _smartLawnMowerRepository;

        public GetLawnQueryHandler(IGenericRepository<SmartLawnMower> smartLawnMowerRepository)
        {
            _smartLawnMowerRepository = smartLawnMowerRepository;
        }

        public async Task<Lawn> RetriveAsync(GetLawnQuery query)
        {
            var activeMower = await _smartLawnMowerRepository.FindBy(x => x.IsActive).FirstOrDefaultAsync();
            return activeMower.Lawn;
        }
    }
}
