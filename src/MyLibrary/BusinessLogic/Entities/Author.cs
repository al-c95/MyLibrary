using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    public sealed class Author : Entity
    {
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Author must have first name.");
                else
                    _firstName = value;
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Author must have last name.");
                else
                    _lastName = value;
            }
        }

        public string GetFullName()
        {
            return (this.FirstName + " " + this.LastName);
        }

        public string GetFullNameLastNameCommaFirstName()
        {
            return (this.LastName + ", " + this.FirstName);
        }

        public string GetFullNameWithFirstInitial()
        {
            return (this.LastName + ", " + this.FirstName.Substring(0,1) + ".");
        }

        public ICollection<Book> Books { get; set; }
    }
}
