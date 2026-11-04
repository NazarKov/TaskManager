using TaskManager.Model;
using TaskManager.Model.Enum;
using TaskManagerDataBase.Model;

namespace TaskManager.Services.Mapping
{
    public static class TaskMapper
    {
        public static TaskItem ToModel(this TaskEntity e) => new()
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description ?? string.Empty,
            ProjectName = e.ProjectName ?? string.Empty,
            Priority = (TaskPriority)e.Priority,
            Status = (TaskItemStatus)e.Status,
            DueDate = e.DueDate,
            IsMine = e.IsMine
        };

        public static TaskEntity ToEntity(this TaskItem m) => new()
        {
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            ProjectName = m.ProjectName,
            Priority = (int)m.Priority,
            Status = (int)m.Status,
            DueDate = m.DueDate,
            IsMine = m.IsMine
        };

        public static List<TaskItem> ToModels(this IEnumerable<TaskEntity> entities)
            => entities.Select(ToModel).ToList();
    }
}
