using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class BoardRemoveCardsPopUp : Popup
{
	public BoardRemoveCardsPopUp(EditBoardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}