using Android.App;
using Android.Content;
using Android.OS;

namespace Quick_Planner.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    class NotificationDismissReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            AndroidServiceManager.Ringtone?.Stop();
            AndroidServiceManager.Vibrator?.Cancel();
        }
    }
}
