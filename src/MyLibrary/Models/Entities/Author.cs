//MIT License

//Copyright (c) 2021

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

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
        public static readonly string NAME_PATTERN = @"^[a-zA-Z-]+$";
        public static readonly string WITH_MIDDLE_NAME_PATTERN = @"^[a-zA-Z-]+ [a-zA-Z].$";

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Author must have first name.");
                }
                else
                {
                    if (Regex.IsMatch(value, NAME_PATTERN) ||
                        Regex.IsMatch(value, WITH_MIDDLE_NAME_PATTERN))
                    {
                        _firstName = value;
                    }
                    else
                    {
                        throw new FormatException("Author first name: " + value + " has incorrect format.");
                    }
                }
            }//set
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Author must have last name.");
                }
                else
                {
                    if (Regex.IsMatch(value, NAME_PATTERN))
                    {
                        _lastName = value;
                    }
                    else
                    {
                        throw new FormatException("Author last name: " + value + " has incorrect format.");
                    }
                }
            }//set
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
            const string NAME_PATTERN = @"^[a-zA-Z\-]+, [a-zA-Z]+( [a-zA-Z].)?$";

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
