using RxUIToolbox.Models;
using System.Collections.Generic;

namespace RxUIToolbox.Services;

public interface IToolsService
{
    IEnumerable<Tool> LoadTools();
}