using ReactiveUI;

namespace RxUIToolbox.Views;

public partial class ToolListView
{
    public ToolListView()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
            DataContext = ViewModel;
        });
    }
}
