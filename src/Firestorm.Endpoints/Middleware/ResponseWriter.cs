﻿using System.Threading.Tasks;
using Firestorm.Endpoints.Formatting;
using Firestorm.Endpoints.Responses;

namespace Firestorm.Endpoints.Web
{
    internal class ResponseWriter
    {
        private readonly IHttpRequestResponder _responder;
        private readonly Response _response;
        private readonly RestEndpointConfiguration _endpointConfig;

        public ResponseWriter(IHttpRequestResponder responder, Response response, RestEndpointConfiguration endpointConfig)
        {
            _responder = responder;
            _response = response;
            _endpointConfig = endpointConfig;
        }

        public Task WriteAsync()
        {
            _responder.SetStatusCode(_response.StatusCode);

            foreach (var header in _response.Headers)
            {
                _responder.SetResponseHeader(header.Key, header.Value);
            }

            var negotiator = new ContentNegotiator(_responder.GetAcceptHeaders(), _responder.GetContentWriter(), _endpointConfig.NamingConventionSwitcher);
            return negotiator.SetResponseBodyAsync(_response.GetFullBody());
        }
    }
}