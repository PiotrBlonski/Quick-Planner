using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quick_Planner.Pages.Projects;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;

namespace Quick_Planner.Models
{
    public partial class Card : ObservableObject
    {
        [ObservableProperty]
        public string title;
        [ObservableProperty] 
        public string description;
        [ObservableProperty]
        public string coverImagePath;
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public bool hasDueDate;
        [ObservableProperty]
        public DateTime dueDate;
        [ObservableProperty]
        public ObservableCollection<int> labelIds;
        [ObservableProperty]
        public ObservableCollection<ChecklistElement> checkListElements;
        [ObservableProperty]
        public int ownerId;

        public Card()
        {
            Title = "";
            Description = "";
            CoverImagePath = "";
            Id = (new object()).GetHashCode();
            HasDueDate = false;
            DueDate = DateTime.Now;
            LabelIds = new ObservableCollection<int>();
            CheckListElements = new ObservableCollection<ChecklistElement>();
            OwnerId = -1;
        }

        public string DateOnly => DueDate.ToShortDateString();

        public IEnumerable<Label> Labels => BoardManager.CurrentBoard?.Labels.Where(label => LabelIds.Contains(label.Id));

        partial void OnLabelIdsChanged(ObservableCollection<int> value) => value.CollectionChanged += NotifyLabels;

        void NotifyLabels(object sender, NotifyCollectionChangedEventArgs e) => UpdateLabels();

        public void UpdateLabels() => OnPropertyChanged(nameof(Labels));

        partial void OnOwnerIdChanged(int oldValue, int newValue)
        {
            List From = BoardManager.CurrentBoard?.Lists.FirstOrDefault(list => list.Id == oldValue);
            From?.UpdateCards();

            List To = BoardManager.CurrentBoard?.Lists.FirstOrDefault(list => list.Id == newValue);
            To?.UpdateCards();

            if (From == null || To == null)
                BoardManager.CurrentBoard?.UpdateCards();
        }

        [RelayCommand]
        public void RemoveSelf()
        {
            if (BoardManager.CurrentBoard.Cards.FirstOrDefault(this) != null)
                BoardManager.CurrentBoard.Cards.Remove(this);
        }

        [RelayCommand]
        async System.Threading.Tasks.Task Edit() => await Shell.Current.GoToAsync($"{nameof(EditCardPage)}", new Dictionary<string, object> { { "Card", this }});

        public override string ToString()
        {
            string LabelsString = string.Empty;
            for (int i = 0; i < LabelIds.Count; i++)
                if(BoardManager.CurrentBoard.Labels.FirstOrDefault(label => label.Id == LabelIds[i]) != null)
                    LabelsString += LabelIds[i].ToString() + (i == (LabelIds.Count - 1) ? "" : "{"); ;

            string CheckListString = string.Empty;
            for (var i = 0; i < CheckListElements.Count; i++)
                CheckListString += CheckListElements[i].ToString() + (i == (CheckListElements.Count - 1) ? "" : "{");

            return OwnerId + "," + Title + "," + Description + "," + CoverImagePath + "," + HasDueDate.ToString() + "," + DueDate.ToString(CultureInfo.InvariantCulture) 
                + ";" + LabelsString + ";" + CheckListString;
        }

        public static Card Parse(string Text)
        {
            string[] SplitText = Text.Split(';');
            string[] SplitData = SplitText[0].Split(',');
            string[] SplitLabels = SplitText[1].Split("{");
            string[] SplitElements = SplitText[2].Split("{");

            ObservableCollection<int> FileLabels = new ObservableCollection<int>();
            if (!string.IsNullOrEmpty(SplitLabels[0]))
                for (int i = 0; i < SplitLabels.Length; i++)
                    FileLabels.Add(int.Parse(SplitLabels[i]));

            ObservableCollection<ChecklistElement> FileElements = new ObservableCollection<ChecklistElement>();
            if (!string.IsNullOrEmpty(SplitElements[0]))
                for (int i = 0; i < SplitElements.Length; i++)
                    FileElements.Add(ChecklistElement.Parse(SplitElements[i]));

            int OwnerId = -1;
            int.TryParse(SplitData[0], out OwnerId);

            return new()
            {
                OwnerId = OwnerId,
                Title = SplitData[1],
                Description = SplitData[2],
                CoverImagePath = SplitData[3],
                HasDueDate = bool.Parse(SplitData[4]),
                DueDate = DateTime.Parse(SplitData[5], CultureInfo.InvariantCulture),
                LabelIds = FileLabels,
                CheckListElements = FileElements
            };
        }
    }
}
