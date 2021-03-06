﻿using System.Threading.Tasks;
using Firestorm.Endpoints.Responses;

namespace Firestorm.Endpoints.Executors
{
    public interface IExecutor
    {
        Task<object> ExecuteAsync(IRequestReader reader, ResponseBuilder response);
        
        IRestEndpoint Endpoint { get; }
    }
}