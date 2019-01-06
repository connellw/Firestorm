using System.Threading.Tasks;
using Firestorm.Rest.Web;

namespace Firestorm.Endpoints.Strategies
{
    internal class DeleteItemStrategy : IUnsafeRequestStrategy<IRestItem>
    {
        public async Task<Feedback> ExecuteAsync(IRestItem item, IEndpointContext context, ResourceBody body)
        {
            Acknowledgment acknowledgment = await item.DeleteAsync();
            return new AcknowledgmentFeedback(acknowledgment);
        }
    }
}