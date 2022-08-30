using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScrapManagement.Application.Interfaces.CacheRepositories;
using ScrapManagement.Application.Interfaces.Contexts;
using ScrapManagement.Application.Interfaces.Repositories;
using ScrapManagement.Infrastructure.CacheRepositories;
using ScrapManagement.Infrastructure.DbContexts;
using ScrapManagement.Infrastructure.Repositories;
using System.Reflection;

namespace ScrapManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IScrapRepository, ScrapRepository>();
            services.AddTransient<IScrapCacheRepository, ScrapCacheRepository>();
            services.AddTransient<IScrapTypeRepository, ScrapTypeRepository>();
            services.AddTransient<IScrapTypeCacheRepository, ScrapTypeCacheRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #endregion Repositories
        }
    }
}