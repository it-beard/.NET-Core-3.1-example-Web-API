using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Itbeard.Data;
using Itbeard.Data.Repositories;
using Itbeard.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Itbeard.Di
{
    public class AutofacContainer
    {
        public IContainer Build(IServiceCollection services)
        {
            Services.AssemblyRunner.Run();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .OrderByDescending(a => a.FullName)
                .ToArray();

            ServicesRegister(ref builder, assemblies);
            RepositoriesRegister(ref builder, assemblies);

            return builder.Build();
        }
        
        private void ServicesRegister(ref ContainerBuilder builder, Assembly[] assemblies)
        {
            var servicesAssembly = assemblies.FirstOrDefault(t => t.FullName.ToLower().Contains("itbeard.services"));
            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }

        private void RepositoriesRegister(ref ContainerBuilder builder, Assembly[] assemblies)
        {
            builder.RegisterGeneric(typeof(RepositoryBase<>))
                .As(typeof(IRepositoryBase<>));

            var dataAssembly = assemblies.FirstOrDefault(t => t.FullName.ToLower().Contains("itbeard.data"));
            builder.RegisterAssemblyTypes(dataAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterType(typeof(UnitOfWork))
                .As(typeof(IUnitOfWork));
        }
    }
}