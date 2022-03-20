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
using System.Threading.Tasks;

namespace MyLibrary.Models.ValueObjects
{
    public class AppVersion
    {
        public readonly int Major;
        public readonly int Minor;
        public readonly int Revision;

        public AppVersion(int major, int minor, int revision)
        {
            this.Major = major;
            this.Minor = minor;
            this.Revision = revision;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(this.Major, this.Minor, this.Revision).GetHashCode();
        }

        public override string ToString()
        {
            return (this.Major + "." + this.Minor + "." + this.Revision);
        }

        public static bool operator == (AppVersion a, AppVersion b)
        {
            if (Object.ReferenceEquals(a,null))
            {
                return Object.ReferenceEquals(b, null);
            }

            return a.Equals(b);
        }

        public static bool operator != (AppVersion a, AppVersion b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            AppVersion that = obj as AppVersion;
            bool equal = true;
            equal = equal && this.Major == that.Major;
            equal = equal && this.Minor == that.Minor;
            equal = equal && this.Revision == that.Revision;

            return equal;
        }
    }
}
