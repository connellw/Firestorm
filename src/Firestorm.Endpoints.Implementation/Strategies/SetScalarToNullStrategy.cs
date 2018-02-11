using System.Threading.Tasks;
using Firestorm.Core;
using Firestorm.Core.Web;

namespace Firestorm.Endpoints.Strategies
{
    /// <summary>
    /// Sets a scalar value to null. Good strategy to use for a DELETE request.
    /// </summary>
    internal class SetScalarToNullStrategy : IUnsafeRequestStrategy<IRestScalar>
    {
        public async Task<Feedback> ExecuteAsync(IRestScalar scalar, IRestEndpointContext context, ResourceBody body)
        {
            Acknowledgment acknowledgment = await scalar.EditAsync(null);
            return new AcknowledgmentFeedback(acknowledgment);
        }
    }
}