using System;
using System.Threading.Tasks;
using Firestorm.Rest.Web;
using Firestorm.Rest.Web.Options;
using Firestorm.Endpoints.Pagination;
using Firestorm.Endpoints.Strategies;
using Firestorm.Host.Infrastructure;

namespace Firestorm.Endpoints
{
    /// <summary>
    /// REST endpoint to handle <see cref="IRestCollection"/> implementations.
    /// </summary>
    internal class RestCollectionEndpoint : IRestEndpoint
    {
        internal RestCollectionEndpoint(IEndpointContext endpointContext, IRestCollection collection)
        {
            Context = endpointContext;
            Collection = collection;
        }

        public IEndpointContext Context { get; }

        public IRestCollection Collection { get; }

        public IRestEndpoint Next(INextPath path)
        {
            string dictionaryPrefix = Context.Configuration.QueryStringConfiguration.DictionaryReferencePrefix;
            if (path.Raw.StartsWith(dictionaryPrefix))
            {
                string identifierName = path.GetCoded(dictionaryPrefix.Length);
                IRestDictionary dictionary = Collection.ToDictionary(identifierName);
                return new RestDictionaryEndpoint(Context, dictionary);
            }
            else
            {
                // TODO split by = char? see https://stackoverflow.com/a/20386425/369247
                IRestItem item = Collection.GetItem(path.Raw);
                return new RestItemEndpoint(Context, item);
            }
        }

        public async Task<ResourceBody> GetAsync(IRestCollectionQuery query)
        {
            QueryValidationUtility.EnsureValidQuery(query);

            RestCollectionData collectionData = await Collection.QueryDataAsync(query);

            var linkCalculator = new PageLinkCalculator(Context.Configuration.ResponseConfiguration.PageConfiguration, query?.PageInstruction, collectionData.PageDetails);
            PageLinks pageLinks = linkCalculator.Calculate();

            return new CollectionBody(collectionData.Items, pageLinks);
        }

        public Task<Options> OptionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Feedback> UnsafeAsync(UnsafeMethod method, ResourceBody body)
        {
            IUnsafeRequestStrategy<IRestCollection> strategy = Context.Configuration.RequestStrategies.ForCollections.GetOrThrow(method);
            return strategy.ExecuteAsync(Collection, Context, body);
        }

        public bool EvaluatePreconditions(IPreconditions preconditions)
        {
            return true;
            // TODO: implement precondition checks
        }
    }
}