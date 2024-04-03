using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quick_Planner.Models;
using Quick_Planner.Pages.Projects.PopUps;
using Quick_Planner.Resources;
using System.Collections.ObjectModel;
using Task = System.Threading.Tasks.Task;

namespace Quick_Planner.ViewModels
{
    [QueryProperty("List", "List")]
    public partial class EditListViewModel : ObservableObject
    {
        [ObservableProperty]
        List list;

        [ObservableProperty]
        ObservableCollection<List> lists;

        Popup Popup;

        [RelayCommand]
        public async Task OpenRemoveListWindow()
        {
            Popup = new RemoveListPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task OpenRemoveCardsWindow()
        {
            Popup = new RemoveCardsPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public async Task OpenMoveCardsWindow()
        {
            foreach (List List in BoardManager.CurrentBoard.Lists)
                List.IsSelected = false;
            Lists = BoardManager.CurrentBoard.Lists.Where(l => l.Id != List.Id).ToObservableCollection();

            Lists.Insert(0, new List() { Title = AppResources.Board, Id = -1 });

            Popup = new MoveCardsListPopUp(this);
            await Shell.Current.ShowPopupAsync(Popup);
        }

        [RelayCommand]
        public void SelectList(List List)
        {
            for (var l = 0; l < Lists.Count; l++)
                Lists.ElementAt(l).IsSelected = false;
            Lists.ElementAt(Lists.IndexOf(Lists.First(l => l.Id == List.Id))).IsSelected = true;
        }

        [RelayCommand]
        public async Task MoveCards()
        {
            List Destination = Lists.FirstOrDefault(l => l.IsSelected);
            if (Destination != null)
            {
                foreach (Card Card in BoardManager.CurrentBoard.Cards.Where(card => card.OwnerId == List.Id))
                {
                    Card.OwnerId = Destination.Id;
                }
            }
            await HideWindow();
        }

        [RelayCommand]
        public async Task RemoveList()
        {
            BoardManager.CurrentBoard.Lists.Remove(List);
            await HideWindow();
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task RemoveCards()
        {
            foreach (var Card in BoardManager.CurrentBoard.Cards.Where(card => card.OwnerId == List.Id).ToList())
                if (Card.OwnerId == List.Id)
                    BoardManager.CurrentBoard.Cards.Remove(Card);
            await HideWindow();
        }

        [RelayCommand]
        public async Task HideWindow()
        {
            await Popup.CloseAsync();
        }
    }
}
