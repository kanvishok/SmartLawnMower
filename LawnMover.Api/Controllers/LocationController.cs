using System.Threading.Tasks;
using System.Web.Http;
using LawnMower.Application;
using LawnMower.Shared.Model;

namespace LawnMower.Api.Controllers
{
    [RoutePrefix("api/Locaiton")]
    public class LocationController : ApiController
    {
        private readonly ILawnMowerService _lawnMowerService;

        public LocationController(ILawnMowerService lawnMowerService)
        {
            _lawnMowerService = lawnMowerService;
        }

        public async Task Move(int unit)
        {
            await _lawnMowerService.Move(unit);
        }
        public async Task<Location> Get()
        {
            return await _lawnMowerService.GetLocation();
        }
    }
}
