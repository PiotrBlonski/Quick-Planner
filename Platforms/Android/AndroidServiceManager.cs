using Android.Media;
using Android.OS;
using Task = Quick_Planner.Models.Task;

namespace Quick_Planner.Platforms.Android
{
    public static class AndroidServiceManager
    {
        public static Ringtone Ringtone = null;
        public static Vibrator Vibrator = null;
        public static int TaskId;
        public static MainActivity MainActivity { get; set; }

        public static void ScheduleNotification(Task Task)
        {
            if (MainActivity == null)
                return;

            MainActivity.ScheduleNotification(Task);
        }

        public static void CancelNotification(Task Task)
        {
            if (MainActivity == null)
                return;

            MainActivity.CancelNotification(Task);
        }
    }
}
