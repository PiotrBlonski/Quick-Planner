using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class AddCheckElementPopIUp : Popup
{
	public AddCheckElementPopIUp(EditCardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}