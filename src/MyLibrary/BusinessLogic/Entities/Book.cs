using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    public sealed class Book : Item
    {
        private string _titleLong;
        public string TitleLong
        {
            get => this._titleLong;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Can't have an empty title.");
                else
                    _titleLong = value;
            }
        }

        public string Isbn { get; set; }
        public string Isbn13 { get; set; }

        public string GetIsbn()
        {
            if (string.IsNullOrWhiteSpace(Isbn) && !(string.IsNullOrWhiteSpace(Isbn13)))
            {
                return Isbn13;
            }
            else if (string.IsNullOrWhiteSpace(Isbn13) && !(string.IsNullOrWhiteSpace(Isbn)))
            {
                return Isbn;
            }
            else if (string.IsNullOrWhiteSpace(Isbn13) && (string.IsNullOrWhiteSpace(Isbn)))
            {
                return "";
            }
            else
            {
                return (Isbn + "; " + Isbn13);
            }
        }

        public double? DeweyDecimal { get; set; }
        public string Format { get; set; }
        public string DatePublished { get; set; }
        public string Edition { get; set; }
        public int Pages { get; set; }
        public string Dimensions { get; set; }
        public string Overview { get; set; }

        public string Msrp { get; set; }
        public string Excerpt { get; set; }
        public string Synopsys { get; set; }

        public ICollection<Author> Authors { get; set; }
        public Publisher Publisher { get; set; }
    }
}
