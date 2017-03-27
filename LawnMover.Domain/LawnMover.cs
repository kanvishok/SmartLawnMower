using System;
using System.Threading;
using LawnMower.Domain.Entity;
using LawnMower.Infrastructure.Command;
using LawnMower.Infrastructure.Repository;
using LawnMower.Shared.Model;

namespace LawnMower.Domain
{
    public class SmartLawnMower : IAggregateRoot, IEntityBase
    {
        public int LawnId { get; private set; }
        public int Id { get; set; }
        public bool IsActive { get; private set; }
        public Direction Direction { get; private set; }
        public Lawn Lawn { get; private set; }
        public Status Status { get; private set; }

        public Location Location { get; private set; }
        public static SmartLawnMower Turn(string direction, SmartLawnMower smartLawnMower)
        {
            smartLawnMower.Direction = (Direction)Enum.Parse(typeof(Direction), direction);
            Thread.Sleep(10000);
            return smartLawnMower;
        }
        public static SmartLawnMower Deactivate(SmartLawnMower smartLawnMower)
        {
            smartLawnMower.IsActive = false;
            return smartLawnMower;
        }

        public static SmartLawnMower Setup(LawnMowerSetupParams lawnMowerSetupParams)
        {
            return new SmartLawnMower
            {
                IsActive = true,
                Lawn = new Lawn { Length = lawnMowerSetupParams.Length, Width = lawnMowerSetupParams.Width },
                Direction = Direction.East, //Default Direction
                Location = new Location (lawnMowerSetupParams.LawnX, lawnMowerSetupParams.LawnY)
            };

        }

        public static void Move(SmartLawnMower smartLawnMover, int units)
        {
            smartLawnMover.Location = SetPosition(smartLawnMover, units);
        }

        private static Location SetPosition(SmartLawnMower smartLawnMover , int units)
        {
            var position = new Location(smartLawnMover.Location);
            switch (smartLawnMover.Direction)
            {
                case Direction.East:
                    position.Y += units;
                    break;
                case Direction.North:
                    position.X += units;
                    break;
                case Direction.West:
                    position.Y -= units;
                    break;
                case Direction.South:
                    position.X -= units;
                    break;
            }
            return position;
        }

        
    }
}
