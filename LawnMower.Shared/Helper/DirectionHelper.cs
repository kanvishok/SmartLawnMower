using System;
using LawnMower.Shared.Model;

namespace LawnMower.Shared.Helper
{
    public class DirectionHelper : IDirectionHelper
    {
        public string GetSideFromDirection(Direction fromDirection, Direction toDirection)
        {
            var angle = ((int)fromDirection) - ((int)toDirection);

            if (angle == 90 || angle == 180 || angle == -270)
                return "Right";
            else if (angle == -90 || angle == -180 || angle == 270)
                return "Left";
            else
                return "Invalid";
        }

        public Direction GetNextDirectionFromSide(string side, Direction currectDirection)
        {
            var angle = (int)currectDirection;
            switch (side)
            {
                case "Left":
                    if (angle == 270)
                        angle = 0;
                    else
                        angle += 90;
                    break;
                case "Right":
                    if (angle == 0)
                        angle = 270;
                    else
                        angle -= 90;
                    break;
            }
            return (Direction)angle;
        }

    }
}
