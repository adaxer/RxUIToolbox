using ReactiveUI;
using System;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Disposables;
using Splat;

namespace RxUIToolbox.ViewModels;

public class ShellViewModel : ReactiveObject, IActivatableViewModel
{
    public ShellViewModel()
    {
        Main = Locator.Current.GetService<MainViewModel>() ?? throw new ArgumentNullException("MainViewModel not registered");

        this.WhenActivated((CompositeDisposable d) =>
        {
            this
            .WhenAnyValue(vm => vm.Main.Title)
            .WhereNotNull()
            .Subscribe(s => Title = s);
        });
    }

    public MainViewModel Main { get; }

    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    [Reactive]
    public string? Title { get; set; }
}
