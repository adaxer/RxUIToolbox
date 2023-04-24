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
            this.BindCommand(ViewModel, vm => vm.SelectCommand, v => v.list);
        });
    }
}
