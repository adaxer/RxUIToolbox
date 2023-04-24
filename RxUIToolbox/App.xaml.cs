using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using RxUIToolbox.Services;
using RxUIToolbox.ViewModels;
using RxUIToolbox.Views;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace RxUIToolbox;

public partial class App : Application
{
    public App()
    {
        IHost host = Init();
        // Since MS DI container is a different type,
        // we need to re-register the built container with Splat again
        Container = host.Services ?? throw new ArgumentNullException("ServiceCollection is not initialized");
        Container.UseMicrosoftDependencyResolver();
    }

    public IServiceProvider Container { get; private set; }

    IHost Init()
    {
        var host = Host
          .CreateDefaultBuilder()
          .ConfigureServices(services =>
          {
              services.UseMicrosoftDependencyResolver();
              var resolver = Locator.CurrentMutable;
              resolver.InitializeSplat();
              resolver.InitializeReactiveUI();

              // Configure our local services and access the host configuration
              ConfigureServices(services);
          })
          .UseEnvironment(Environments.Development)
          .Build();

        return host;
    }

    void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient(typeof(IFactory<>), typeof(GenericFactory<>));
        services.AddTransient<ILogger, TraceLogger>();
        services.AddTransient<IToolsService, ToolsService>();

        services.AddSingleton<ShellViewModel>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<ToolListViewModel>();
        services.AddTransient<WorkspaceViewModel>();

        services.AddTransient<IViewFor<ShellViewModel>, ShellView>();
        services.AddTransient<IViewFor<MainViewModel>, MainView>();
        services.AddTransient<IViewFor<ToolListViewModel>, ToolListView>();
        services.AddTransient<IViewFor<WorkspaceViewModel>, WorkspaceView>();

    }
}
