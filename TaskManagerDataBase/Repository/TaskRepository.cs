using Microsoft.EntityFrameworkCore; 
using TaskManagerDataBase.Context;
using TaskManagerDataBase.Model;
using TaskManagerDataBase.Repository.Interface;

namespace TaskManagerDataBase.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbContextFactory<ContextDataBase> _factory;

        public TaskRepository(IDbContextFactory<ContextDataBase> factory) => _factory = factory;

        public async Task<List<TaskEntity>> GetAllAsync()
        {
            await using var db = await _factory.CreateDbContextAsync();
            return await db.Tasks.AsNoTracking().OrderBy(t => t.DueDate).ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            await using var db = await _factory.CreateDbContextAsync();
            return await db.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<int> AddAsync(TaskEntity entity)
        {
            await using var db = await _factory.CreateDbContextAsync();
            db.Tasks.Add(entity);
            await db.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(TaskEntity entity)
        {
            await using var db = await _factory.CreateDbContextAsync();
            db.Tasks.Update(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var db = await _factory.CreateDbContextAsync();
            await db.Tasks.Where(t => t.Id == id).ExecuteDeleteAsync();
        }
    }
}
