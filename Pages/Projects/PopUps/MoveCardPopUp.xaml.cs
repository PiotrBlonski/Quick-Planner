using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class MoveCardPopUp : Popup
{
	public MoveCardPopUp(EditCardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}