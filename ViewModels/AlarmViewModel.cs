using CommunityToolkit.Mvvm.ComponentModel;
using Task = Quick_Planner.Models.Task;

namespace Quick_Planner.ViewModels
{
    [QueryProperty("Task", "Task")]
    public partial class AlarmViewModel : ObservableObject
    {
        [ObservableProperty]
        public Task task;
    }
}
