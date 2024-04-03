using CommunityToolkit.Maui;
using LocalizationResourceManager.Maui;
using Microsoft.Extensions.Logging;
using Quick_Planner.Pages.Alarm;
using Quick_Planner.Pages.Projects;
using Quick_Planner.Pages.Settings;
using Quick_Planner.Pages.Tasks;
using Quick_Planner.Resources;
using Quick_Planner.ViewModels;

namespace Quick_Planner
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseLocalizationResourceManager(settings =>
                {
                    settings.RestoreLatestCulture(true);
                    settings.AddResource(AppResources.ResourceManager);
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("LilitaOne-Regular.ttf", "LilitaOne");
                });
            builder.Services.AddSingleton<TasksListPage>();
            builder.Services.AddSingleton<TasksViewModel>();
            builder.Services.AddSingleton<ProjectsListPage>();
            builder.Services.AddSingleton<ProjectsListViewModel>();
            builder.Services.AddTransient<BoardView>();
            builder.Services.AddTransient<BoardViewModel>();
            builder.Services.AddTransient<EditBoardPage>();
            builder.Services.AddTransient<EditListPage>();
            builder.Services.AddTransient<EditCardPage>();
            builder.Services.AddTransient<EditBoardViewModel>();
            builder.Services.AddTransient<EditListViewModel>();
            builder.Services.AddTransient<EditCardViewModel>();
            builder.Services.AddTransient<TaskAddPage>();
            builder.Services.AddTransient<TaskAddViewModel>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddTransient<AlarmPage>();
            builder.Services.AddTransient<AlarmViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            Handlers.RemoveBorders();
            return builder.Build();
        }
    }
}