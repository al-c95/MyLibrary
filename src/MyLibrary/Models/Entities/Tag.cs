﻿//MIT License

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
                    if (!Validate(value))
                    {
                        throw new ArgumentException("Tag can't have commas.");
                    }
                    else
                    {
                        this._name = value;
                    }
                }
            }//set
        }//Name

        public static bool Validate(string name)
        {
            return !(name.Contains(", ") || name.Contains(","));
        }

        public ICollection<Item> Items { get; set; }
    }//class
}
