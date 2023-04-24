using RxUIToolbox.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace RxUIToolbox.Services;

public interface IToolsService
{
    Task<IEnumerable<Tool>> LoadTools(int delayMs, CancellationToken ct);

    IObserver<Tool> PostCurrentTool { get; }
}