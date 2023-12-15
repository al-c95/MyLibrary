//MIT License

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
using MyLibrary.Models.Entities;

namespace MyLibrary.Import
{
    public abstract class CsvImportCollection<T> where T : Entity
    {
        protected ICsvParserService _csvParserService;
        protected List<T> _entities;

        public int ParsedCount { get; protected set; }
        public int SkippedCount { get; protected set; }

        public CsvImportCollection(ICsvParserService parserService)
        {
            this._entities = new List<T>();
            this._csvParserService = parserService;
        }

        public abstract void LoadFromFile(string fileName, Action<int, int> progressCallback);
        public IEnumerable<T> GetAll() => this._entities;
    }
}