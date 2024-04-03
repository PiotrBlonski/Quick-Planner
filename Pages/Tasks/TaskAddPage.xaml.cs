using Quick_Planner.Resources;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Tasks;
public partial class TaskAddPage : ContentPage
{
    TaskAddViewModel viewModel;
    public TaskAddPage(TaskAddViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        viewModel = vm;
        vm.TaskAddPage = this;
	}

    public void PopulatePicker()
    {
        List<string> Elements = new List<string> { AppResources.None, AppResources.Notification, AppResources.Alarm };
        Picker Picker = FindByName("NotificationPicker") as Picker; 
        Picker.ItemsSource = Elements;
        Picker.SelectedIndex = viewModel.CurrentTask.Notificationtype;
    }
}