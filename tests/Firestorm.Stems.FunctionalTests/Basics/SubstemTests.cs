﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Firestorm.Stems.Essentials;
using Firestorm.Stems.FunctionalTests.Models;
using Firestorm.Stems.FunctionalTests.Web;
using Firestorm.Stems.Roots.DataSource;
using Firestorm.Testing.Http;
using Newtonsoft.Json;
using Xunit;

namespace Firestorm.Stems.FunctionalTests.Basics
{
    public class SubstemTests : IClassFixture<ExampleFixture<SubstemTests>>
    {
        private HttpClient HttpClient { get; }

        public SubstemTests(ExampleFixture<SubstemTests> fixture)
        {
            HttpClient = fixture.HttpClient;
        }

        #region Stems

        [DataSourceRoot]
        public class ArtistsStem : Stem<Artist>
        {
            [Identifier]
            [Get(Display.Nested)]
            public static Expression<Func<Artist, int>> ID
            {
                get { return a => a.ArtistID; }
            }
            
            [Get]
            public static Expression<Func<Artist, string>> Name
            {
                get { return a => a.Name; }
            }

            [Get]
            [Substem(typeof(TracksStem))]
            public static Expression<Func<Artist, IEnumerable<Track>>> Tracks
            {
                get { return a => a.Tracks; }
            }

            [Get]
            [Substem(typeof(AlbumsStem))]
            public static Expression<Func<Artist, IEnumerable<Album>>> Albums
            {
                get { return a => a.Albums; }
            }
        }

        [DataSourceRoot]
        public class AlbumsStem : Stem<Album>
        {
            [Identifier]
            [Get(Display.Nested)]
            public static Expression<Func<Album, int>> ID
            {
                get { return a => a.AlbumID; }
            }
            
            [Get]
            [Substem(typeof(TracksStem))]
            public static Expression<Func<Album, IEnumerable<Track>>> Tracks
            {
                get { return a => a.Tracks; }
            }
        }

        [DataSourceRoot]
        public class TracksStem : Stem<Track>
        {
            [Identifier]
            [Get(Display.Nested)]
            public static Expression<Func<Track, int>> ID
            {
                get { return a => a.TrackID; }
            }
            
            [Get, Set]
            public static Expression<Func<Track, string>> Title
            {
                get { return a => a.Title; }
            }
        }

        #endregion

        [Fact]
        public async Task ArtistTracks_Get_DoesntThrow()
        {
            dynamic obj = await GetDynamicObject("/artists/3/tracks");
        }

        [Fact]
        public async Task ArtistTracks_Post_NewIdentifier()
        {
            HttpResponseMessage response1 = await HttpClient.PostAsJsonAsync("/artists/3/tracks", new
            {
                title = "My new track"
            });

            ResponseAssert.Success(response1);

            string json1 = await response1.Content.ReadAsStringAsync();
            dynamic obj1 = JsonConvert.DeserializeObject(json1);

            Assert.True(obj1.identifier > 0);

            string trackUrl = "/artists/3/tracks/" + obj1.identifier;

            HttpResponseMessage response2 = await HttpClient.PutAsJsonAsync(trackUrl, new
            {
                title = "My edited track"
            });

            ResponseAssert.Success(response2);

            var track = await GetDynamicObject(trackUrl);

            Assert.Equal("My edited track", (string)track.title);
        }

        private async Task<dynamic> GetDynamicObject(string requestUri)
        {
            // TODO same in identifier tests
            HttpResponseMessage response = await HttpClient.GetAsync(requestUri);
            string json = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(json);
            return obj;
        }
    }
}