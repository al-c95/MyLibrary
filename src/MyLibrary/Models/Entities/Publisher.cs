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
            set => ValidateName(value);
        }

        public Publisher() { }

        public Publisher(string name)
        {
            this.ValidateName(name);
        }

        private void ValidateName(string name)
        {
            if (name == null || string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Publisher must have a name.");
            else
                _name = name;
        }

        public ICollection<Book> Books { get; set; }
    }//class
}
