using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerDataBase.Model;

namespace TaskManagerDataBase.Repository.Interface
{
    public interface ITaskRepository
    {
        Task<List<TaskEntity>> GetAllAsync();
        Task<TaskEntity?> GetByIdAsync(int id);
        Task<int> AddAsync(TaskEntity entity); 
        Task UpdateAsync(TaskEntity entity);
        Task DeleteAsync(int id);
    }
}
