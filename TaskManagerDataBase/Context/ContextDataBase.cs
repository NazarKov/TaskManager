using Microsoft.EntityFrameworkCore; 
using TaskManagerDataBase.Model;

namespace TaskManagerDataBase.Context
{
    public class ContextDataBase : DbContext
    {
        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options) { }

        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>(e =>
            {
                e.HasKey(t => t.Id);
                e.Property(t => t.Title).IsRequired().HasMaxLength(200);
                e.Property(t => t.Description).HasMaxLength(2000);
                e.Property(t => t.ProjectName).HasMaxLength(100);
                e.HasIndex(t => t.DueDate); 
            });
        }
    }
}
