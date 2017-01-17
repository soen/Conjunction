using System;

namespace Conjunction.Foundation.Core.Infrastructure
{
  internal interface IDependencyResolver
  {
    object GetInstance(Type serviceType);
    TService GetInstance<TService>();
  }
}