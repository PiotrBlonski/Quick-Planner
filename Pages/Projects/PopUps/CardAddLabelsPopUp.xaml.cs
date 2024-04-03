using CommunityToolkit.Maui.Views;
using Quick_Planner.ViewModels;

namespace Quick_Planner.Pages.Projects.PopUps;

public partial class CardAddLabelsPopUp : Popup
{
	public CardAddLabelsPopUp(EditCardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}