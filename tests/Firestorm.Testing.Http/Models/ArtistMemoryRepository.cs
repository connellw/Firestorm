using System.Linq;
using Firestorm.Engine.Defaults;
using Firestorm.Testing.Models;
using Firestorm.Testing;

namespace Firestorm.Testing.Http.Models
{
    public class ArtistMemoryRepository : MemoryRepository<Artist>
    {
        public ArtistMemoryRepository()
            :base(TestRepositories.GetArtists().ToList())
        {
            
        }
    }
}