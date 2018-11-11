﻿using Firestorm.Host;

namespace Firestorm.Endpoints.Web
{
    public static class EndpointServicesExtensions
    {
        /// <summary>
        /// Configures the data source for this Firestorm API.
        /// </summary>
        public static IFirestormServicesBuilder AddEndpoints(this IFirestormServicesBuilder builder, RestEndpointConfiguration config)
        {   
            builder.Add<RestEndpointConfiguration>(config);
            builder.Add<IRequestInvoker, EndpointsRequestInvoker>();
            
            return builder;
        }
    }
}