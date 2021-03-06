using System.Collections.Generic;
using System.Threading.Tasks;
using Firestorm.Endpoints.Requests;
using Firestorm.Rest.Web;

namespace Firestorm.Endpoints.Strategies
{
    public class AddToCollectionStrategy : ICommandStrategy<IRestCollection>
    {
        public async Task<Feedback>  ExecuteAsync(IRestCollection collection, IEndpointContext context, ResourceBody body)
        {
            var collectionBody = body as CollectionBody;
            if (collectionBody != null)
            {
                var feedbackItems = new List<Feedback>();

                foreach (RestItemData itemData in collectionBody.Items)
                {
                    CreatedItemAcknowledgment acknowledgment = await collection.AddAsync(itemData);
                    feedbackItems.Add(new AcknowledgmentFeedback(acknowledgment));
                    // TODO: catch exceptions
                }

                return new MultiFeedback(feedbackItems);
            }

            var itemBody = body as ItemBody;
            if (itemBody != null)
            {
                CreatedItemAcknowledgment acknowledgment = await collection.AddAsync(itemBody.Item);
                return new AcknowledgmentFeedback(acknowledgment);
            }

            throw new ItemBodyNotSupportedException(body.ResourceType);
        }
    }
}