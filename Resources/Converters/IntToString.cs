using System.Globalization;

namespace Quick_Planner.Resources.Converters
{
    internal class IntToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 1)
                return "notif_icon.png";
            if ((int)value == 2)
                return "speaker_icon.png";
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "notif_icon.png")
                return 1.ToString();
            if ((string)value == "speaker_icon.png")
                return 2.ToString();
            else
                return 0.ToString();
        }
    }
}
