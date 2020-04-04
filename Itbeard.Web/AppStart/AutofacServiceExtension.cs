using Autofac;
using Itbeard.Di;
using Microsoft.Extensions.DependencyInjection;

namespace Itbeard.Web.AppStart
{
    public static class AutofacServiceExtension
    {
        public static IContainer ConfigureAutofac(this IServiceCollection services)
        {
            var container = new AutofacContainer();
            return container.Build(services);
        }
    }
}