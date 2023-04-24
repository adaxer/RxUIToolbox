using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace RxUIToolbox.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    public MainViewModel()
    {
        Tools = Locator.Current.GetService<ToolListViewModel>() ?? throw new ArgumentNullException("ToolsViewModel not registered");
        Workspace = Locator.Current.GetService<WorkspaceViewModel>() ?? throw new ArgumentNullException("WorkspaceViewModel not registered");

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

    public ToolListViewModel Tools { get; set; }
    public WorkspaceViewModel Workspace { get; set; }


    [Reactive]
    public string Title { get; set; } = "RxUIToolbox";
    
}
