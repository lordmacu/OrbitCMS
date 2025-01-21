using Infrastructure.Persistence;
using Application.Services;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.Common;

namespace API.Configurations
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds application-specific services to the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method registers the PostService with a scoped lifetime in the dependency injection container.
        /// </remarks>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<PostService>();
            services.AddScoped<ISlugService, SlugService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }


        /// <summary>
        /// Adds repository services to the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method registers various repository and service interfaces with their corresponding implementations
        /// </remarks>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IPostTypeRepository, PostTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }


        /// <summary>
        /// Adds the database context to the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the database context to.</param>
        /// <param name="connectionString">The connection string for the MySQL database.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method configures and adds the <see cref="AppDbContext"/> to the service collection
        /// using MySQL as the database provider with a specific server version.
        /// </remarks>
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32))));

            return services;
        }

    }
}
