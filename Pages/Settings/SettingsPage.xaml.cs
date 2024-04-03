using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Settings;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}