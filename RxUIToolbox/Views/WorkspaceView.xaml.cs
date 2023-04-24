using ReactiveUI;

namespace RxUIToolbox.Views;

public partial class WorkspaceView
{
    public WorkspaceView()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
            DataContext = ViewModel;
        });
    }
}
