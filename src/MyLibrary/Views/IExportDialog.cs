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
//SOFTWARE IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE

using System;

namespace MyLibrary.Views
{
    public interface IExportDialog
    {
        string Title { get; set; }
        string Label1 { get; set; }
        string Label2 { get; set; }
        string Path { get; set; }
        Boolean BrowseButtonEnabled { get; set; }
        Boolean StartButtonEnabled { get; set; }
        Boolean CloseButtonEnabled { get; set; }
        Boolean PathFieldEnabled { get; set; }

        string ShowFolderBrowserDialog(string type);
        void ShowErrorDialog(string message);
        void CloseDialog();

        event EventHandler BrowseButtonClicked;
        event EventHandler StartButtonClicked;
        event EventHandler Cancelled;
    }
}
