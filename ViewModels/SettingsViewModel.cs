using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocalizationResourceManager.Maui;
using Quick_Planner.Models;
using System.Collections.ObjectModel;

namespace Quick_Planner.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        ILocalizationResourceManager LocalizationResourceManager;
        public SettingsViewModel(ILocalizationResourceManager localizationResourceManager) 
        {
            Palettes = ThemeManager.Palettes;
            Languages = LanguageManager.Languages;
            LocalizationResourceManager = localizationResourceManager;
        }

        [ObservableProperty]
        ObservableCollection<Palette> palettes;

        [ObservableProperty]
        ObservableCollection<Language> languages;

        [RelayCommand]
        public void SetLanguage(string Code)
        {
            int LanguageIndex = LanguageManager.GetLanguageIndexByCode(Code);
            Languages[Settings.LanguageIndex].IsSelected = false;
            Languages[LanguageIndex].IsSelected = true;
            LocalizationResourceManager.CurrentCulture = new System.Globalization.CultureInfo(Code);
            Settings.LanguageIndex = LanguageIndex;
            Settings.Save();
        }

        [RelayCommand]
        public void SetTheme(int Index)
        {
            Palettes[Settings.ThemeIndex].IsSelected = false;
            Palettes[Index].IsSelected = true;
            Settings.ThemeIndex = Index;
            try { ThemeManager.UpdateTheme(); }
            catch { };
            Settings.Save();
        }
    }
}
