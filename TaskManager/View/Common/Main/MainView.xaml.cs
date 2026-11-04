using TaskManager.ViewModel.Common.Main;

namespace TaskManager.View.Common.Main;

public partial class MainView : ContentPage
{
    private readonly MainViewModel _viewModel;
    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCommand.ExecuteAsync(null);
    }
}