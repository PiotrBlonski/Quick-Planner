using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quick_Planner.Models;
using Quick_Planner.Pages.Projects.PopUps;
using Quick_Planner.Resources;
using Label = Quick_Planner.Models.Label;
using Task = System.Threading.Tasks.Task;

namespace Quick_Planner.ViewModels
{
    [QueryProperty("Board", "Board")]
    public partial class EditBoardViewModel : ObservableObject
    {
        [ObservableProperty]
        Board board;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(GetColor), nameof(RedText))]
        float red;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(GetColor), nameof(GreenText))]
        float green;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(GetColor), nameof(BlueText))]
        float blue;

        [ObservableProperty]
        string currentTitle = AppResources.New_Label;

        [ObservableProperty]
        Label currentLabel = new();

        public Color GetColor => new(Red, Green, Blue);

        public string RedText => AppResources.Red + " : " + Math.Round(Red * 100, 1) + "%";

        public string GreenText => AppResources.Green + " : " + Math.Round(Green * 100, 1) + "%";

        public string BlueText => AppResources.Blue + " : " + Math.Round(Blue * 100, 1) + "%";

        Popup Popup;

        [RelayCommand]
        public async Task OpenLabelEditorWindow(Label Label)
        {
            RestartLabelData();

            if (Label == null)
                CurrentLabel = new Label();
            else
                CurrentLabel = Label;

            CurrentTitle = CurrentLabel.Title;
            Red = CurrentLabel.Color.Red;
            Green = CurrentLabel.Color.Green;
            Blue = CurrentLabel.Color.Blue;

            Popup = new BoardAddLabelPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task OpenMoveCardsWindow()
        {
            foreach (List List in Board.Lists)
                List.IsSelected = false;

            Popup = new BoardMoveCardsPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task OpenRemoveCardsWindow()
        {
            Popup = new BoardRemoveCardsPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task OpenDeleteWindow()
        {
            Popup = new BoardRemovePopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task OpenRemoveLabelWindow(Label Label)
        {
            Popup = new BoardRemoveLabelPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
            CurrentLabel = Label;
        }

        [RelayCommand]
        public async Task HideWindow()
        {
            await Popup.CloseAsync();
        }

        [RelayCommand]
        public void SelectList(List List)
        {
            foreach (List list in Board.Lists)
                list.IsSelected = false;
            List.IsSelected = true;
        }

        [RelayCommand]
        public async Task MoveCards()
        {
            List Destination = Board.Lists.FirstOrDefault(l => l.IsSelected);
            if (Destination != null)
            {
                foreach (Card Card in Board.UnAssignedCards)
                {
                    Card.OwnerId = Destination.Id;
                }
            }
            await HideWindow();
        }

        [RelayCommand]
        public async Task RemoveCards()
        {
            foreach (var Card in Board.UnAssignedCards.ToList())
                Board.Cards.Remove(Card);
            await HideWindow();
        }

        [RelayCommand]
        public async Task RemoveBoard()
        {
            await Popup.CloseAsync();
            await Shell.Current.GoToAsync("../..");
            Board.Delete();
            BoardManager.Boards.Remove(Board);
        }

        [RelayCommand]
        public async Task RemoveLabel()
        {
            foreach (Card Card in Board.Cards)
                Card.LabelIds.Remove(CurrentLabel.Id);

            Board.Labels.Remove(CurrentLabel);
            await HideWindow();
        }

        [RelayCommand]
        public async Task SaveLabel()
        {
            if (!Board.Labels.Contains(CurrentLabel))
                Board.Labels.Add(CurrentLabel);

            CurrentLabel.Color = GetColor;
            CurrentLabel.Title = CurrentTitle;

            await HideWindow();
        }

        void RestartLabelData()
        {
            Red = 255;
            Green = 255;
            Blue = 255;
            CurrentTitle = AppResources.New_Label;
        }
    }
}
