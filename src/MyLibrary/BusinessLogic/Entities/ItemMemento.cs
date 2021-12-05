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
        public string Image { get; }

        public ItemMemento(string notes, string image)
        {
            this.Notes = notes;
            this.Image = image;
        }//ctor
    }//class
}
