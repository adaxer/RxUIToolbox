using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using ReactiveUI;
using System.Collections.Generic;

namespace RxUIToolboxViewModels;

public class DiagramViewModel : ReactiveObject, IRoutableViewModel
{
    public DiagramViewModel(IScreen screen)
    {
        HostScreen = screen;

        PlotModel = new PlotModel();

        var line1 = new LineSeries() { Title = "line1" };
        var line2 = new LineSeries() { Title = "line2" };
        var line3 = new LineSeries() { Title = "line3" };

        for (int i = 0; i < 10; i++)
        {
            line1.Points.Add(new DataPoint(i, i));
            line2.Points.Add(new DataPoint(i, i + 1));
            line3.Points.Add(new DataPoint(i, i + 2));
        }

        PlotModel.Series.Add(line1);
        PlotModel.Series.Add(line2);
        PlotModel.Series.Add(line3);
        PlotModel.Legends.Add(new Legend()
        {
            LegendPosition = LegendPosition.TopCenter,
            LegendFontSize = 15
        });

        PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
    }


    public string? UrlPathSegment => "Oxyplot Example";

    public IScreen HostScreen { get; }
    public PlotModel PlotModel { get; }
}
