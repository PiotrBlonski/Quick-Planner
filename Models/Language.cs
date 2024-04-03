using CommunityToolkit.Mvvm.ComponentModel;

namespace Quick_Planner.Models
{
    public partial class Language : ObservableObject
    {
        [ObservableProperty]
        string fullName;

        [ObservableProperty]
        string code;

        [ObservableProperty]
        string imageName;

        [ObservableProperty]
        bool isSelected;

        public Language(string FullName, string Code, string ImageName) 
        { 
            this.FullName = FullName;
            this.Code = Code;
            this.ImageName = ImageName;
            IsSelected = false;
        }
    }
}
