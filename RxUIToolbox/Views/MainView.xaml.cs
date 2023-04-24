using ReactiveUI;
using RxUIToolbox.ViewModels;
using Splat;
using System.Reactive.Disposables;
using System.Windows;

namespace RxUIToolbox.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView 
{
    public MainView()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
            DataContext = ViewModel = ((Parent as ViewModelViewHost)?.ViewModel as MainViewModel) ?? Locator.Current.GetService<MainViewModel>();

            //this.OneWayBind(ViewModel, vm => vm.Tools, v => v.toolList.ViewModel);
        });
    }
}
