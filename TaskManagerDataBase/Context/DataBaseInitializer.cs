using Microsoft.EntityFrameworkCore; 
using TaskManagerDataBase.Model;

namespace TaskManagerDataBase.Context
{
    public class DataBaseInitializer
    {
        public static void Initialize(IDbContextFactory<ContextDataBase> factory)
        {
            using var db = factory.CreateDbContext();
            db.Database.EnsureCreated();

            if (db.Tasks.Any()) return;

            var today = DateTime.Today;
            db.Tasks.AddRange(
                new TaskEntity
                {
                    Title = "Підготувати звіт",
                    Description = "Квартальний звіт для керівництва",
                    ProjectName = "Аналітика",
                    Priority = 1,
                    Status = 1,
                    DueDate = today,
                    IsMine = true
                },
                new TaskEntity
                {
                    Title = "Оновити дизайн",
                    Description = "Нові макети головної сторінки",
                    ProjectName = "Сайт",
                    Priority = 0,
                    Status = 0,
                    DueDate = today.AddDays(3),
                    IsMine = false
                },
                new TaskEntity
                {
                    Title = "Code review",
                    Description = "Перевірити PR #42",
                    ProjectName = "Backend",
                    Priority = 2,
                    Status = 2,
                    DueDate = today.AddDays(-2),
                    IsMine = true
                });

            db.SaveChanges();
        }
    }
}
