using ReactiveUI;
using RxUIToolbox;
using System;
using System.Reactive;

namespace RxUIToolboxViewModels;

public class SideOneViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly IFactory<DiagramViewModel> diagramFactory;

    public SideOneViewModel(IScreen screen, IFactory<DiagramViewModel> diagramFactory)
    {
        HostScreen = screen;
        this.diagramFactory = diagramFactory;
        ShowDiagramCommand = ReactiveCommand.Create(ShowDiagram);
    }

    private void ShowDiagram()
    {
        HostScreen.Router.Navigate.Execute(diagramFactory.Create()!).Subscribe();
    }

    public string? UrlPathSegment => "SideOne";

    public IScreen HostScreen { get; }

    public ReactiveCommand<Unit,Unit> ShowDiagramCommand { get; }
}
