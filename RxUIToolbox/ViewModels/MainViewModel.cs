using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RxUIToolboxViewModels;
using Splat;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace RxUIToolbox.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IFactory<ILogger> loggerFactory;
    private ILogger? logger;

    public MainViewModel(ToolListViewModel tools, WorkspaceViewModel workspace, SidebarViewModel sidebar, IFactory<ILogger> loggerFactory)
    {
        Tools = tools;
        Workspace = workspace;
        Sidebar = sidebar;
        this.loggerFactory = loggerFactory;
        this.WhenActivated((CompositeDisposable d) =>
        {
            HandleActivation();

            Disposable.Create(() => HandleDeactivation()).DisposeWith(d);
        });

    }

    private void HandleDeactivation()
    {
        Title="Deactivated";
    }

    private void HandleActivation()
    {
        Logger.Write("Activating", LogLevel.Debug);
        UpdateTitle();
    }

    private async void UpdateTitle()
    {
        await Task.Delay(1000);
        while(true)
        {
            Title =  $"RxUIToolbox - Application ({DateTime.Now.ToString("HH:mm:ss")})";
            await Task.Delay(1000);
        }
    }

    public ViewModelActivator Activator { get; }= new ViewModelActivator();

    public ToolListViewModel Tools { get; }
    public WorkspaceViewModel Workspace { get; }
    public SidebarViewModel Sidebar { get; }

    private ILogger Logger => logger ??= (loggerFactory.Create() ?? throw new ArgumentNullException("No ILogger seems to be defined"));


    [Reactive]
    public string Title { get; set; } = "RxUIToolbox";
    
}
