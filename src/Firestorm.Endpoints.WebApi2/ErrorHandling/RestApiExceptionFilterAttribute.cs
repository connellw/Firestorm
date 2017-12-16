using System;
using System.Net.Http;
using System.Web.Http.Filters;
using Firestorm.Core.Web;
using Firestorm.Endpoints.Responses;
using Firestorm.Endpoints.Start;

namespace Firestorm.Endpoints.WebApi2.ErrorHandling
{
    public class RestApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var controller = context.ActionContext.ControllerContext.Controller as FirestormController;
            if (controller == null)
                throw new ArgumentException("RestApiExceptionFilterAttribute should only be applied to FirestormController.");
            
            var exceptionInfo = new ExceptionErrorInfo(context.Exception);

            var response = new Response(controller.ResourcePath);
            controller.ResponseBuilder.AddError(response, exceptionInfo);

            context.Response = context.Request.CreateResponse(response.StatusCode, response.GetFullBody());
        }
    }
}