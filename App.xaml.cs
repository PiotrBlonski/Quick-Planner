using Quick_Planner.Models;
using Task = System.Threading.Tasks.Task;

namespace Quick_Planner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            LoadData();

            MainPage = new AppShell();

        }

        static async void LoadData()
        {
            await Task.Run(() =>
            {
                ThemeManager.LoadDefaultPalettes();
                Settings.Load();
                ThemeManager.UpdateTheme();
                TaskManager.LoadTasks();
                BoardManager.LoadBoards();
            });
        }
    }
}