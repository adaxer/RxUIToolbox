using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

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

            ViewModel!
             .ConfirmClear
             .RegisterHandler(interaction =>
             {
                 var deleteIt = MessageBox.Show(interaction.Input, "Question", MessageBoxButton.YesNo);
                 interaction.SetOutput(deleteIt == MessageBoxResult.Yes);
             })
             .DisposeWith(d);
        });
    }
}
