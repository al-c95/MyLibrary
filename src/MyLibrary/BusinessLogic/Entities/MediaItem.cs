using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    public class MediaItem : Item
    {
        public ItemType Type { get; set; }
        public long Number { get; set; }
        public int RunningTime { get; set; }
        public int ReleaseYear { get; set; }
    }
}
