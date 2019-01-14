﻿using System.Linq;
using System.Threading.Tasks;
using Firestorm.Endpoints;
using Firestorm.Endpoints.Responses;
using Firestorm.Engine;
using Firestorm.Rest.Web;
using Firestorm.Testing;
using Firestorm.Testing.Http;
using Firestorm.Testing.Models;
using Xunit;

namespace Firestorm.AspNetCore2.IntegrationTests
{
    /// <summary>
    /// Some basic tests for the Endpoint chains using the <see cref="IntegratedRestDirectory"/>.
    /// </summary>
    public class EndpointEngineTests
    {
        [Fact]
        public async Task FieldSelector_ManualNext_CorrectName()
        {
            IEndpointContext endpointContext = new TestEndpointContext();

            var testQuery = new TestCollectionQuery
            {
                SelectFields = new[] { "Name" }
            };

            EngineRestCollection<Artist> artistsCollection = IntegratedRestDirectory.GetArtistCollection(endpointContext.Request);
            IRestEndpoint endpoint = endpointContext.Configuration.EndpointResolver.GetFromResource(endpointContext, artistsCollection);
            endpoint = endpoint.Next(new AggregatorNextPath("123", endpointContext.Configuration.NamingConventionSwitcher));
            var response = (ItemBody)(await endpoint.GetAsync(testQuery));

            Assert.Equal(response.Item["Name"], TestRepositories.ArtistName);
        }

        [Fact]
        public async Task FieldSelector_Collection_DoesntThrow()
        {
            IEndpointContext endpointContext = new TestEndpointContext();

            var testQuery = new TestCollectionQuery
            {
                SelectFields = new[] { "Id", "Name" }
            };

            EngineRestCollection<Artist> artistsCollection = IntegratedRestDirectory.GetArtistCollection(endpointContext.Request);
            IRestEndpoint endpoint = endpointContext.Configuration.EndpointResolver.GetFromResource(endpointContext, artistsCollection);
            var resource = (CollectionBody)(await endpoint.GetAsync(testQuery));

            Assert.Equal(1, resource.Items.Count());


            var modifiers = new DefaultResponseModifiers(endpointContext.Configuration.ResponseConfiguration);
            var builder = new ResponseBuilder(new Response("/"), modifiers);
            builder.AddResource(resource);
        }
    }
}
