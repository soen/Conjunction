using System;
using Conjunction.Core.Infrastructure;
using Conjunction.Core.Infrastructure.Logging.Logging;
using Conjunction.Core.Model.Services;

namespace Conjunction.Core
{
  public static class Locator
  {
	  private static readonly IMutableDependencyResolver MutableDependencyResolver;

	  static Locator()
    {
	    MutableDependencyResolver = new MutableDependencyResolver();
	    Initialize();
    }

		public static IDependencyResolver Current => MutableDependencyResolver;
		
		private static Action<IMutableDependencyResolver> _dependencyRegistrar;
	  public static Action<IMutableDependencyResolver> DependencyRegistrar
	  {
		  get { return _dependencyRegistrar; }
		  set
		  {
			  _dependencyRegistrar = value;
				Initialize();
		  }
	  }

	  private static void Initialize()
    {
			MutableDependencyResolver.Register(LogProvider.GetCurrentClassLogger);
			MutableDependencyResolver.Register<ISearchQueryValueConversionService>(() => new SearchQueryValueConversionService());

	    DependencyRegistrar?.Invoke(MutableDependencyResolver);
    }
  }
}