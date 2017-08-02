using System;
using Conjunction.Core.Model.Providers.Indexing;
using Conjunction.Core.Model.Providers.SearchQueryElement;
using Conjunction.Core.Model.Providers.SearchQueryValue;
using Conjunction.Core.Model.Repositories;
using Conjunction.Sitecore.Model.Processing;
using Conjunction.Sitecore.Model.Providers.Indexing;
using Conjunction.Sitecore.Model.Repositories;

namespace Conjunction.Sitecore.Model
{
	/// <summary>
	/// Provides functionalities to buld new <see cref="ISearchResultRepository{T}"/> instances using a specified configuration.
	/// </summary>
	/// <typeparam name="T">The type of <see cref="IndexableEntity"/> implementation to use.</typeparam>
	public class SearchResultRepositoryBuilder<T> where T : IndexableEntity, new()
	{
		private readonly Type _indexNameProviderType;
		private readonly Type _searchQueryPredicateBuilderType;

		public SearchResultRepositoryBuilder()
			: this(typeof(MasterOrWebIndexNameProvider), typeof(SearchQueryPredicateBuilder<T>))
		{
		}

		private SearchResultRepositoryBuilder(Type indexNameProviderType, Type searchQueryPredicateBuilderType)
		{
			if (indexNameProviderType == null)
				throw new ArgumentNullException(nameof(indexNameProviderType));
			if (searchQueryPredicateBuilderType == null)
				throw new ArgumentNullException(nameof(searchQueryPredicateBuilderType));

			_indexNameProviderType = indexNameProviderType;
			_searchQueryPredicateBuilderType = searchQueryPredicateBuilderType;
		}

		/// <summary>
		/// Configures the builder to use a given <see cref="IIndexNameProvider"/> type.
		/// </summary>
		/// <typeparam name="TIndexNameProvider">The <see cref="IIndexNameProvider"/> type to configure,</typeparam>
		/// <returns>A configured <see cref="SearchQueryPredicateBuilder{T}"/>.</returns>
		public SearchResultRepositoryBuilder<T> WithIndexNameProvider<TIndexNameProvider>()
			where TIndexNameProvider : IIndexNameProvider
		{
			return new SearchResultRepositoryBuilder<T>(typeof(TIndexNameProvider), _searchQueryPredicateBuilderType);
		}

		/// <summary>
		/// Configures the builder to use a given <see cref="ISearchQueryPredicateBuilder{T}"/> type.
		/// </summary>
		/// <typeparam name="TSearchQueryPredicateBuilder">The <see cref="TSearchQueryPredicateBuilder"/> type to configure,</typeparam>
		/// <returns>A configured <see cref="SearchQueryPredicateBuilder{T}"/>.</returns>
		public SearchResultRepositoryBuilder<T> WithPredicateBuilder<TSearchQueryPredicateBuilder>()
			where TSearchQueryPredicateBuilder : ISearchQueryPredicateBuilder<T>
		{
			return new SearchResultRepositoryBuilder<T>(_indexNameProviderType, typeof(TSearchQueryPredicateBuilder));
		}

		/// <summary>
		/// Creates a new <see cref="ISearchResultRepository{T}"/> instance using the specified builder configuration.
		/// </summary>
		/// <param name="searchQueryElementProvider">The specified element provider.</param>
		/// <param name="searchQueryValueProvider">The specified value provider.</param>
		/// <returns>A configured <see cref="ISearchResultRepository{T}"/> instance.</returns>
		public ISearchResultRepository<T> Create(ISearchQueryElementProvider searchQueryElementProvider,
																						 ISearchQueryValueProvider searchQueryValueProvider)
		{
			if (searchQueryElementProvider == null)
				throw new ArgumentNullException(nameof(searchQueryElementProvider));
			if (searchQueryValueProvider == null)
				throw new ArgumentNullException(nameof(searchQueryValueProvider));

			var indexNameProvider = GetIndexNameProvider();
			var predicateBuilder = GetSearchQueryPredicateBuilder(searchQueryValueProvider);

			return new SearchResultRepository<T>(searchQueryElementProvider, indexNameProvider, predicateBuilder);
		}

		private IIndexNameProvider GetIndexNameProvider()
		{
			var instance = Activator.CreateInstance(_indexNameProviderType);
			return (IIndexNameProvider) instance;
		}

		private ISearchQueryPredicateBuilder<T> GetSearchQueryPredicateBuilder(ISearchQueryValueProvider searchQueryValueProvider)
		{
			var instance = Activator.CreateInstance(_searchQueryPredicateBuilderType, searchQueryValueProvider);
			return (ISearchQueryPredicateBuilder<T>) instance;
		}
	}
}