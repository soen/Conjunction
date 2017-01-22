using System;
using System.Collections.Generic;
using System.Linq;

namespace Conjunction.Foundation.Core.Infrastructure
{
  internal sealed class MutableDependencyResolver : IMutableDependencyResolver
  {
    private readonly Dictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();

    public void Register<TService, TImpl>() where TImpl : TService
    {
      _registrations.Add(typeof(TService), () => GetInstance(typeof(TImpl)));
    }

    public void Register<TService>(Func<TService> instanceFactory)
    {
      _registrations.Add(typeof(TService), () => instanceFactory());
    }

    public void RegisterSingleton<TService>(TService instance)
    {
      _registrations.Add(typeof(TService), () => instance);
    }

    public void RegisterSingleton<TService>(Func<TService> instanceFactory)
    {
      var lazy = new Lazy<TService>(instanceFactory);
      Register(() => lazy.Value);
    }

    public object GetInstance(Type serviceType)
    {
      Func<object> factory;
      if (_registrations.TryGetValue(serviceType, out factory))
        return factory();

      if (!serviceType.IsAbstract)
        return CreateInstance(serviceType);

      throw new InvalidOperationException($"No registration for: {serviceType}");
    }

    public TService GetInstance<TService>()
    {
      return (TService) GetInstance(typeof(TService));
    }

    private object CreateInstance(Type implementationType)
    {
      var ctor = implementationType.GetConstructors().Single();
      var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType);
      var dependencies = parameterTypes.Select(GetInstance).ToArray();

      return Activator.CreateInstance(implementationType, dependencies);
    }
  }
}