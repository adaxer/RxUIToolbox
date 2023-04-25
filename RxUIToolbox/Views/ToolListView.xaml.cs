using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
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
            this.BindCommand(ViewModel, vm => vm.ClearListCommand, v => v.clearList);
            this.BindInteraction(ViewModel, vm => vm.ConfirmClear, OnConfirmClearTask).DisposeWith(d);

            //ViewModel!
            // .ConfirmClear
            // .RegisterHandler(OnConfirmClear)
            // .DisposeWith(d);
        });
    }

    private void OnConfirmClear(InteractionContext<string, bool> context)
    {
        var deleteIt = MessageBox.Show(context.Input, "Question", MessageBoxButton.YesNo);
        context.SetOutput(deleteIt == MessageBoxResult.Yes);
    }

    private Task OnConfirmClearTask(InteractionContext<string, bool> context)
    {
        var deleteIt = MessageBox.Show(context.Input, "Question", MessageBoxButton.YesNo);
        context.SetOutput(deleteIt == MessageBoxResult.Yes);
        return Task.CompletedTask;
    }
}
