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
using System.Threading.Tasks;

namespace MyLibrary.Views
{
    public interface IExcelImportDialog
    {
        string Label1Text { get; set; }
        string Label2Text { get; set; }
        bool CloseButtonEnabled { get; set; }
        bool BrowseButtonEnabled { get; set; }
        bool FileFieldEnabled { get; set; }
        string FileFieldText { get; set; }
        bool StartButtonEnabled { get; set; }
        bool BookChecked { get; set; }
        bool MediaItemChecked { get; set; }
        bool RadioButtonsEnabled { get; set; }

        void AddSuccess(string message);
        void AddWarning(string message);
        void AddError(string message);
        string ShowFileBrowserDialog();
        void ShowErrorDialog(Exception exception);

        event EventHandler BrowseButtonClicked;
        event EventHandler FileFieldTextChanged;
        event EventHandler StartButtonClicked;
    }
}
