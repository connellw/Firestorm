using System.Collections.Generic;
using System.Linq;
using Firestorm.Endpoints.Tests.Stubs.MemoryResources;

namespace Firestorm.Endpoints.Tests.Stubs
{
    //[StartRestEndpoint("artists")]
    internal class ArtistMemoryCollection : MemoryRestCollection<Artist>
    {
        protected override List<Artist> Entities { get; } = TestRepositories.GetArtists().ToList();

        protected override string GetIdentifier(Artist entity)
        {
            return entity.ID.ToString();
        }
    }
}