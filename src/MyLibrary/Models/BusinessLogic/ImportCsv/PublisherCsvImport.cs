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
using System.Threading.Tasks;
using MyLibrary.Models.Entities;

namespace MyLibrary.Models.BusinessLogic.ImportCsv
{
    public class PublisherCsvImport : CsvImport
    {
        private IPublisherService _service;

        public PublisherCsvImport(string[] allLines, IPublisherService service)
        {
            // validate header
            if (allLines[0].Equals("Publisher"))
            {
                this._lines = allLines;
            }
            else
            {
                throw new FormatException("Publishers CSV file does not have appropriate structure.");
            }

            this._service = service;
        }

        public override string GetTypeName => "Publisher";

        async public static Task<PublisherCsvImport> BuildAsync(ICsvFile file)
        {
            return new PublisherCsvImport(await file.ReadLinesAsync(), new PublisherService());
        }

        public async override Task<bool> AddIfNotExists(CsvRowResult row)
        {
            Publisher publisher = row.Entity as Publisher;
            if (await this._service.ExistsWithName(publisher.Name))
            {
                return false;
            }
            else
            {
                await this._service.Add(publisher);

                return true;
            }
        }

        public override IEnumerator<CsvRowResult> GetEnumerator()
        {
            int index = 0;
            foreach (var line in this._lines)
            {
                if (index == 0)
                {
                    // skipping header row
                    index++;
                    continue;
                }

                // read data row and get result
                // TODO: refactor validation
                if (line == null || string.IsNullOrWhiteSpace(line))
                {
                    yield return new CsvRowResult(index + 1, CsvRowResult.Status.ERROR, null, null);
                }
                else
                {
                    yield return new CsvRowResult(index + 1, CsvRowResult.Status.SUCCESS, new Publisher { Name = line }, line);
                }

                index++;
            }
        }//GetEnumerator
    }
}
