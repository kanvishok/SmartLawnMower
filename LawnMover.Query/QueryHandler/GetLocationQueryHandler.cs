using System.Data.Entity;
using System.Threading.Tasks;
using LawnMover.Query.Queries;
using LawnMower.Domain;
using LawnMower.Infrastructure.Query;
using LawnMower.Infrastructure.Repository;
using LawnMower.Shared.Model;

namespace LawnMover.Query.QueryHandler
{
    public class GetLocationQueryHandler: IQueryHandler<GetLocationQuery,Location>
    {
        private readonly IGenericRepository<SmartLawnMower> _smartLawnMowerRepository;

        public GetLocationQueryHandler(IGenericRepository<SmartLawnMower> smartLawnMowerRepository)
        {
            _smartLawnMowerRepository = smartLawnMowerRepository;
        }

        public async Task<Location> RetriveAsync(GetLocationQuery query)
        {
            var activeMower = await _smartLawnMowerRepository.FindBy(x => x.IsActive).FirstOrDefaultAsync();
            return activeMower.Location;
        }
    }
}
