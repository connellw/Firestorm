using System;
using JetBrains.Annotations;

namespace Firestorm.Endpoints
{
    /// <summary>
    /// The context for the request to an <see cref="IRestEndpoint"/>.
    /// </summary>
    public interface IRestEndpointContext : IDisposable
    {
        /// <summary>
        /// The global configuration for all endpoints in this API.
        /// </summary>
        [NotNull]
        RestEndpointConfiguration Configuration { get; }

        /// <summary>
        /// The logged in user calling the endpoint.
        /// </summary>
        IRestUser User { get; }

        /// <summary>
        /// Event that is called when the endpoint is disposed.
        /// Can/should be used to attach handlers that dispose of dependencies too e.g. Stems.
        /// </summary>
        event EventHandler OnDispose;
    }
}