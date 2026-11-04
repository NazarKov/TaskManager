using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Model;

namespace TaskManager.Services.TaskService.Interface
{
    public interface ITaskService
    {
        Task<IReadOnlyList<TaskItem>> GetTasksAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(int id);
    }
}
