using System;
using Autofac;

namespace LawnMower.Infrastructure.Dependencies
{
    public class Resolver : IResolver
    {
        private readonly IComponentContext _context;

        public Resolver(IComponentContext context)
        {
            _context = context;
        }

        public T Resolve<T>()
        {
            return _context.Resolve<T>();
        }

        public object ResolveOptional(Type type)
        {
            return _context.ResolveOptional(type);
        }
    }
}