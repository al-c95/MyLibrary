﻿//MIT License

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

namespace MyLibrary.Views.Excel
{
    public class MediaItemsExcel : ExcelBase<MediaItem>
    {
        public MediaItemsExcel(IExcelFile file)
            :base("Media Items", file)
        {
            WriteHeaderCell("Title", "B");
            WriteHeaderCell("Type", "C");
            WriteHeaderCell("Number", "D");
            WriteHeaderCell("Running Time", "E");
            WriteHeaderCell("Release Year", "F");
            WriteHeaderCell("Tags", "G");
            WriteHeaderCell("Notes", "H");
        }

        public override void WriteEntity(MediaItem entity)
        {
            object[] values = new object[]
            {
                entity.Id,
                entity.Title,
                entity.Type.ToString(),
                entity.Number,
                entity.RunningTime,
                entity.ReleaseYear,
                entity.GetCommaDelimitedTags(),
                entity.Notes
            };
            if (this._currRow % 2 == 0)
            {
                WriteEvenRow(this._currRow, values);
            }
            else
            {
                WriteOddRow(this._currRow, values);
            }

            this._currRow++;
        }
    }
}
