using CommunityToolkit.Maui.Core.Extensions;
using Quick_Planner.Models;
using System.Collections.ObjectModel;
using Task = System.Threading.Tasks.Task;

namespace Quick_Planner
{
    public static class BoardManager
    {
        public static ObservableCollection<Board> Boards;

        public static int CurrentBoardIndex = -1;

        public static int CurrentListIndex = -1;
        public static void LoadBoards() => Boards = Board.LoadAll().ToObservableCollection();

        public static Board CurrentBoard => Boards == null ? null : Boards[CurrentBoardIndex];

        public static bool StopTask = false;

        public static Task AutoSaveTask => new(() =>
        {
            while(true)
            {
                Thread.Sleep(2000);
                CurrentBoard?.Save();

                if (StopTask)
                {
                    StopTask = false;
                    break;
                }
            }
        });
    }
}
