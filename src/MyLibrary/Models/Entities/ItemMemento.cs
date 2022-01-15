using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    public class ItemMemento
    {
        public string Notes { get; }
        public byte[] Image { get; }

        public ItemMemento(string notes, byte[] image)
        {
            this.Notes = notes;
            this.Image = image;
        }//ctor
    }//class
}
