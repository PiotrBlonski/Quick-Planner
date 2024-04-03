using Android.App;
using Android.OS;
using Android.Runtime;
using Quick_Planner.Resources;

namespace Quick_Planner
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public static readonly string ChannelId = "notificationServiceChannel";
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override void OnCreate()
        {
            base.OnCreate();

            if(Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                string Name = ChannelId;
                string Description = AppResources.Channel_Description;
                var Channel = new NotificationChannel(Name, Description, NotificationImportance.High);
                Channel.Description = Description;
                if(GetSystemService(NotificationService) is NotificationManager manager)
                    manager.CreateNotificationChannel(Channel);
            }
        }
    }
}