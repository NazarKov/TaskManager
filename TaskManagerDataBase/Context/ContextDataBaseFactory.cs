using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design; 

namespace TaskManagerDataBase.Context
{
    internal class ContextDataBaseFactory : IDesignTimeDbContextFactory<ContextDataBase>
    {
        public ContextDataBase CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContextDataBase>();

            optionsBuilder.UseSqlite("C:\\Users\\nazar\\AppData\\Local\\User Name\\com.companyname.taskmanager\\Data\\taskmanager.db3");

            return new ContextDataBase(optionsBuilder.Options);
        }
    }
}
