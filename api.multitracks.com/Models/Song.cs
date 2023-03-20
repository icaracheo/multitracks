using System;
using System.Collections.Generic;

namespace api.multitracks.com.Models
{
    public partial class Song
    {
        public int SongId { get; set; }
        public DateTime DateCreation { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; } = null!;
        public decimal Bpm { get; set; }
        public string TimeSignature { get; set; } = null!;
        public bool Multitracks { get; set; }
        public bool CustomMix { get; set; }
        public bool Chart { get; set; }
        public bool RehearsalMix { get; set; }
        public bool Patches { get; set; }
        public bool SongSpecificPatches { get; set; }
        public bool ProPresenter { get; set; }
    }
}
