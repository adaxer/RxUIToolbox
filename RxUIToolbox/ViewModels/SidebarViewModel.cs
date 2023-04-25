using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RxUIToolbox;
using Splat;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace RxUIToolboxViewModels;

public class SidebarViewModel : ReactiveObject, IScreen
{
    private IRoutableViewModel? forwardViewModel;
    private readonly IFactory<SideOneViewModel> sideOneFactory;

    public SidebarViewModel(IFactory<SideOneViewModel> sideOneFactory)
    {
        this.sideOneFactory = sideOneFactory;
        Router = new RoutingState();
        Router.CurrentViewModel.Subscribe(vm => CurrentViewModel = vm);

        var canGoBack = this
            .WhenAnyValue(x => x.Router.NavigationStack.Count)
            //.Do(c => Trace.WriteLine($"{c} - {Router.NavigationStack.LastOrDefault()?.GetType()?.Name}"))
            .Select(c => c > 2);

        BackCommand = ReactiveCommand.CreateFromObservable(
            GoBack,
            canGoBack);

        GoFirst();

        var canGoForward = this
            .WhenAnyValue(x => x.Router.NavigationStack.Count)
            //.Do(c => Trace.WriteLine($"{c} - {Router.NavigationStack.LastOrDefault()?.GetType()?.Name}"))
            .Select(c => c <3 && forwardViewModel!=null);

        ForwardCommand = ReactiveCommand.CreateFromObservable(
            GoForward,
            canGoForward);

        GoFirst();
    }

    private IObservable<IRoutableViewModel> GoForward()
    {
        var forwardTo = forwardViewModel;
        forwardViewModel = null;
        return Router.Navigate.Execute(forwardTo!);
    }

    private IObservable<IRoutableViewModel?> GoBack()
    {
        forwardViewModel = Router.NavigationStack.Last();
        return Router.NavigateBack.Execute(Unit.Default);
    }

    public RoutingState Router { get; }

    [Reactive]
    public IRoutableViewModel? CurrentViewModel { get; private set; }

    public ReactiveCommand<Unit, IRoutableViewModel?> BackCommand { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> ForwardCommand { get; }

    private async void GoFirst()
    {
        await Task.Delay(1000);
        Router.Navigate.Execute(sideOneFactory.Create()!).Subscribe();
    }
}
