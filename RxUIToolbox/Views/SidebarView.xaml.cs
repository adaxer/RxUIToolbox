using ReactiveUI;
using RxUIToolboxViewModels;

namespace RxUIToolboxViews;

/// <summary>
/// Interaction logic for SidebarView.xaml
/// </summary>
public partial class SidebarView
{
    public SidebarView()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            DataContext = ViewModel;
        });

    }
}
