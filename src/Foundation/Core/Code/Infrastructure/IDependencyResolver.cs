using System;

namespace Conjunction.Foundation.Core.Infrastructure
{
  public interface IDependencyResolver
  {
    object GetInstance(Type serviceType);
    TService GetInstance<TService>();
  }
}