using Android.App;
using Android.Content.PM;
using Android.Widget;
using Quick_Planner.Pages.Alarm;
using Quick_Planner.Platforms.Android;
using Quick_Planner.Resources;
using Intent = Android.Content.Intent;
using Task = Quick_Planner.Models.Task;
namespace Quick_Planner
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density | ConfigChanges.Orientation)]
    public class MainActivity : MauiAppCompatActivity
    {
        public MainActivity() { AndroidServiceManager.MainActivity = this; }
        
        public void ScheduleNotification(Task Task)
        {
            Intent intent = new(ApplicationContext, typeof(AlarmReceiver));
            intent.PutExtra("titleExtra", Task.Title);
            intent.PutExtra("textExtra", AppResources.At + ": " + (Task.Date + Task.Time)); 
            intent.PutExtra("TaskIdExtra", Task.Id);
            intent.PutExtra("notifExtra", Task.Notificationtype);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(ApplicationContext, Task.Id, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);
            AlarmManager alarmManager = (AlarmManager)GetSystemService(AlarmService);
            DateTime NotificationTime = Task.Date + (Task.IsNotificationTimeSet ? Task.Notificationtime : Task.Time);
            alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, DateTimeOffset.Parse(NotificationTime.ToString()).ToUnixTimeMilliseconds(), pendingIntent);
            Toast.MakeText(ApplicationContext, AppResources.Notification_Scheduled, ToastLength.Long).Show();
        }

        public void CancelNotification(Task Task)
        {
            Intent intent = new Intent(ApplicationContext, typeof(AlarmReceiver));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(ApplicationContext, Task.Id, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);
            AlarmManager alarmManager = (AlarmManager)GetSystemService(AlarmService);
            alarmManager.Cancel(pendingIntent);
        }

        protected async override void OnNewIntent(Intent intent)
        {
            if (TaskManager.Tasks == null)
                TaskManager.LoadTasks();

            if (AndroidServiceManager.Ringtone != null && AndroidServiceManager.Ringtone.IsPlaying)
                await Shell.Current.GoToAsync($"{nameof(AlarmPage)}",
                    new Dictionary<string, object> { { "Task", TaskManager.Tasks.FirstOrDefault(task => task.Id == AndroidServiceManager.TaskId) }});
        }
    }
}