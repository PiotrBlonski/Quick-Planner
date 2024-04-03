using Quick_Planner.Models;
using System.Collections.ObjectModel;

namespace Quick_Planner
{
    static class ThemeManager
    {
        public static ObservableCollection<Palette> Palettes = new();
        public static void UpdateTheme()
        {
            Application.Current.Resources["Primary"] = GetColor(3);
            Application.Current.Resources["Secondary"] = GetColor(2);
            Application.Current.Resources["Tertiary"] = GetColor(1);
            Application.Current.Resources["Quaternary"] = GetColor(0);
        }

        public static Color GetColor(int Index) => Palettes[Settings.ThemeIndex].Colors[Index];

        public static void LoadDefaultPalettes()
        {
            Color[,] Colors = new Color[,] { { Color.FromHex("#352F44"), Color.FromHex("#5C5470"), Color.FromHex("#B9B4C7"), Color.FromHex("#FAF0E6") },
                                            { Color.FromHex("#2D4356"), Color.FromHex("#435B66"), Color.FromHex("#A76F6F"), Color.FromHex("#EAB2A0") },
                                            { Color.FromHex("#1F1717"), Color.FromHex("#CE5A67"), Color.FromHex("#F4BF96"), Color.FromHex("#FCF5ED") },
                                            { Color.FromHex("#6B240C"), Color.FromHex("#994D1C"), Color.FromHex("#E48F45"), Color.FromHex("#F5CCA0") },
                                            { Color.FromHex("#4F4A45"), Color.FromHex("#6C5F5B"), Color.FromHex("#ED7D31"), Color.FromHex("#F6F1EE") },
                                            { Color.FromHex("#7C93C3"), Color.FromHex("#9EB8D9"), Color.FromHex("#A25772"), Color.FromHex("#EEF5FF") },
                                            { Color.FromHex("#607274"), Color.FromHex("#B2A59B"), Color.FromHex("#DED0B6"), Color.FromHex("#FAEED1") }
                                        };
            for (var c = 0; c < Colors.GetLength(0); c++)
            {
                Color[] CurrentColors = new Color[4];

                for (var i = 0; i < 4; i++)
                    CurrentColors[i] = Colors[c, i];

                Palettes.Add(new Palette(CurrentColors, c));
            }
        }
    }
}
