using System.Web.Http;
using LawnMower.Application;

namespace LawnMower.Api.Controllers
{
    [RoutePrefix("api/mower")]
    public class MowerController : ApiController
    {
        private readonly ILawnMowerService _lawnMowerService;

        public MowerController(ILawnMowerService lawnMowerService)
        {
            _lawnMowerService = lawnMowerService;
        }

        //public async Task Put()

    }
}
