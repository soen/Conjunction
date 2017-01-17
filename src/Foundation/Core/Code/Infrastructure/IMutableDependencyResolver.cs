using System;

namespace Conjunction.Foundation.Core.Infrastructure
{
  internal interface IMutableDependencyResolver : IDependencyResolver
  {
    void Register<TService>(Func<TService> instanceFactory);
    void Register<TService, TImpl>() where TImpl : TService;
    void RegisterSingleton<TService>(TService instance);
    void RegisterSingleton<TService>(Func<TService> instanceFactory);
  }
}
