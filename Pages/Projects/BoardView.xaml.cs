using Quick_Planner.ViewModels;
using System.Diagnostics;
using Task = System.Threading.Tasks.Task;

namespace Quick_Planner.Pages.Projects;

public partial class BoardView : ContentPage
{
    readonly BoardViewModel viewModel;
    public BoardView(BoardViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        viewModel = vm;
    }

    bool IsDragging;

    public CollectionView Collection;

    Task DragTask;

    private void DragStarting(object sender, DragStartingEventArgs e)
    {
        if (DragTask?.Status == TaskStatus.Running)
            return;
        StartDragTask();
    }

    private void DragOver(object sender, DragEventArgs e) => Collection = ((sender as Element).Parent as Border).Parent as CollectionView;

    private void DropCompleted(object sender, DropCompletedEventArgs e) => IsDragging = false;

    private void Drop(object sender, DropEventArgs e) => Collection.ScrollTo(viewModel.CurrentObject);

    private void StartDragTask()
    {
        IsDragging = true;
        DragTask = Task.Run(() =>
        {
            while (IsDragging)
            {
                object Before = viewModel.OnElement;

                Thread.Sleep(500);

                object After = viewModel.OnElement;

                if (Before == After && IsDragging)
                    try { Collection?.ScrollTo(Before, position: ScrollToPosition.Center); }
                    catch (Exception e) { Trace.WriteLine(e); }
            }
        });
    }
}