using System.Globalization;

namespace Quick_Planner.Models
{
    public static class Settings
    {
        public static int ThemeIndex = 0;
        public static int LanguageIndex = 0;
        public static string FileLocation = Path.Combine(FileSystem.AppDataDirectory, "Settings.txt");

        public static bool FileExits => File.Exists(FileLocation);

        public static void Save()
        {
            File.WriteAllText(FileLocation, LanguageIndex + "," + ThemeIndex);
        }

        public static void Load()
        {
            if (!FileExits)
            {
                LanguageIndex = LanguageManager.GetLanguageIndexByCode(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
                Save();
            }

            string[] Text = File.ReadAllText(FileLocation).Split(',');

            LanguageIndex = int.Parse(Text[0]);
            ThemeIndex = int.Parse(Text[1]);

            LanguageManager.Languages[LanguageIndex].IsSelected = true;
            ThemeManager.Palettes[ThemeIndex].IsSelected = true;
        }
    }
}
