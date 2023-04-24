using ReactiveUI;
using RxUIToolbox.ViewModels;
using Splat;
using System;

namespace RxUIToolbox.Views;

public partial class ShellView
{
    public ShellView()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
            DataContext = ViewModel = Locator.Current.GetService<ShellViewModel>() ?? throw new ArgumentNullException("ShellViewModel not registered");

            // this.OneWayBind(ViewModel, vm => vm.Main, v => v.mainHost.ViewModel);
        });
    }
}
