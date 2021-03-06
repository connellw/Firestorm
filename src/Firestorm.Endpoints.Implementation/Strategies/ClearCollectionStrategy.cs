using System.Threading.Tasks;
using Firestorm.Endpoints.Requests;
using Firestorm.Rest.Web;

namespace Firestorm.Endpoints.Strategies
{
    public class ClearCollectionStrategy : ICommandStrategy<IRestCollection>
    {
        public async Task<Feedback>  ExecuteAsync(IRestCollection collection, IEndpointContext context, ResourceBody body)
        {
            var ack = await collection.DeleteAllAsync(null); // TODO query
            return new AcknowledgmentFeedback(ack);
        }
    }
}