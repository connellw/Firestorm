﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Firestorm.Rest.Web.Options;
using Newtonsoft.Json;
using Xunit;

namespace Firestorm.Testing.Http.Tests
{
    public abstract class BasicIntegrationTestsBase : HttpIntegrationTestsBase
    {
        protected BasicIntegrationTestsBase(IHttpIntegrationSuite integrationSuite)
            : base(integrationSuite)
        { }

        [Fact]
        public async Task RootDirectory_Get_ContainsArtistsCollection()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("/");
            ResponseAssert.Success(response);

            string json = await response.Content.ReadAsStringAsync();
            var arr = JsonConvert.DeserializeObject<RestItemData[]>(json);

            Assert.Equal("artists", arr[0]["name"]);
            Assert.Equal("collection", arr[0]["type"]);
        }

        [Fact]
        public async Task RootDirectory_Options_SuccessAndDeserialises()
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Options, "/"));
            ResponseAssert.Success(response);

            string json = await response.Content.ReadAsStringAsync();
            var options = JsonConvert.DeserializeObject<Options>(json);
        }

        [Fact]
        public async Task ArtistsCollection_Get_SuccessAndDeserialises()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("/artists");
            ResponseAssert.Success(response);

            string json = await response.Content.ReadAsStringAsync();
            var arr = JsonConvert.DeserializeObject<RestItemData[]>(json);
        }

        [Fact]
        public async Task ArtistItem_Get_SuccessAndDeserialises()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("/artists/123");
            ResponseAssert.Success(response);

            string json = await response.Content.ReadAsStringAsync();
            object obj = JsonConvert.DeserializeObject(json);
        }

        [Fact]
        public async Task ArtistNameScalar_Get_Correct()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("/artists/123/name");
            ResponseAssert.Success(response);

            string responseStr = await response.Content.ReadAsStringAsync();
            Assert.Equal("\"" + TestRepositories.ArtistName + "\"", responseStr);
        }

        [Fact]
        public async Task NonExistentItemID_Get_404()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("/artists/321");
            ResponseAssert.Status(response, HttpStatusCode.NotFound);

            string json = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(json);

            Assert.NotNull(obj);
            Assert.Equal("item_with_identifier_not_found", (string)obj.error);
        }

        [Fact]
        public async Task NonExistentScalarField_Get_404()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("/artists/123/ohdear");
            ResponseAssert.Status(response, HttpStatusCode.NotFound);

            string json = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(json);

            Assert.NotNull(obj);
            Assert.Equal("field_not_found", (string)obj.error);
        }

        [Fact]
        public async Task ArtistsCollection_GetWithFields_DeserialisesAndCorrect()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("/artists?fields=id,name");
            ResponseAssert.Success(response);

            string json = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(json);
            Assert.NotNull(obj);

            string name = obj[0].name;
            Assert.Equal(TestRepositories.ArtistName, name);
        }

        [Fact]
        public async Task ArtistsCollection_Options_DeserialisesAndCorrect()
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Options, "/artists"));
            ResponseAssert.Success(response);

            string json = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(json);
            Assert.NotNull(obj);

            string description = obj.description;
            Assert.NotNull(description);
        }

        [Fact]
        public async Task ArtistsCollection_Add_Successful()
        {
            var content = new JsonContent(new
            {
                id = 10, 
                name = "Haken"
            });
            
            HttpResponseMessage postResponse = await HttpClient.PostAsync("/artists", content);
            ResponseAssert.Success(postResponse);
        }

        [Fact]
        public async Task ArtistsCollection_Delete_Successful()
        {
            HttpResponseMessage deleteResponse = await HttpClient.DeleteAsync("/artists/123");
            ResponseAssert.Success(deleteResponse);
        }
    }
}
