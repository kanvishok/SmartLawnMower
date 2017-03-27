using System.Threading.Tasks;
using LawnMower.Domain;
using LawnMower.Domain.Commands;
using LawnMower.Domain.Entity;
using LawnMower.Infrastructure.Command;
using LawnMower.Infrastructure.Query;
using LawnMower.Shared.Model;
using LawnMover.Query.Queries;

namespace LawnMower.Application
{
    public class LawnMowerService : ILawnMowerService
    {
        private readonly IBus _bus;
        private readonly IQueryDispatcher _queryDispatcher;
        public LawnMowerService(IBus bus, IQueryDispatcher queryDispatcher)
        {
            _bus = bus;
            _queryDispatcher = queryDispatcher;
        }
        public async Task<string> Trun(string direction)
        {
            var turnCommand = new TurnCommand(direction);
            await _bus.SendAsync<TurnCommand, SmartLawnMower>(turnCommand);
            return "CorId";//yet to implement
        }

        public async Task Setup(LawnMowerSetupParams parameters)
        {
            var initiateCommand = new InitiateCommand(parameters);
            await _bus.SendAsync<InitiateCommand, SmartLawnMower>(initiateCommand);
        }

        public async Task<string> Move(int units)
        {
            var moveCommand = new MoveCommand(units);
            await _bus.SendAsync<MoveCommand, SmartLawnMower>(moveCommand);
            return "CorId";//yet to implement
        }

        public async Task<Lawn> GetLawnSize()
        {
            var query = new GetLawnQuery();
            return await _queryDispatcher.DispatchAsync<GetLawnQuery, Lawn>(query);
        }

        public async Task<Location> GetLocation()
        {
            var query = new GetLocationQuery();
            return await _queryDispatcher.DispatchAsync<GetLocationQuery, Location>(query);
        }

        public async Task<Direction> GetDirection()
        {
            var query = new GetDirectionQuery();
            return await _queryDispatcher.DispatchAsync<GetDirectionQuery, Direction>(query);
        }
    }

  
}
