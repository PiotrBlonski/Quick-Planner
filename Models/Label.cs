using CommunityToolkit.Mvvm.ComponentModel;
using Quick_Planner.Resources;
using Color = Microsoft.Maui.Graphics.Color;

namespace Quick_Planner.Models
{
    public partial class Label : ObservableObject
    {
        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public Color color;

        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public bool isSelected;
        public Label()
        {
            Id = (new object()).GetHashCode();
            Title = AppResources.New_Label;
            Color = Color.FromRgb(255, 255, 255);
            IsSelected = false;
        }

        public override string ToString()
        {
            return Id + "," + Title + "," + Color.ToHex();
        }

        public static Label Parse(string Text)
        {
            string[] SplitText = Text.Split(',');
            return new Label() { Id = int.Parse(SplitText[0]), Title = SplitText[1], Color = Color.FromArgb(SplitText[2])};
        }
    }
}
