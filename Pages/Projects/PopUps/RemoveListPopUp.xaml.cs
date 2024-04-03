using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class RemoveListPopUp : Popup
{
	public RemoveListPopUp(EditListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}