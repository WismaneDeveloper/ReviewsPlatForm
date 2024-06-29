using Microsoft.Extensions.DependencyInjection;
using Repository.Contracts;
using Repository.LogicRepositories;
using Services.ContractServices;
using Services.LogicServices;

namespace Services
{
    /// <summary>
    /// Clase estática para registrar las dependencias del proyecto en el contenedor de servicios.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Método de extensión para registrar las dependencias de los servicios y repositorios en el contenedor de servicios.
        /// </summary>
        /// <param name="services">El contenedor de servicios de la aplicación.</param>
        /// <returns>El contenedor de servicios con las dependencias registradas.</returns>
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            /*============================== LIBROS ================================*/
            services.AddScoped<ICBook, BookRepository>();
            services.AddScoped<IBookServices, BookServices>();

            /*============================== USUARIOS ================================*/
            services.AddScoped<ICUser, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
