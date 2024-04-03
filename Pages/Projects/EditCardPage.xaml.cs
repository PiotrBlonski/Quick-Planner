using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects;

public partial class EditCardPage : ContentPage
{
	public EditCardPage(EditCardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private async void Return(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}