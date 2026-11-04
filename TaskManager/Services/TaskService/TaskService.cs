using TaskManager.Model; 
using TaskManager.Services.Mapping;
using TaskManager.Services.TaskService.Interface;
using TaskManagerDataBase.Repository.Interface;

namespace TaskManager.Services.TaskService
{
    internal class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository) => _repository = repository;

        public async Task<IReadOnlyList<TaskItem>> GetTasksAsync()
            => (await _repository.GetAllAsync()).ToModels();

        public async Task<TaskItem?> GetByIdAsync(int id)
            => (await _repository.GetByIdAsync(id))?.ToModel();

        public async Task AddAsync(TaskItem task)
            => task.Id = await _repository.AddAsync(task.ToEntity());

        public async Task UpdateAsync(TaskItem task)
            => await _repository.UpdateAsync(task.ToEntity());

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
