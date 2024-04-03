using CommunityToolkit.Mvvm.ComponentModel;

namespace Quick_Planner.Models
{
    public partial class Palette : ObservableObject
    {
        [ObservableProperty]
        public List<Color> colors;
        [ObservableProperty]
        public int index;
        [ObservableProperty]
        public bool isSelected;
        public Palette(Color[] Colors, int Index) 
        {
            this.Colors = Colors.ToList();
            this.Index = Index;
            IsSelected = false;
        }
    }
}
