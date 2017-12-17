using System.Collections.Generic;
using System.Net;
using Firestorm.Core.Web;
using Firestorm.Core.Web.Options;

namespace Firestorm.Endpoints.Responses
{
    public class MainBodyResponseBuilder : IResponseBuilder
    {
        public void AddResource(Response response, ResourceBody resourceBody)
        {
            object obj = resourceBody.GetObject();

            response.ResourceBody = obj;
            response.StatusCode = obj != null ? HttpStatusCode.OK : HttpStatusCode.NoContent;

            // TODO: take move NoContent because there may be content added later in ExtraBody
        }

        public void AddOptions(Response response, Options options)
        {
            throw new System.NotImplementedException("Options response not implemented");
        }

        public void AddAcknowledgment(Response response, Acknowledgment acknowledgment)
        {
        }

        public void AddError(Response response, ErrorInfo error)
        {
        }

        public void AddMultiFeedback(Response response, IEnumerable<Feedback> feedbackItems)
        {
        }
    }
}