using ReactiveUI;
using RxUIToolboxViewModels;
using System.Reactive.Disposables;

namespace RxUIToolboxViews;

public partial class SideOneView 
{
    public SideOneView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, x => x.UrlPathSegment, x => x.PathTextBlock.Text)
                .DisposeWith(disposables);

            this.BindCommand(ViewModel, vm => vm.ShowDiagramCommand, v => v.showDiagram);
        });
    }
}
