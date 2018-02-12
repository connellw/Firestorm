using System.Threading.Tasks;
using Firestorm.Core.Web;
using Firestorm.Core.Web.Options;
using Firestorm.Endpoints.Preconditions;
using JetBrains.Annotations;

namespace Firestorm.Endpoints
{
    /// <summary>
    /// The endpoint for a resource. Usually located by a URL.
    /// </summary>
    public interface IRestEndpoint
    {
        /// <summary>
        /// Drills down into the next layer of the URL path.
        /// </summary>
        IRestEndpoint Next(string path);

        /// <summary>
        /// Performs a GET request.
        /// </summary>
        Task<ResourceBody> GetAsync(IRestCollectionQuery query);

        /// <summary>
        /// Performs an OPTIONS request.
        /// </summary>
        Task<Options> OptionsAsync();

        /// <summary>
        /// Performs an unsafe request i.e. changes the state of the resource on the server.
        /// Request resource body is provided.
        /// </summary>
        Task<Feedback> UnsafeAsync(UnsafeMethod method, [CanBeNull] ResourceBody body);

        /// <summary>
        /// Returns true if the preconditions for this endpoint are fulfilled.
        /// This should result in a 412 status code for unsafe requests or a 304 for a GET request.
        /// </summary>
        bool EvaluatePreconditions(IPreconditions preconditions);
    }
}