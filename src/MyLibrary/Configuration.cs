using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MyLibrary
{
    static class Configuration
    {
        // database connection string
        public static readonly string CONNECTION_STRING;

        public const string APP_NAME = "MyLibrary";
        public const string APP_VERSION = "0.9.0";

        public static string APP_DESCRIPTION
        {
            get
            {
                StringBuilder descriptionBuilder = new StringBuilder();
                descriptionBuilder.Append("Application for keeping track of books and other \"library\" items.");
                descriptionBuilder.AppendLine("");
                descriptionBuilder.AppendLine("");
                descriptionBuilder.AppendLine("Thanks To: ");
                descriptionBuilder.AppendLine("Newtonsoft.Json 13.0.1 by James Newton-King");
                descriptionBuilder.AppendLine("Dapper 2.0.123 by Sam Saffron, Marc Gravell and Nick Craver");
                descriptionBuilder.AppendLine("Microsoft.Data.Sqlite.Core by Microsoft");

                return descriptionBuilder.ToString();
            }
        }

        public const string APP_COPYRIGHT = "License: MIT";

        static Configuration()
        {
            CONNECTION_STRING = @"Data Source=" + ConfigurationManager.AppSettings.Get("dbPath") + "; foreign keys=True";
        }
    }//class
}
