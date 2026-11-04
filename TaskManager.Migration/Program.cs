using Microsoft.EntityFrameworkCore;
using TaskManagerDataBase.Context;

namespace TaskManager.Migration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContextDataBase>();
            optionsBuilder.UseSqlite("C:\\Users\\nazar\\AppData\\Local\\User Name\\com.companyname.taskmanager\\Data\\taskmanager.db3");

            var db = new ContextDataBase(optionsBuilder.Options);
            db.Database.Migrate(); 

            db.SaveChanges();
        }
    }
}
