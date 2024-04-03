using Quick_Planner.ViewModels;
using System.Diagnostics;

namespace Quick_Planner.Pages.Projects;

public partial class ProjectsListPage : ContentPage
{
    public ProjectsListPage(ProjectsListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void ChangeAutoSaveStatus(object sender, EventArgs e)
    {
        if (BoardManager.AutoSaveTask.Status.Equals(TaskStatus.Running))
        {
            BoardManager.CurrentBoard?.Save();
            BoardManager.StopTask = true;
        }
    }
}