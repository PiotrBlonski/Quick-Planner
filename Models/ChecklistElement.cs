using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Quick_Planner.Models
{
    public partial class ChecklistElement : ObservableObject
    {
        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public bool isChecked;

        public ChecklistElement(string Title)
        {
            this.Title = Title;
        }
        public ChecklistElement(string Title, bool IsChecked)
        {
            this.Title = Title;
            this.IsChecked = IsChecked;
        }
        public override string ToString()
        {
            return Title + "," + IsChecked.ToString();
        }

        public static ChecklistElement Parse(string Text)
        {
            string[] SplitText = Text.Split(',');
            return new ChecklistElement(SplitText[0], bool.Parse(SplitText[1]));
        }

        [RelayCommand]
        public void ChangeCheckState()
        {
            IsChecked = !IsChecked;
        }
    }
}
