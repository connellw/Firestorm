﻿using System.Threading.Tasks;
using Firestorm.Endpoints.Formatting;
using Firestorm.Endpoints.Responses;
using Firestorm.Host.Infrastructure;

namespace Firestorm.Endpoints
{
    internal class ResponseWriter
    {
        private readonly IHttpRequestResponder _responder;
        private readonly Response _response;
        private readonly INamingConventionSwitcher _nameSwitcher;

        public ResponseWriter(IHttpRequestResponder responder, Response response, INamingConventionSwitcher nameSwitcher)
        {
            _responder = responder;
            _response = response;
            _nameSwitcher = nameSwitcher;
        }

        public Task WriteAsync()
        {
            _responder.SetStatusCode(_response.StatusCode);

            foreach (var header in _response.Headers)
            {
                _responder.SetResponseHeader(header.Key, header.Value);
            }

            var negotiator = new ContentNegotiator(_responder.GetAcceptHeaders(), _responder.GetContentWriter(), _nameSwitcher);
            return negotiator.SetResponseBodyAsync(_response.GetFullBody());
        }
    }
}