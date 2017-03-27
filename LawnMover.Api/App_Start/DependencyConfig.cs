using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FluentValidation;
using LawnMover.Query.Queries;
using LawnMower.Application;
using LawnMower.Infrastructure.Command;
using LawnMower.Infrastructure.Dependencies;
using LawnMower.Infrastructure.Event;
using LawnMower.Infrastructure.Query;
using LawnMower.Infrastructure.Repository;
using LawnMower.Domain;
using LawnMower.Domain.EventHandlers;
using LawnMower.Domain.Entity;
using LawnMower.Shared.Helper;

namespace LawnMower.Api
{
    public class DependencyConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LawnMowerService>().As<ILawnMowerService>().InstancePerLifetimeScope();
            builder.RegisterType<DirectionHelper>().As<IDirectionHelper>().InstancePerLifetimeScope();
            builder.RegisterType<Resolver>().As<IResolver>();
            builder.RegisterType<Bus>().As<IBus>().InstancePerLifetimeScope();

            var queryAssembly = typeof(GetLawnQuery).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(queryAssembly).AsClosedTypesOf(typeof(IQueryHandler<,>));
            builder.RegisterAssemblyTypes(queryAssembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(queryAssembly).AsClosedTypesOf(typeof(IValidator<>));

            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();

            var domainAssembly = typeof(SmartLawnMower).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(domainAssembly).AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.RegisterAssemblyTypes(domainAssembly).AsClosedTypesOf(typeof(IValidator<>));
            builder.RegisterAssemblyTypes(domainAssembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(TurnCompletedEventHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IEventHandler<>));

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<LawnMowerContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}