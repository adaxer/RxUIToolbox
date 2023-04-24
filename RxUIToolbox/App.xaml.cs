using ReactiveUI;
using RxUIToolbox.ViewModels;
using Splat;
using System.Reflection;
using System.Windows;

namespace RxUIToolbox;

public partial class App : Application
{
    public App()
    {
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

        Locator.CurrentMutable.RegisterLazySingleton<MainViewModel>(()=>new MainViewModel());
        Locator.CurrentMutable.RegisterLazySingleton<ShellViewModel>(()=>new ShellViewModel());
        Locator.CurrentMutable.RegisterLazySingleton<ToolListViewModel>(()=>new ToolListViewModel());
        Locator.CurrentMutable.RegisterLazySingleton<WorkspaceViewModel>(()=>new WorkspaceViewModel());
    }
}
