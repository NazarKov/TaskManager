using Microsoft.EntityFrameworkCore; 
using TaskManagerDataBase.Context;
using TaskManagerDataBase.Model;
using TaskManagerDataBase.Repository;
using TaskManagerDataBase.Repository.Interface;

namespace TaskManager.Extensions
{
    internal static class DataServiceExtensions
    {
        public static IServiceCollection AddTaskManagerData(this IServiceCollection services, string dbPath)
        {
            services.AddDbContextFactory<ContextDataBase>(o => o.UseSqlite($"Filename={dbPath}"));
            services.AddSingleton<ITaskRepository, TaskRepository>();
            return services;
        }

        public static void InitializeTaskManagerData(this IServiceProvider provider)
        {
            var factory = provider.GetRequiredService<IDbContextFactory<ContextDataBase>>();
            using var db = factory.CreateDbContext();
            db.Database.MigrateAsync();

           
            db.SaveChanges();
        } 
    }
}
