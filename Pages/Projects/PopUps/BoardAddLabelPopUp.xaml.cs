using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class BoardAddLabelPopUp : Popup
{
	public BoardAddLabelPopUp(EditBoardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}