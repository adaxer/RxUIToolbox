using Splat;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace RxUIToolbox.Services;

public class TraceLogger : ILogger
{
    public LogLevel Level => LogLevel.Debug;

    public void Write([Localizable(false)] string message, LogLevel logLevel)
    {
        Trace.WriteLine(Format(message, logLevel));
    }

    private string? Format(string message, LogLevel logLevel) => $"[{logLevel}] ({DateTime.Now.ToString("HH:mm:ss,fff")}) : => {message}";

    public void Write(Exception exception, [Localizable(false)] string message, LogLevel logLevel)
    {
        Trace.TraceError($"{Format(message, logLevel)}\n{exception}");
    }

    public void Write([Localizable(false)] string message, [Localizable(false)] Type type, LogLevel logLevel)
    {
        Trace.TraceError($"[{type}:{Format(message, logLevel)}");
    }

    public void Write(Exception exception, [Localizable(false)] string message, [Localizable(false)] Type type, LogLevel logLevel)
    {
        Trace.TraceError($"[{type}:{Format(message, logLevel)}\n{exception}");
    }
}
