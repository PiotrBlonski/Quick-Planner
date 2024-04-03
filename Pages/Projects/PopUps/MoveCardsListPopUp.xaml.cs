using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class MoveCardsListPopUp : Popup
{
	public MoveCardsListPopUp(EditListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}