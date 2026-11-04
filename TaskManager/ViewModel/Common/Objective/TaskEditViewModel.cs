using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;   
using TaskManager.Model;
using TaskManager.Model.Enum;
using TaskManager.Services.Navigation.Interface;
using TaskManager.Services.TaskService.Interface; 

namespace TaskManager.ViewModel.Common.Objective
{
    public partial class TaskEditViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ITaskService _taskService;
        private readonly INavigationService _navigation;
        private readonly IDialogService _dialogs;
        private int _id;

        public TaskEditViewModel(ITaskService taskService, INavigationService navigation, IDialogService dialogs)
        {
            _taskService = taskService;
            _navigation = navigation;
            _dialogs = dialogs;
        }

        // Порядок збігається з enum-ами TaskPriority / TaskItemStatus
        public IReadOnlyList<string> PriorityOptions { get; } = new[] { "Низький", "Середній", "Високий" };
        public IReadOnlyList<string> StatusOptions { get; } = new[] { "До виконання", "В процесі", "Виконано" };

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string title = string.Empty;

        [ObservableProperty] private string description = string.Empty;
        [ObservableProperty] private string projectName = string.Empty;
        [ObservableProperty] private int priorityIndex = 1;
        [ObservableProperty] private int statusIndex;
        [ObservableProperty] private DateTime dueDate = DateTime.Today;
        [ObservableProperty] private bool isMine = true;
        [ObservableProperty] private bool isBusy;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PageTitle))]
        [NotifyPropertyChangedFor(nameof(CanDelete))]
        private bool isNew = true;

        public string PageTitle => IsNew ? "Нова задача" : "Редагування задачі";
        public bool CanDelete => !IsNew;

        // Приймає задачу з головної сторінки (режим редагування)
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (!query.TryGetValue("task", out var value) || value is not TaskItem task)
                return;

            _id = task.Id;
            Title = task.Title;
            Description = task.Description;
            ProjectName = task.ProjectName;
            PriorityIndex = (int)task.Priority;
            StatusIndex = (int)task.Status;
            DueDate = task.DueDate;
            IsMine = task.IsMine;
            IsNew = false;
        }

        private bool CanSave() => !string.IsNullOrWhiteSpace(Title);

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                var task = new TaskItem
                {
                    Id = _id,
                    Title = Title.Trim(),
                    Description = Description?.Trim() ?? string.Empty,
                    ProjectName = ProjectName?.Trim() ?? string.Empty,
                    Priority = (TaskPriority)PriorityIndex,
                    Status = (TaskItemStatus)StatusIndex,
                    DueDate = DueDate.Date,
                    IsMine = IsMine
                };

                if (IsNew)
                    await _taskService.AddAsync(task);
                else
                    await _taskService.UpdateAsync(task);

                await _navigation.GoBackAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (IsNew) return;

            var confirmed = await _dialogs.ConfirmAsync(
                "Видалити задачу?", "Цю дію не можна скасувати.", "Видалити", "Скасувати");
            if (!confirmed) return;

            await _taskService.DeleteAsync(_id);
            await _navigation.GoBackAsync();
        }

        [RelayCommand]
        private Task CancelAsync() => _navigation.GoBackAsync();
    }
}

