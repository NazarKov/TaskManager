namespace TaskManager.ViewModel.Common.Main; 
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input; 
using System.Collections.ObjectModel;
using TaskManager.Model;
using TaskManager.Model.Enum;
using TaskManager.Services.Navigation;
using TaskManager.Services.Navigation.Interface;
using TaskManager.Services.TaskService.Interface;

public partial class MainViewModel : ObservableObject
{
    private readonly ITaskService _taskService; 
    private readonly INavigationService _navigation;
    private readonly IDialogService _dialogs;
    private List<TaskItem> _allTasks = new();

    public MainViewModel(ITaskService taskService, INavigationService navigation, IDialogService dialogs)
    {
        _taskService = taskService;
        _navigation = navigation;
        _dialogs = dialogs;

    }

    public ObservableCollection<TaskItem> Tasks { get; } = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAllFilter))]
    [NotifyPropertyChangedFor(nameof(IsMineFilter))]
    [NotifyPropertyChangedFor(nameof(IsTodayFilter))]
    [NotifyPropertyChangedFor(nameof(IsOverdueFilter))]
    private TaskFilter selectedFilter = TaskFilter.All;

    [ObservableProperty] private bool isBusy;

    // Статистика
    [ObservableProperty] private int totalCount;
    [ObservableProperty] private int todoCount;
    [ObservableProperty] private int inProgressCount;
    [ObservableProperty] private int doneCount;

    // Для підсвічування активного фільтра
    public bool IsAllFilter => SelectedFilter == TaskFilter.All;
    public bool IsMineFilter => SelectedFilter == TaskFilter.Mine;
    public bool IsTodayFilter => SelectedFilter == TaskFilter.Today;
    public bool IsOverdueFilter => SelectedFilter == TaskFilter.Overdue;

    partial void OnSearchTextChanged(string value) => ApplyFilter();
    partial void OnSelectedFilterChanged(TaskFilter value) => ApplyFilter();

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            _allTasks = (await _taskService.GetTasksAsync()).ToList();
            UpdateStatistics();
            ApplyFilter();
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void SetFilter(TaskFilter filter) => SelectedFilter = filter;

    [RelayCommand]
    private Task AddTaskAsync() => _navigation.GoToAsync(Routes.TaskEdit);

    [RelayCommand]
    private Task EditTaskAsync(TaskItem task)
        => _navigation.GoToAsync(Routes.TaskEdit, new Dictionary<string, object> { ["task"] = task });

    [RelayCommand]
    private async Task DeleteTaskAsync(TaskItem task)
    {
        var confirmed = await _dialogs.ConfirmAsync(
            "Видалити задачу?", task.Title, "Видалити", "Скасувати");
        if (!confirmed) return;

        await _taskService.DeleteAsync(task.Id);
        await LoadAsync();
    }

    [RelayCommand]
    private async Task ToggleDoneAsync(TaskItem task)
    {
        task.Status = task.Status == TaskItemStatus.Done
            ? TaskItemStatus.ToDo
            : TaskItemStatus.Done;

        await _taskService.UpdateAsync(task);
        await LoadAsync();
    }

    private void UpdateStatistics()
    {
        TotalCount = _allTasks.Count;
        TodoCount = _allTasks.Count(t => t.Status == TaskItemStatus.ToDo);
        InProgressCount = _allTasks.Count(t => t.Status == TaskItemStatus.InProgress);
        DoneCount = _allTasks.Count(t => t.Status == TaskItemStatus.Done);
    }

    private void ApplyFilter()
    {
        var today = DateTime.Today;
        IEnumerable<TaskItem> query = _allTasks;

        query = SelectedFilter switch
        {
            TaskFilter.Mine => query.Where(t => t.IsMine),
            TaskFilter.Today => query.Where(t => t.DueDate.Date == today && t.Status != TaskItemStatus.Done),
            TaskFilter.Overdue => query.Where(t => t.DueDate.Date < today && t.Status != TaskItemStatus.Done),
            _ => query
        };

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var text = SearchText.Trim();
            query = query.Where(t =>
                t.Title.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                t.ProjectName.Contains(text, StringComparison.OrdinalIgnoreCase));
        }

        Tasks.Clear();
        foreach (var task in query)
            Tasks.Add(task);
    }
}