using System.Threading.Tasks;
using LawnMower.Shared.Model;
using LawnMower.Domain.Entity;

namespace LawnMower.Application
{
    public interface ILawnMowerService
    {
        Task Setup(LawnMowerSetupParams parameters);
        Task<string> Trun(string direction);
        Task<string> Move(int units);
        Task<Lawn> GetLawnSize();
        Task<Location> GetLocation();
        Task<Direction> GetDirection();
    }
}