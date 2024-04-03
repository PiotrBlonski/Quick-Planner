using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quick_Planner.Pages.Projects;
using Quick_Planner.Resources;

namespace Quick_Planner.Models
{
    public partial class List : ObservableObject
    {
        [ObservableProperty]
        public string title;
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        bool isSelected = false;

        public List()
        {
            Title = "";
            Id = (new object()).GetHashCode();
        }
        public override string ToString() =>  Id + "," + Title;

        public IEnumerable<Card> OwnedCards => BoardManager.CurrentBoard.Cards.Where(card => card.OwnerId == Id);

        public static List Parse(string Text)
        {
            string[] SplitText = Text.Split(',', StringSplitOptions.RemoveEmptyEntries);

            return new()
            {
                Id = int.Parse(SplitText[0]),
                Title = SplitText[1]
            };
        }

        [RelayCommand]
        async System.Threading.Tasks.Task Edit()
        {
            BoardManager.CurrentListIndex = BoardManager.Boards[BoardManager.CurrentBoardIndex].Lists.IndexOf(this);
            await Shell.Current.GoToAsync($"{nameof(EditListPage)}",
                new Dictionary<string, object> { { "List", this }
                });
        }

        [RelayCommand]
        public void AddCard()
        {
            BoardManager.CurrentBoard.Cards.Add(new() { Title = AppResources.New_Card, OwnerId = Id });
        }

        public void UpdateCards()
        {
            OnPropertyChanged(nameof(OwnedCards));
        }
    }
}
