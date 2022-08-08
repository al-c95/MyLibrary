//MIT License

//Copyright (c) 2022

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
using System.Threading;
using System.Threading.Tasks;
using OfficeOpenXml;
using MyLibrary.Models.Entities;
using MyLibrary.Models.ValueObjects;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;
using MyLibrary.Utils;

namespace MyLibrary.Models.BusinessLogic.ImportExcel
{
    public class MediaItemImportExcelService : ImportExcelService<MediaItem>
    {
        public MediaItemImportExcelService(ExcelPackage excel, AppVersion runningVersion,
            IUnitOfWorkProvider unitOfWorkProvider)
            :base(excel, "Media item", runningVersion, unitOfWorkProvider)
        {
            bool sane = true;
            sane = sane && ReadCellAsString(this._excel, "Media item", "B2").Equals("Media items");
            sane = sane && ReadCellAsString(this._excel, "Media item", "A6").Equals("Id");
            sane = sane && ReadCellAsString(this._excel, "Media item", "B6").Equals("Title");
            sane = sane && ReadCellAsString(this._excel, "Media item", "C6").Equals("Type");
            sane = sane && ReadCellAsString(this._excel, "Media item", "D6").Equals("Number");
            sane = sane && ReadCellAsString(this._excel, "Media item", "E6").Equals("Running Time");
            sane = sane && ReadCellAsString(this._excel, "Media item", "F6").Equals("Release Year");
            sane = sane && ReadCellAsString(this._excel, "Media item", "G6").Equals("Tags");
            sane = sane && ReadCellAsString(this._excel, "Media item", "H6").Equals("Notes");
            if (!sane)
            {
                throw new FormatException("Provided Excel is not a valid export from MyLibrary");
            }
        }

        public override IEnumerable<RowResult> Run()
        {
            throw new NotImplementedException();
        }
    }//class
}
