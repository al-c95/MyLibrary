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
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary.Models.Csv
{
    public class TagCsvImport : CsvImport
    {
        public override string GetTypeName => "Tag";

        public TagCsvImport(string[] allLines)
        {
            // validate headers
            if (allLines[0].Equals("Tag"))
            {
                this._lines = allLines;
            }
            else
            {
                throw new FormatException("Tags CSV file does not have appropriate structure.");
            }
        }

        async public static Task<TagCsvImport> BuildAsync(ICsvFile file)
        {
            return new TagCsvImport(await file.ReadLinesAsync());
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
                if (Tag.Validate(line))
                {
                    yield return new CsvRowResult(index + 1, CsvRowResult.Status.SUCCESS, new Tag { Name = line });
                }
                else
                { 
                    yield return new CsvRowResult(index + 1, CsvRowResult.Status.ERROR, null);
                }

                index++;
            }
        }//GetEnumerator

        public override async Task<bool> AddIfNotExists(CsvRowResult row)
        {
            TagService service = new TagService();
            Tag tag = row.Entity as Tag;
            if (await service.ExistsWithName(tag.Name))
            {
                return false;
            }
            else
            {
                await service.Add(tag);

                return true;
            }
        }
    }//class
}
