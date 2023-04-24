using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace RxUIToolbox.Models;

public class MovingTool : ReactiveObject
{
    public Tool? Tool { get; set; }
    [Reactive]
    public double X { get; set; }
    [Reactive]
    public double Y { get; set; }
}
