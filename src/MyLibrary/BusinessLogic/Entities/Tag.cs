using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    public sealed class Tag : Entity
    {
        private string _name;
        public string Name
        {
            get => this._name;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Tag can't be empty.");
                else
                {
                    if (value.Contains(", ") ||
                        value.Contains(","))
                    {
                        throw new ArgumentException("Tag can't have commas.");
                    }
                    else
                    {
                        this._name = value;
                    }
                }
            }
        }

        public ICollection<Item> Items { get; set; }
    }
}
