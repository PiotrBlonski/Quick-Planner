using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quick_Planner.Pages.Tasks;
using System.Collections.ObjectModel;
using Task = Quick_Planner.Models.Task;

namespace Quick_Planner.ViewModels
{
    public partial class TasksViewModel : ObservableObject
    {
        public TasksViewModel() 
        {
            Tasks = TaskManager.Tasks;
        }

        [ObservableProperty]
        ObservableCollection<Task> tasks;

        [RelayCommand]
        public void Delete(Task Task)
        {
#if ANDROID
                Quick_Planner.Platforms.Android.AndroidServiceManager.CancelNotification(Task);
#endif
            Tasks.Remove(Task); 
            Task.Delete();
        }

        [RelayCommand]
        private void Add()
        {
            Update(new(), false);
        }

        [RelayCommand]
        public void Edit(Task Task)
        {
            Update(Task, true);
        }

        async void Update(Task Task, bool Editing) 
        {
            await Shell.Current.GoToAsync($"{nameof(TaskAddPage)}",
                new Dictionary<string, object> { { "Task", Task },
                                               { "Editing", Editing }
                });
        }
    }
}
