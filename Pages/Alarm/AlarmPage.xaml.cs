namespace Quick_Planner.Pages.Alarm;
using Quick_Planner.ViewModels;

public partial class AlarmPage : ContentPage
{
	public AlarmPage(AlarmViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	void StopAlarm(object sender, EventArgs e)
	{
#if ANDROID
		Quick_Planner.Platforms.Android.AndroidServiceManager.Ringtone.Stop();
		Quick_Planner.Platforms.Android.AndroidServiceManager.Vibrator.Cancel();
		Shell.Current.GoToAsync("..");
#endif
    }
}