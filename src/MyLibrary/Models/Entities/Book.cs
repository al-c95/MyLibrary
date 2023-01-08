//MIT License

//Copyright (c) 2021-2023

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

namespace MyLibrary.Models.Entities
{
    public sealed class Book : Item
    {
        public override ItemType Type { get => ItemType.Book; set => throw new InvalidOperationException("Cannot change Book.Type."); }

        public Book()
        {
            this.Authors = new List<Author> { };
        }

        private string _titleLong;
        public string TitleLong
        {
            get => this._titleLong;
            set => this._titleLong = value;
        }

        public static readonly string ISBN_10_PATTERN = @"^(\d|X){10}$";
        public static readonly string ISBN_13_PATTERN = @"^(\d|X){13}$";
        private string _isbn;
        public string Isbn
        {
            get => this._isbn;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                {
                    this._isbn = "";
                }
                else if (Regex.IsMatch(value, ISBN_10_PATTERN))
                {
                    this._isbn = value;
                }
                else
                {
                    throw new FormatException("Isbn: " + value + " has incorrect format.");
                }
            } 
        }

        private string _isbn13;
        public string Isbn13
        {
            get => this._isbn13;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                {
                    this._isbn13 = "";
                }
                else if (Regex.IsMatch(value, ISBN_13_PATTERN))
                {
                    this._isbn13 = value;
                }
                else
                {
                    throw new FormatException("Isbn13: " + value + " has incorrect format.");
                }
            } 
        }

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
                return Isbn13;
            }
        }

        public static readonly string DEWEY_DECIMAL_PATTERN = @"^\d+\.?\d*$";
        public decimal? DeweyDecimal 
        {
            get;
            set;
        }

        public string Format { get; set; }
        public string DatePublished { get; set; }
        public string PlaceOfPublication { get; set; }
        public string Edition { get; set; }
        public int Pages { get; set; }
        public string Dimensions { get; set; }
        public string Overview { get; set; }

        private string _language;
        public string Language
        {
            get => this._language;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Book.Language must not be empty.");

                this._language = value;
            }
        }

        public string Msrp { get; set; }
        public string Excerpt { get; set; }
        public string Synopsys { get; set; }

        public ICollection<Author> Authors { get; set; }
        public Publisher Publisher { get; set; }

        /// <summary>
        /// Get a string of the authors in the format: Lastname, F. and Lastname, F. .... and Lastname, F.
        /// </summary>
        /// <returns></returns>
        public string GetAuthorList()
        {
            if (this.Authors.Count == 0)
                return null;

            StringBuilder authors = new StringBuilder();
            foreach (var author in this.Authors)
            {
                authors.Append(author.GetFullNameWithFirstInitial() + " and ");
            }
            string authorsString = authors.ToString().Substring(0, authors.ToString().Length-5);

            return authorsString;
        }//GetAuthorList

        /// <summary>
        /// Get a string of the authors in the format: Firstname Lastname; Firstname Lastname; ... Firstname Lastname;
        /// </summary>
        /// <returns></returns>
        public string GetAuthorListFullNamesGiven()
        {
            if (this.Authors.Count == 0)
                return null;

            string firstAuthor = this.Authors.First().FirstName + " " + this.Authors.First().LastName;
            if (this.Authors.Count == 1)
            {
                return firstAuthor;
            }
            else
            {
                StringBuilder authors = new StringBuilder();
                foreach (var author in this.Authors)
                {
                    authors.Append(author.FirstName + " " + author.LastName + "; ");
                }

                return authors.ToString().Substring(0, authors.ToString().Length-2);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());

            builder.AppendLine();
            builder.AppendLine();

            ToStringAppendField(builder, "ISBN: ", this.Isbn);
            ToStringAppendField(builder, "ISBN 13: ", this.Isbn13);
            ToStringAppendField(builder, "Long Title: ", this.TitleLong);

            builder.AppendLine("Dewey Decimal: ");
            if (!(DeweyDecimal is null))
            {
                builder.AppendLine(this.DeweyDecimal.ToString());
                builder.AppendLine();
            }
            else
            {
                builder.AppendLine(); 
                builder.AppendLine();
            }

            ToStringAppendField(builder, "Format: ", this.Format);
            ToStringAppendField(builder, "Publisher: ", this.Publisher.Name);
            ToStringAppendField(builder, "Date Published: ", this.DatePublished);
            ToStringAppendField(builder, "Place of Publication: ", this.PlaceOfPublication);
            ToStringAppendField(builder, "Edition: ", this.Edition);
            ToStringAppendField(builder, "Pages: ", this.Pages.ToString());
            ToStringAppendField(builder, "Dimensions: ", this.Dimensions);
            ToStringAppendField(builder, "Overview: ", this.Overview);
            ToStringAppendField(builder, "Language: ", this.Language);
            ToStringAppendField(builder, "MSRP: ", this.Msrp);
            ToStringAppendField(builder, "Excerpt: ", this.Excerpt);
            ToStringAppendField(builder, "Synopsys: ", this.Synopsys);

            builder.AppendLine("Authors: ");
            builder.Append(this.GetAuthorList());

            return builder.ToString();
        }//ToString
    }//class
}
