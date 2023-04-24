using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RxUIToolbox.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System;
using Splat;
using System.Linq;

namespace RxUIToolbox.ViewModels;

public class WorkspaceViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly ILogger logger;

    public WorkspaceViewModel(ILogger logger)
    {
        Activator = new ViewModelActivator();
        this.WhenActivated((CompositeDisposable d) =>
        {
            logger.Write("WorkspaceViewModel activated", LogLevel.Debug);
        });
        this.logger = logger;
        MessageBus.Current.Listen<MovingTool>().Subscribe(SetTool);
    }

    private void SetTool(MovingTool newTool)
    {
        if (!Tools.Any(t => t.Tool?.Name == newTool.Tool?.Name))
        {
            Tools.Add(newTool);
        }
        var tool = Tools.Single(t => t.Tool?.Name == newTool.Tool?.Name);
        tool.X = newTool.X;
        tool.Y = newTool.Y;
    }

    public ViewModelActivator Activator { get; }

    [Reactive]
    public string Title { get; set; } = "A WorkspaceViewModel";

    public ICollection<MovingTool> Tools { get; } = new ObservableCollection<MovingTool>();
}
