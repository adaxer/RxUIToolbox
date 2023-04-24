using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RxUIToolbox.Models;
using RxUIToolbox.Services;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using Splat;
using System.Reactive.Linq;

namespace RxUIToolbox.ViewModels;

public class ToolListViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IToolsService toolsService;
    private CancellationTokenSource? cts;
    private ILogger logger;

    public ToolListViewModel(IToolsService toolsService, IFactory<ILogger> loggerFactory)
    {
        this.toolsService = toolsService;
        logger = loggerFactory.Create() ?? throw new ArgumentNullException("No ILogger seems to be defined");
        LoadToolsCommand = ReactiveCommand.CreateFromTask<int>(LoadTools);
        LoadToolsCommand.ThrownExceptions.Subscribe(e => logger.Write(e, e.Message, typeof(ToolListViewModel),LogLevel.Error));
        SelectCommand = ReactiveCommand.Create(SelectTool);
        CancelLoadCommand = ReactiveCommand.Create(() => cts?.Cancel(), LoadToolsCommand.IsExecuting);
        ClearListCommand = ReactiveCommand.Create(ClearList, this.WhenAnyValue(x => x.Tools).Select(c => c?.Count > 0));
    }
    public ReactiveCommand<int, Unit> LoadToolsCommand { get; }
    public ReactiveCommand<Unit, Unit> SelectCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelLoadCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearListCommand { get; }

    public Interaction<string, bool> ConfirmClear { get; } = new Interaction<string, bool>();

    [Reactive]
    public ICollection<Tool>? Tools { get; private set; }
    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    [Reactive]
    public Tool? CurrentTool { get; set; }

    private async Task LoadTools(int delayTime)
    {
        Tools = new List<Tool>();
        logger.Write("Loading tools",LogLevel.Debug);
        cts = new CancellationTokenSource();
        Tools = (await toolsService.LoadTools(delayTime, cts.Token)).ToList();
        logger.Write("Loaded tools", LogLevel.Debug);
    }

    private void SelectTool()
    {
        if (CurrentTool != null)
        {
            toolsService.PostCurrentTool.OnNext(CurrentTool);
        }
    }

    private async void ClearList()
    {
        var canClear = await ConfirmClear.Handle("Are you sure you want to clear the list?");

        if (canClear)
        {
            Tools = null;
            logger.Write("Info cleared", LogLevel.Debug);
        }
        else
        {
            logger.Write("Clearing info denied by user",LogLevel.Debug);
        }
    }
}
