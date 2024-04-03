using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;

namespace Quick_Planner.Models
{
    public partial class Task : ObservableObject
    {
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string filename;
        [ObservableProperty]
        public string title;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasPassed))]
        public bool hasEndDate;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DateOnly))]
        public DateTime date;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CultureAwareTime))]
        public TimeSpan time;
        [ObservableProperty]
        public int notificationtype;
        [ObservableProperty]
        public bool isNotificationTimeSet;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CultureAwareNotificationTime))]
        public TimeSpan notificationtime;

        public Task()
        {
            id = (new object()).GetHashCode();
            Filename = $"{Path.GetRandomFileName()}.task.txt";
            Title = "";
            HasEndDate = false;
            Date = DateTime.Today;
            Time = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, 0);
            Notificationtype = 0;
            IsNotificationTimeSet = false;
            Notificationtime = TimeSpan.Zero;
        }

        public string DateOnly => Date.ToShortDateString();

        public string CultureAwareTime => DateTime.MinValue.Add(Time).ToShortTimeString();

        public string CultureAwareNotificationTime => DateTime.MinValue.Add(Notificationtime).ToShortTimeString();

        public bool HasPassed => HasEndDate ? Date + Time < DateTime.Now : false;

        public override string ToString() => Id + "," + Title + "," + HasEndDate + "," + Date.ToString(CultureInfo.InvariantCulture) + "," + Time + "," + Notificationtype + "," + Notificationtime + "," + IsNotificationTimeSet;

        public void Save() =>
            File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, Filename), ToString());

        public void Delete() =>
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, Filename));

        public static Task Load(string filename)
        {
            try {
                filename = Path.Combine(FileSystem.AppDataDirectory, filename);

                if (!File.Exists(filename))
                    throw new FileNotFoundException("Unable to find file on local storage.", filename);

                string[] Text = File.ReadAllText(filename).Split(',');
                return new()
                {
                    Filename = Path.GetFileName(filename),
                    Id = int.Parse(Text[0]),
                    Title = Text[1],
                    HasEndDate = bool.Parse(Text[2]),
                    Date = DateTime.Parse(Text[3], CultureInfo.InvariantCulture),
                    Time = TimeSpan.Parse(Text[4]),
                    Notificationtype = int.Parse(Text[5]),
                    Notificationtime = TimeSpan.Parse(Text[6]),
                    IsNotificationTimeSet = bool.Parse(Text[7])
                };
            }
            catch { return null; }
        }

        public static IEnumerable<Task> LoadAll()
        {
            return Directory.EnumerateFiles(FileSystem.AppDataDirectory, "*.task.txt")
                            .Select(filename => Load(Path.GetFileName(filename)))
                            .Where(task => task != null)
                            .OrderByDescending(task => task.HasEndDate).ThenBy(task => task.Date.Date + task.Time).ThenBy(task => task.Title);
        }

        public Task CopyProperties(Task Task)
        {
            Filename = Task.Filename;
            Id = Task.Id;
            Title = Task.Title;
            HasEndDate = Task.HasEndDate;
            Date = Task.Date;
            Time = Task.Time;
            Notificationtype = Task.Notificationtype;
            IsNotificationTimeSet = Task.IsNotificationTimeSet;
            Notificationtime = Task.Notificationtime;
            return this;
        }
    }
}
