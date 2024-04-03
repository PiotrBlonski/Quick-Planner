using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Quick_Planner.Models;
using Quick_Planner.Pages.Projects;
using Quick_Planner.Resources;
using System.Collections.ObjectModel;

namespace Quick_Planner.ViewModels
{
    public partial class ProjectsListViewModel : ObservableObject
    {
        public ProjectsListViewModel()
        {
            Boards = BoardManager.Boards;
        }

        [ObservableProperty]
        ObservableCollection<Board> boards;

        [ObservableProperty]
        Board currentBoard;

        [ObservableProperty]
        bool isWindowOpen = false;

        [RelayCommand]
        public void Delete()
        {
            CurrentBoard.Delete();
            Boards.Remove(CurrentBoard);
            HideWindow();
        }

        [RelayCommand]
        public void RequestDelete(Board Board)
        {
            IsWindowOpen = true;
            CurrentBoard = Board;
        }

        [RelayCommand]
        public void HideWindow()
        {
            IsWindowOpen = false;
        }

        [RelayCommand]
        private void Add()
        {
            Update(new Board() { Name = AppResources.New_Board, Cards = new(), Lists = new(), Labels = new() }, false);
        }

        [RelayCommand]
        public void Edit(Board Board)
        {
            Update(Board, true);
        }

        async void Update(Board Board, bool Editing)
        {
            if(!Editing)
                BoardManager.Boards.Add(Board);

            BoardManager.CurrentBoardIndex = BoardManager.Boards.IndexOf(Board);

            BoardManager.StopTask = false;
            if (!BoardManager.AutoSaveTask.Status.Equals(TaskStatus.Running))
                BoardManager.AutoSaveTask.Start();

            await Shell.Current.GoToAsync($"{nameof(BoardView)}",
                new Dictionary<string, object> { { "Board", Board }
                });
        }
    }
}
