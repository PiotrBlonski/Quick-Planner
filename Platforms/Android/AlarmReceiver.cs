using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using AndroidX.Core.App;
using Uri = Android.Net.Uri;

namespace Quick_Planner.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string Message = intent.GetStringExtra("textExtra").ToString();
            string Title = intent.GetStringExtra("titleExtra").ToString();
            AndroidServiceManager.TaskId = intent.GetIntExtra("TaskIdExtra", -1);
            var Notification = new NotificationCompat.Builder(context, MainApplication.ChannelId)
                                .SetSmallIcon(Resource.Drawable.appicon)
                                .SetContentText(Message)
                                .SetContentTitle(Title)
                                .Build();
            var Manager = context.GetSystemService(Context.NotificationService) as NotificationManager;
            if (intent.GetIntExtra("notifExtra", 0) == 2)
            {
                Intent notificationIntent = new Intent(context, typeof(MainActivity));

                notificationIntent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);

                PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, notificationIntent, PendingIntentFlags.Immutable);

                Notification.ContentIntent = pendingIntent;
                Notification.DeleteIntent = PendingIntent.GetBroadcast(context, 0, new Intent(context, typeof(NotificationDismissReceiver)), PendingIntentFlags.Immutable);

                Uri alarmUri = RingtoneManager.GetDefaultUri(RingtoneType.Alarm) ?? RingtoneManager.GetDefaultUri(RingtoneType.Notification);

                AndroidServiceManager.Ringtone = RingtoneManager.GetRingtone(context, alarmUri);
                AndroidServiceManager.Vibrator = Build.VERSION.SdkInt >= BuildVersionCodes.S ? ((VibratorManager)context.GetSystemService(Context.VibratorManagerService)).DefaultVibrator : (Vibrator)context.GetSystemService(Context.VibratorService);
                AndroidServiceManager.Ringtone.Play();
                AndroidServiceManager.Vibrator.Vibrate(VibrationEffect.CreatePredefined(0));
            }
            Manager.Notify(new object().GetHashCode(), Notification);
        }
    }
}
