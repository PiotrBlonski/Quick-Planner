using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quick_Planner.AppPermissions;
using Quick_Planner.Pages.Tasks;
using Quick_Planner.Resources;
using System.Diagnostics;
using Task = Quick_Planner.Models.Task;

namespace Quick_Planner.ViewModels
{
    [QueryProperty("Task", "Task")]
    [QueryProperty("Editing", "Editing")]
    public partial class TaskAddViewModel : ObservableObject
    {
        public TaskAddPage TaskAddPage;

        [ObservableProperty]
        Task currentTask;

        [ObservableProperty]
        Task task;

        [ObservableProperty]
        bool editing;

        partial void OnTaskChanged(Task value)
        {
            CurrentTask = new Task().CopyProperties(value);
            TaskAddPage.PopulatePicker();
        }

        [RelayCommand]
        async System.Threading.Tasks.Task CreateTask()
        {
            string Status = TaskManager.CheckIfValid(CurrentTask);

            if (Status != "OK")
            {
                await TaskAddPage.DisplayAlert(AppResources.Error, Status, "OK");
                return;
            }

            if (Editing)
            {
                TaskManager.Tasks.Remove(Task);
#if ANDROID
                Quick_Planner.Platforms.Android.AndroidServiceManager.CancelNotification(Task);
#endif
            }

            if (!CurrentTask.HasEndDate)
            {
                CurrentTask.Date = new DateTime(1970, 1, 1);
                CurrentTask.Time = TimeSpan.Zero;
                CurrentTask.Notificationtype = 0;
            }

            if (CurrentTask.Notificationtype > 0)
            {
#if ANDROID
                PermissionStatus status = await CheckNotificationPermission();

                if(status == PermissionStatus.Granted)
                    Quick_Planner.Platforms.Android.AndroidServiceManager.ScheduleNotification(CurrentTask);
                else
                {
                    TaskAddPage.DisplayAlert(AppResources.Error, AppResources.Permission_Notification_Error, "OK");
                    return;
                }
#endif
            }

            int InsertIndex = CurrentTask.HasEndDate ? TaskManager.Tasks.IndexOf(TaskManager.Tasks.FirstOrDefault(t => t.Date.Date + t.Time >= CurrentTask.Date.Date + CurrentTask.Time))
                                              : TaskManager.Tasks.IndexOf(TaskManager.Tasks.Where(t => !t.HasEndDate).FirstOrDefault(t => t.Title.CompareTo(CurrentTask.Title) > 0));

            TaskManager.Tasks.Insert(InsertIndex == -1 ? (CurrentTask.HasEndDate ? TaskManager.Tasks.IndexOf(TaskManager.Tasks.LastOrDefault(t => t.HasEndDate)) + 1 : TaskManager.Tasks.Count) : InsertIndex, CurrentTask);

            CurrentTask.Save();

            CloseSelf();
        }
        async void CloseSelf()
        {
            await Shell.Current.GoToAsync("..");
        }

        async Task<PermissionStatus> CheckNotificationPermission()
        {
            PermissionStatus status = await PermissionManager.CheckPermission<NotificationsPermission>();

            if (status != PermissionStatus.Granted)
                status = await PermissionManager.RequestPermission<NotificationsPermission>();

            return status;
        }
    }
}
