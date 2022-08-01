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
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary.Models.BusinessLogic
{
    /// <summary>
    /// Base class for implementing services dealing with data-access repositories.
    /// Managed driver does not support asynchronous operations, so we provide "asynchronous" wrappers.
    /// </summary>
    public abstract class ServiceBase
    {
        protected readonly IUnitOfWorkProvider _uowProvider;

        /// <summary>
        /// Default constructor. Instantiates real unit of work.
        /// </summary>
        public ServiceBase()
        {
            this._uowProvider = new UnitOfWorkProvider();
        }

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="uowProvider"></param>
        public ServiceBase(IUnitOfWorkProvider uowProvider)
        {
            this._uowProvider = uowProvider;
        }
    }//class
}
