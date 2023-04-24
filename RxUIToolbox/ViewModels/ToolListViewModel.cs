using ReactiveUI;

namespace RxUIToolbox.ViewModels;

public class ToolListViewModel : ReactiveObject, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    public string Title => "Tool-List als ViewModel";
}
