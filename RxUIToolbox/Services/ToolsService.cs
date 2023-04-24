using RxUIToolbox.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RxUIToolbox.Services;

public class ToolsService : IToolsService
{
    public IEnumerable<Tool> LoadTools()
    {
        var result = Directory
            .GetFiles(Path.Combine(Environment.CurrentDirectory, "Assets"), "*.svg")
            .Select(f => new Tool { Name = Path.GetFileNameWithoutExtension(f), FileName = f });
        foreach (var tool in result) 
        {
            yield return tool;
        }
    }
}
