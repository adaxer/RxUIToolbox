using ReactiveUI;
using System;
using System.Reactive;

namespace RxUIToolboxViewModels;

public class SideOneViewModel : ReactiveObject, IRoutableViewModel
{
    public SideOneViewModel(IScreen screen)
    {
        HostScreen = screen;
        ShowDiagramCommand = ReactiveCommand.Create(ShowDiagram);
    }

    private void ShowDiagram()
    {
        HostScreen.Router.Navigate.Execute(new DiagramViewModel(HostScreen)).Subscribe();
    }

    public string? UrlPathSegment => "SideOne";

    public IScreen HostScreen { get; }

    public ReactiveCommand<Unit,Unit> ShowDiagramCommand { get; }
}
