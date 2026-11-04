using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Extensions;
using TaskManager.Services.Navigation;
using TaskManager.Services.Navigation.Interface;
using TaskManager.Services.TaskService;
using TaskManager.Services.TaskService.Interface;
using TaskManager.View.Common.Objective;
using TaskManager.ViewModel.Common.Main;
using TaskManager.ViewModel.Common.Objective;
using TaskManagerDataBase.Context;

namespace TaskManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "taskmanager.db3");
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddDbContextFactory<ContextDataBase>(o => o.UseSqlite($"Filename={dbPath}"));

            builder.Services.AddSingleton<INavigationService, ShellNavigationService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();


            builder.Services.AddTransient<TaskEditViewModel>();
            builder.Services.AddTransient<TaskEditView>();

            builder.Services.AddTaskManagerData(dbPath);
            builder.Services.BuildServiceProvider().InitializeTaskManagerData();

            builder.Services.AddSingleton<ITaskService, TaskService>();
            builder.Services.AddScoped<MainViewModel>();
            return builder.Build();
        }
    }
}
