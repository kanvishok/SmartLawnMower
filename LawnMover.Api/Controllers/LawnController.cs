using System.Threading.Tasks;
using System.Web.Http;
using LawnMower.Application;
using LawnMower.Domain.Entity;
using LawnMower.Shared.Model;

namespace LawnMower.Api.Controllers
{
    [RoutePrefix("api/Lawn")]
    public class LawnController : ApiController
    {
        private readonly ILawnMowerService _lawnMowerService;

        public LawnController(ILawnMowerService lawnMowerService)
        {
            _lawnMowerService = lawnMowerService;
        }

        public async Task Post([FromBody] LawnMowerSetupParams lawnMowerSetupParams)
        {
            await _lawnMowerService.Setup(lawnMowerSetupParams);
        }

        public async Task<Lawn> Get()
        {
            return await _lawnMowerService.GetLawnSize();
        }

    }
}
