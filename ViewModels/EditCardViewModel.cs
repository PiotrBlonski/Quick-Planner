using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quick_Planner.Models;
using Quick_Planner.Pages.Projects.PopUps;
using Quick_Planner.Resources;
using System.Collections.ObjectModel;
using Label = Quick_Planner.Models.Label;
using Task = System.Threading.Tasks.Task;
namespace Quick_Planner.ViewModels
{
    [QueryProperty("Card", "Card")]
    public partial class EditCardViewModel : ObservableObject
    {
        [ObservableProperty]
        Card card;

        [ObservableProperty]
        ChecklistElement checklistElement;

        [ObservableProperty]
        string archiveButtonText = AppResources.Restore_Card;

        [ObservableProperty]
        List<List> lists;

        [ObservableProperty]
        ObservableCollection<Label> labels;

        Popup Popup;

        [RelayCommand]
        public async Task OpenLabelsWindow()
        {
            Labels = BoardManager.CurrentBoard.Labels;
            foreach (var Label in Labels) 
                Label.IsSelected = Card.Labels.Contains(Label);

            Popup = new CardAddLabelsPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task OpenCheckListWindow()
        {
            ChecklistElement = new ChecklistElement(AppResources.New_Checkmark);
            Popup = new AddCheckElementPopIUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task OpenMoveWindow()
        {
            Lists = BoardManager.CurrentBoard.Lists.Where(list => list.Id != Card.OwnerId).ToList();

            if(BoardManager.CurrentBoard.Lists.FirstOrDefault(list => list.Id == Card.OwnerId) != null)
                Lists.Insert(0, new List() { Title = AppResources.Board, Id = -1 });

            foreach (List List in BoardManager.CurrentBoard.Lists)
                List.IsSelected = false;

            Popup = new MoveCardPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task MoveCard()
        {
            List Destination = Lists.FirstOrDefault(list => list.IsSelected);

            if (Destination != null)
            {
                Card = BoardManager.CurrentBoard.Cards.First(card => card.Id == Card.Id);
                Card.OwnerId = Destination.Id;
            }

            await HideWindow();
        }

        [RelayCommand]
        public async Task OpenRemoveWindow()
        {
            Popup = new RemoveCardPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task HideWindow()
        {
            await Popup.CloseAsync();
        }

        [RelayCommand]
        public void SelectLabel(Label Label) =>Label.IsSelected = !Label.IsSelected;

        [RelayCommand]
        public void SelectList(List List)
        {
            List.IsSelected = true;
            foreach (List list in Lists.Where(list => list.Id != List.Id))
                list.IsSelected = false;
        }

        [RelayCommand]
        public async Task ApplyLabels()
        {
            Card.LabelIds = Labels.Where(l => l.IsSelected).Select(l => l.Id).ToObservableCollection();
            Card.UpdateLabels();
            await HideWindow();
        }

        [RelayCommand]
        public async Task AddCheckmark()
        {
            Card.CheckListElements.Add(ChecklistElement);
            await HideWindow();
        }

        [RelayCommand]
        public void RemoveCheckmark(ChecklistElement CheckMark) => Card.CheckListElements.Remove(CheckMark);
       

        [RelayCommand]
        public async Task RemoveCard()
        {
            Card.RemoveSelf();
            await Popup.CloseAsync();
            await Shell.Current.GoToAsync("..");
        }
    }
}
