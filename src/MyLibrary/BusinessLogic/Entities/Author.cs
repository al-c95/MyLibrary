using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                if (string.IsNullOrWhiteSpace(value))
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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Author must have last name.");
                else
                    _lastName = value;
            }
        }

        public Author() { }

        public Author(string fullName)
        {
            const string WITH_MIDDLE_NAME_PATTERN = @"^[a-zA-Z]+ [a-zA-Z]. [a-zA-Z]+$";
            const string NO_MIDDLE_NAME_PATTERN = @"^[a-zA-Z]+ [a-zA-Z]+$";

            if (Regex.IsMatch(fullName, WITH_MIDDLE_NAME_PATTERN))
            {
                string[] parts = Regex.Split(fullName, @" [a-zA-Z]. ");
                string middleSubstring = fullName.Substring(fullName.IndexOf(' '), 3);

                this._lastName = parts[1];
                this._firstName = parts[0] + middleSubstring;
            }
            else if (Regex.IsMatch(fullName, NO_MIDDLE_NAME_PATTERN))
            {
                string[] parts = Regex.Split(fullName, " ");
                this._lastName = parts[1];
                this._firstName = parts[0];
            }
            else
            {
                throw new ArgumentException("Author full name: " + fullName + " has incorrect format.");
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
            // TODO: have two initials in the case of a middle name
            return (this.LastName + ", " + this.FirstName.Substring(0,1) + ".");
        }

        public void SetFullNameFromCommaFormat(string name)
        {
            const string NAME_PATTERN = @"^[a-zA-Z]+, [a-zA-Z]+( [a-zA-Z].)?$";

            if (Regex.IsMatch(name, NAME_PATTERN))
            {
                string[] parts = Regex.Split(name, ", ");
                this._firstName = parts[1];
                this._lastName = parts[0];
            }
            else
            {
                throw new ArgumentException("Invalid comma-format name: " + name);
            }
        }

        public ICollection<Book> Books { get; set; }
    }//class
}
