using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class BoardRemoveLabelPopUp : Popup
{
	public BoardRemoveLabelPopUp(EditBoardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}