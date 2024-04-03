using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Quick_Planner.Models
{
    public partial class Board : ObservableObject
    {
        [ObservableProperty]
        public string filename;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public ObservableCollection<Label> labels;
        [ObservableProperty]
        public ObservableCollection<List> lists;
        [ObservableProperty]
        public ObservableCollection<Card> cards;

        public Board() => Filename = $"{Path.GetRandomFileName()}.board.txt";

        partial void OnCardsChanged(ObservableCollection<Card> value) => value.CollectionChanged += Cards_CollectionChanged;

        public IEnumerable<Card> UnAssignedCards => Cards.Where(card => Lists.FirstOrDefault(list => list.Id == card.OwnerId) == null);

        public override string ToString()
        {
            string LabelsString = "";
            for (int i = 0; i < Labels?.Count; i++)
                LabelsString += Labels[i].ToString() + (i == Labels.Count - 1 ? "" : ">");

            string ListsString = "";
            for (int i = 0; i < Lists?.Count; i++)
                ListsString += Lists[i].ToString() + (i == Lists.Count - 1 ? "" : ">");

            string CardsString = "";
            for (var i = 0; i < Cards?.Count; i++)
                CardsString += Cards[i].ToString() + (i == Cards.Count - 1 ? "" : ">");

            return Name + "\n" + LabelsString + "\n" + ListsString + "\n" + CardsString;
        }

        public void Save() => File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, Filename), ToString());
        public void Delete() => File.Delete(Path.Combine(FileSystem.AppDataDirectory, Filename));
        public static Board Load(string filename)
        {
            try {
                filename = Path.Combine(FileSystem.AppDataDirectory, filename);

                if (!File.Exists(filename))
                    throw new FileNotFoundException("Unable to find file on local storage.", filename);

                string[] SplitString = File.ReadAllText(filename).Split('\n');
                string Name = SplitString[0];
                string[] Labels = SplitString[1].Split('>');
                string[] Lists = SplitString[2].Split('>');
                string[] Cards = SplitString[3].Split('>');

                ObservableCollection<Label> FileLabels = new ObservableCollection<Label>();
                if (!string.IsNullOrEmpty(SplitString[1]))
                    for (int i = 0; i < Labels.Length; i++)
                        FileLabels.Add(Label.Parse(Labels[i]));

                ObservableCollection<List> FileLists = new ObservableCollection<List>();
                if (!string.IsNullOrEmpty(SplitString[2]))
                    for (int i = 0; i < Lists.Length; i++)
                        FileLists.Add(List.Parse(Lists[i]));

                ObservableCollection<Card> FileCards = new ObservableCollection<Card>();
                if (!string.IsNullOrEmpty(SplitString[3]))
                    for (int i = 0; i < Cards.Length; i++)
                        FileCards.Add(Card.Parse(Cards[i]));

                return new()
                {
                    Filename = Path.GetFileName(filename),
                    Name = Name,
                    Labels = FileLabels,
                    Lists = FileLists,
                    Cards = FileCards
                };
            } catch { return null; }
        }

        public static IEnumerable<Board> LoadAll()
        {
            string appDataPath = FileSystem.AppDataDirectory;

            return Directory.EnumerateFiles(appDataPath, "*.board.txt")
                            .Select(filename => Load(Path.GetFileName(filename)))
                            .Where(board => board != null)
                            .OrderByDescending(board => board.Name);
        }

        [RelayCommand]
        public void DeleteList(List List) => Lists.Remove(List);

        public void Cards_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                UpdateItems(e.OldItems);
            if (e.NewItems != null)
                UpdateItems(e.NewItems);
        }

        private void UpdateItems(System.Collections.IList Items)
        {
            foreach (Card Card in Items)
            {
                List Destination = Lists.FirstOrDefault(list => list.Id == Card.OwnerId);
                if (Destination != null)
                    Destination.UpdateCards();
                else
                    UpdateCards();
            }
        }

        public void UpdateCards() => OnPropertyChanged(nameof(UnAssignedCards));
    }
}
