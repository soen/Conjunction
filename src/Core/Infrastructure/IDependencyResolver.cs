using System;

namespace Conjunction.Core.Infrastructure
{
  public interface IDependencyResolver
  {
    object GetInstance(Type serviceType);
    TService GetInstance<TService>();
  }
}