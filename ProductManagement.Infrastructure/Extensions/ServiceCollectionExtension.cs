using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            //var assemblies = BuildManager.GetReferencedAssemblies();
            var assembly = Assembly.Load("ProductManagement.Application");
            if (assembly is null) return;

            var services = assembly.GetTypes()
                .Where(x => x.IsClass && (typeof(IRegisterScoped).IsAssignableFrom(x) || typeof(IRegisterSingleton).IsAssignableFrom(x))).ToList();

            foreach (var service in services)
            {
                var interfac = service.GetInterfaces().FirstOrDefault();
                if (interfac is null) continue;

                if (typeof(IRegisterSingleton).IsAssignableFrom(service))
                    serviceCollection.AddSingleton(interfac, service);

                if (typeof(IRegisterScoped).IsAssignableFrom(service))
                    serviceCollection.AddScoped(interfac, service);

            }
        }

    }

    public interface IRegisterScoped
    {

    }

    public interface IRegisterSingleton
    {

    }
}
