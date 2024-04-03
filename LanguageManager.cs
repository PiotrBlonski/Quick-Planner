using Quick_Planner.Models;
using System.Collections.ObjectModel;

namespace Quick_Planner
{
    static class LanguageManager
    {
        public static ObservableCollection<Language> Languages = new ObservableCollection<Language> { new Language("English", "en", "flag_us_icon.png"), new Language("Polski", "pl", "flag_pl_icon.png") };

        public static int GetLanguageIndexByCode(string Code) => Languages.IndexOf(Languages.FirstOrDefault(lan => lan.Code == Code));

        public static Language GetCurrenlyUsedLanguage() => Languages[Settings.LanguageIndex];
    }
}
