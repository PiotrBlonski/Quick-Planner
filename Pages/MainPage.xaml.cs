using Quick_Planner.Pages.Alarm;
using Quick_Planner.Pages.Projects;
using Quick_Planner.Pages.Settings;
using Quick_Planner.Pages.Tasks;

namespace Quick_Planner
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void GoToTasksPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TasksListPage));
        }

        async void GoToProjectsPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ProjectsListPage));
        }

        async void GoToSettingsPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
#if ANDROID
            if(Quick_Planner.Platforms.Android.AndroidServiceManager.Ringtone != null && Quick_Planner.Platforms.Android.AndroidServiceManager.Ringtone.IsPlaying)
            {
                if(TaskManager.Tasks == null)
                    TaskManager.LoadTasks();

                Shell.Current.GoToAsync($"{nameof(AlarmPage)}",
                    new Dictionary<string, object> { { "Task", TaskManager.Tasks.FirstOrDefault(task => task.Id == Quick_Planner.Platforms.Android.AndroidServiceManager.TaskId) }});
            }
#endif
        }
    }
}