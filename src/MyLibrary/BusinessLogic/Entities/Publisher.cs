using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    public sealed class Publisher : Entity
    {
        private string _name;
        public string Name
        {
            get => this._name;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Publisher must have a name.");
                }
                else
                {
                    _name = value;
                }
            }
        }

        public ICollection<Book> Books { get; set; }
    }
}
