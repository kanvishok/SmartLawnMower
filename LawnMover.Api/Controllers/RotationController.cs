using System;
using System.Threading.Tasks;
using System.Web.Http;
using LawnMower.Api.Models;
using LawnMower.Application;
using LawnMower.Shared.Model;

namespace LawnMower.Api.Controllers
{
    [RoutePrefix("api/rotation")]
    public class RotationController : ApiController
    {
        private readonly ILawnMowerService _lawnMowerService;

        public RotationController(ILawnMowerService lawnMowerService)
        {
            _lawnMowerService = lawnMowerService;
        }
        
        public async Task Put([FromBody] RotationAction rotationAction)
        {
            try
            {
                await _lawnMowerService.Trun(rotationAction.Direction);
            }
            catch (Exception ex)
            {
                
            }
        }
        public async Task<Direction> Get()
        {
            return await _lawnMowerService.GetDirection();
        }
    }
}
