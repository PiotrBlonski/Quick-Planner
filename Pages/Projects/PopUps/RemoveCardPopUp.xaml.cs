using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class RemoveCardPopUp : Popup
{
	public RemoveCardPopUp(EditCardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}