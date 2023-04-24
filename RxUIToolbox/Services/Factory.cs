using Microsoft.Extensions.DependencyInjection;
using System;

namespace RxUIToolbox;


public interface IFactory<T>
{
    T? Create();
}

public class GenericFactory<T> : IFactory<T>
{
    private readonly IServiceProvider serviceProvider;

    public GenericFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
    public T? Create()
    {
        return serviceProvider.GetService<T>();
    }
}
