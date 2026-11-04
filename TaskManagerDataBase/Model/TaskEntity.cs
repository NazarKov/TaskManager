using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerDataBase.Model
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ProjectName { get; set; }
        public int Priority { get; set; } 
        public int Status { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsMine { get; set; }
    }
}
