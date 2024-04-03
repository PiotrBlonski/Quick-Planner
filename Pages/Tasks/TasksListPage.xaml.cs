using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Tasks;

public partial class TasksListPage : ContentPage
{
    public TasksListPage(TasksViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}