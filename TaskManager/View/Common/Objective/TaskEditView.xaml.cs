using TaskManager.ViewModel.Common.Objective;

namespace TaskManager.View.Common.Objective;

public partial class TaskEditView : ContentPage
{
	public TaskEditView(TaskEditViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}