using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Model.Enum;

namespace TaskManager.Model
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public TaskPriority Priority { get; set; }
        public TaskItemStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsMine { get; set; }

        // Тексти для відображення в UI
        public string PriorityText => Priority switch
        {
            TaskPriority.High => "Високий",
            TaskPriority.Medium => "Середній",
            _ => "Низький"
        };

        public string StatusText => Status switch
        {
            TaskItemStatus.ToDo => "До виконання",
            TaskItemStatus.InProgress => "В процесі",
            _ => "Виконано"
        };
    }
}
