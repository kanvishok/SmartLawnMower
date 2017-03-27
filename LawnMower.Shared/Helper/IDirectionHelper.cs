using LawnMower.Shared.Model;

namespace LawnMower.Shared.Helper
{
    public interface IDirectionHelper
    {
        string GetSideFromDirection(Direction fromDirection, Direction toDirection);
    }
}