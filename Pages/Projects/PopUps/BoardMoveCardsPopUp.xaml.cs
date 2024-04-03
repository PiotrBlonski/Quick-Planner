using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class BoardMoveCardsPopUp : Popup
{
	public BoardMoveCardsPopUp(EditBoardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}