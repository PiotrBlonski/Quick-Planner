using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects;

public partial class EditBoardPage : ContentPage
{
    EditBoardViewModel viewModel;
	public EditBoardPage(EditBoardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        viewModel = vm;
	}

    private async void Return(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}