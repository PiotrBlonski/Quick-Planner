using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quick_Planner.Models;
using Quick_Planner.Pages.Projects;
using Quick_Planner.Resources;
using Task = System.Threading.Tasks.Task;

namespace Quick_Planner.ViewModels
{
    [QueryProperty("Board", "Board")]
    public partial class BoardViewModel : ObservableObject
    {
        [ObservableProperty]
        public Board board;
        [ObservableProperty]
        object currentObject;
        [ObservableProperty]
        object onElement;

        [RelayCommand]
        async Task EditBoard()
        {
            await Shell.Current.GoToAsync($"{nameof(EditBoardPage)}",
                new Dictionary<string, object> { { "Board", Board }
                });
        }

        [RelayCommand]
        public void SetItem(object Object) => CurrentObject = Object;

        [RelayCommand]
        public void AddList() => Board.Lists.Add(new() { Title = AppResources.New_List });

        [RelayCommand]
        public void MoveItem(object Object)
        {
            if (CurrentObject is Card CurrentCard)
            {
                if (Object is List List && List.Id != CurrentCard.OwnerId)
                    CurrentCard.OwnerId = List.Id;

                if (Object is Card Card)
                {
                    int From = BoardManager.CurrentBoard.Cards.IndexOf(CurrentCard);
                    int To = BoardManager.CurrentBoard.Cards.IndexOf(Card);

                    BoardManager.CurrentBoard.Cards.Move(From, To);

                    if (CurrentCard.OwnerId != Card.OwnerId)
                        CurrentCard.OwnerId = Card.OwnerId;
                }
            }
            if (CurrentObject is List CurrentList)
            {
                int From = BoardManager.CurrentBoard.Lists.IndexOf(CurrentList);
                int To = BoardManager.CurrentBoard.Lists.IndexOf(Object is List ? (List)Object : BoardManager.CurrentBoard.Lists.FirstOrDefault(list => list.Id == ((Card)Object).OwnerId));
                BoardManager.CurrentBoard.Lists.Move(From, To);
            }
        }

        [RelayCommand]
        public void SetDrag(object Object) => OnElement = Object;
    }
}
