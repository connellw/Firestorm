using System;
using System.Collections.Generic;

namespace Firestorm.Testing.Models
{
    public class Artist
    {
        public Artist()
        {
        }

        public Artist(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public ICollection<Album> Albums { get; set; }
        
        public DateTime? StartDate { get; set; }
    }
}