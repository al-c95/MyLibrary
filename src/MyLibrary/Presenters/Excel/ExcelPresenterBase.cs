using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Views.Excel;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary.Presenters.Excel
{
    // https://stackoverflow.com/questions/31647268/strategy-pattern-with-strategies-contains-similar-code
    public abstract class ExcelPresenterBase
    {
        /// <summary>
        /// Writes entities as rows to the Excel, then saves the file and disposes of the view.
        /// </summary>
        /// <param name="numberExported"></param>
        /// <returns></returns>
        public abstract Task RenderExcel(IProgress<int> numberExported);

        internal static ExcelPresenterBase GetExcelPresenter(string type, IExcelFile file)
        {
            switch (type)
            {
                case "tag":
                    return new TagExcelPresenter(file);
                case "publisher":
                    return new PublisherExcelPresenter(file);
                case "author":
                    return new AuthorExcelPresenter(file);
                case "media item":
                    return new MediaItemExcelPresenter(file);
                case "book":
                    return new BookExcelPresenter(file);
                case "wishlist":
                    return new WishlistExcelPresenter(file);
                default:
                    throw new Exception("Unknown export type: " + type);
            }
        }
    }//class
}
