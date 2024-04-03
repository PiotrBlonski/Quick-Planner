using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class BoardRemovePopUp : Popup
{
	public BoardRemovePopUp(EditBoardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}