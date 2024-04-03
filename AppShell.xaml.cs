using Quick_Planner.Pages.Alarm;
using Quick_Planner.Pages.Projects;
using Quick_Planner.Pages.Settings;
using Quick_Planner.Pages.Tasks;

namespace Quick_Planner
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TasksListPage), typeof(TasksListPage));
            Routing.RegisterRoute(nameof(TaskAddPage), typeof(TaskAddPage));
            Routing.RegisterRoute(nameof(ProjectsListPage), typeof(ProjectsListPage));
            Routing.RegisterRoute(nameof(BoardView), typeof(BoardView));
            Routing.RegisterRoute(nameof(EditBoardPage), typeof(EditBoardPage));
            Routing.RegisterRoute(nameof(EditListPage), typeof(EditListPage));
            Routing.RegisterRoute(nameof(EditCardPage), typeof(EditCardPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(AlarmPage), typeof(AlarmPage));
        }
    }
}