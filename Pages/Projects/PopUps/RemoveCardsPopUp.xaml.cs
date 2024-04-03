using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class RemoveCardsPopUp : Popup
{
	public RemoveCardsPopUp(EditListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}