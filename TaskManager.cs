using CommunityToolkit.Maui.Core.Extensions;
using Quick_Planner.Resources;
using System.Collections.ObjectModel;
using Task = Quick_Planner.Models.Task;

namespace Quick_Planner
{
    public static class TaskManager
    {
        public static ObservableCollection<Task> Tasks;

        public static void LoadTasks()
        {
            Tasks = Task.LoadAll().ToObservableCollection();
        }

        public static bool IsTitleInvalid(string Title) => string.IsNullOrWhiteSpace(Title);

        public static bool HasTimePassed(Task Task) => (Task.HasEndDate && Task.Date + Task.Time < DateTime.Now);

        public static bool HasDatePassed(Task Task) => (Task.Date.Date == DateTime.Today && Task.IsNotificationTimeSet && (Task.Notificationtime < DateTime.Now.TimeOfDay));

        public static bool IsNotificationTimePastTime(Task Task) => Task.IsNotificationTimeSet && Task.Notificationtime > Task.Time;

        public static string CheckIfValid(Task Task)
        {
            if (IsTitleInvalid(Task.Title))
                return AppResources.Empty_Title_Error;

            if (HasTimePassed(Task))
                return AppResources.Time_Error;

            if (HasDatePassed(Task))
                return AppResources.NotificationTime_Error;

            if (IsNotificationTimePastTime(Task))
                return AppResources.NotificationTime_Error2;

            return "OK";
        }
    }
}
