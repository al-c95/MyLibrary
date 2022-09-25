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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MyLibrary.Models.ValueObjects;

namespace MyLibrary
{
    public static class Configuration
    {
        public static readonly string CONNECTION_STRING;

        public const string APP_NAME = "MyLibrary";
        public static readonly AppVersion APP_VERSION = new AppVersion(1, 4, 0);

        static Configuration()
        {
            CONNECTION_STRING = @"Data Source=" + ConfigurationManager.AppSettings.Get("dbPath") + "; foreign keys=True;";
        }

        public static string APP_DESCRIPTION
        {
            get
            {
                StringBuilder descriptionBuilder = new StringBuilder();
                descriptionBuilder.Append("Application for keeping track of books and other \"library\" items.");
                descriptionBuilder.AppendLine("");
                descriptionBuilder.AppendLine("");
                descriptionBuilder.AppendLine("Thanks To: ");
                descriptionBuilder.AppendLine(@"Icons by Yusuke Kamiyamane: https://p.yusukekamiyamane.com/");
                descriptionBuilder.AppendLine("Newtonsoft.Json 13.0.1 by James Newton-King");
                descriptionBuilder.AppendLine("Dapper 2.0.123 by Sam Saffron, Marc Gravell and Nick Craver");
                descriptionBuilder.AppendLine("EPPlus 5.8.8 by EPPlus Software AB");
                descriptionBuilder.AppendLine("CircularProgressBar 2.8.0.16 by Soroush Falahati");
                descriptionBuilder.AppendLine("WinFormAnimation 1.6.0.4 by Soroush Falahati");
                descriptionBuilder.AppendLine("Microsoft.Data.Sqlite.Core by Microsoft");
                descriptionBuilder.AppendLine("EntityFramework 6.4.4 by Microsoft");

                return descriptionBuilder.ToString();
            }
        }

        public const string APP_COPYRIGHT = "License: MIT";

        public static readonly string[] TIPS = new string[]
        {
            "You can add new rows to an Excel workbook exported from MyLibrary which will create new records of books and media items.",
            "When scan mode is enabled while searching online for book data by ISBN, the search will start immediately after a valid ISBN is entered.",
            "Database statistics can be viewed by clicking on the View -> Database statistics menu item.",
            "You can store information about multiple copies of the same item. Click the 'Manage Copies' button after selecting an item in the main window.",
            "You can import data in CSV format for tags, authors, and publishers, via File -> Import -> CSV"
        };
    }//class
}
