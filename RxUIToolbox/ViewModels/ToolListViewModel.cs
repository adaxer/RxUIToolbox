using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RxUIToolbox.Models;
using RxUIToolbox.Services;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace RxUIToolbox.ViewModels;

public class ToolListViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IToolsService toolsService;

    public ToolListViewModel(IToolsService toolsService)
    {
        this.toolsService = toolsService;
        LoadToolsCommand = ReactiveCommand.Create(LoadTools);
    }
    public ReactiveCommand<Unit, Unit> LoadToolsCommand { get; }

    [Reactive]
    public ICollection<Tool>? Tools { get; private set; }
    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    [Reactive]
    public Tool? CurrentTool { get; set; }
    private void LoadTools()
    {
        Tools = new List<Tool>();
        Tools = toolsService.LoadTools().ToList();
    }
}
