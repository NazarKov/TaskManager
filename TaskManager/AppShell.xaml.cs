using TaskManager.Services.Navigation;
using TaskManager.View.Common.Objective; 

namespace TaskManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(Routes.TaskEdit, typeof(TaskEditView));
        }
    }
}
