using System;

namespace LawnMower.Infrastructure.Dependencies
{
    public interface IResolver
    {
        T Resolve<T>();
        object ResolveOptional(Type type);
    }
}