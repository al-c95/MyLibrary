using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Models.Entities
{
    public sealed class Copy : Entity
    {
        public Item Item { get; set; }
        public string Notes { get; set; }
    }
}
