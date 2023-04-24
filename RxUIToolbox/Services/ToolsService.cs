using ReactiveUI;
using RxUIToolbox.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Threading;

namespace RxUIToolbox.Services;

public class ToolsService : IToolsService
{
    public ToolsService()
    {
        currentTool
            .CombineLatest(positions)
            .Select(o => new MovingTool { Tool = o.First, X = o.Second.X, Y = o.Second.Y })
            .Subscribe(mt => MessageBus.Current.SendMessage(mt));

        //positions.Subscribe(p => Trace.WriteLine(p.X.ToString()));
        //currentTool.Subscribe(t=>Trace.WriteLine(t.ToString()));
    }

    public IObserver<Tool> PostCurrentTool => currentTool;

    public async Task<IEnumerable<Tool>?> LoadTools(int delayTime, CancellationToken ct)
    {
        for (int i = 0; i < delayTime / 100; i++)
        {
            ct.ThrowIfCancellationRequested();
            await Task.Delay(100);
        }

        var result = Directory
            .GetFiles(Path.Combine(Environment.CurrentDirectory, "Assets"), "*.svg")
            .Select(f => new Tool { Name = Path.GetFileNameWithoutExtension(f), FileName = f })
            .ToList() as IEnumerable<Tool>;
        return result!;
    }

    private IObservable<(double X, double Y)> positions = Observable
        .Generate<(double X, double Y), (double X, double Y)>((0.5, 0.5), v => true, v => (Move(v.X), Move(v.Y)), i => i, i => TimeSpan.FromMilliseconds(50));

    static Random rnd = new Random(Guid.NewGuid().GetHashCode());
    private ReplaySubject<Tool> currentTool = new ReplaySubject<Tool>();

    static double Move(double x)
    {
        var result = x + 0.05 * (rnd.NextDouble() - 0.5);
        return (result < 0.0)
            ? 1.0 - result
            : (result > 1.0)
                ? result - 1.0
                : result;
    }
}
