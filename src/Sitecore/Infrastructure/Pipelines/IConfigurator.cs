using Sitecore.Pipelines;

namespace Conjunction.Sitecore.Infrastructure.Pipelines
{
	public interface IConfigurator
	{
		void Process(PipelineArgs args);
	}
}