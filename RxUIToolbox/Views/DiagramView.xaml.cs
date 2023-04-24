using ReactiveUI;
using System.Diagnostics;
using System.Reactive.Disposables;

namespace RxUIToolboxViews;

public partial class DiagramView
{
    public DiagramView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, x => x.PlotModel, x => x.plotView.Model)
                .DisposeWith(disposables);
        });
    }
}
