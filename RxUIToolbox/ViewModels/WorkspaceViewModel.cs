using ReactiveUI;

namespace RxUIToolbox.ViewModels;

public class WorkspaceViewModel : ReactiveObject, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    public string Title => "Workspace als ViewModel";
}
