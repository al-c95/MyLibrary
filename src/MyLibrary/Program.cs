using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Presenters;
using MyLibrary.Views;

namespace MyLibrary
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow window = new MainWindow();
            ItemPresenter presenter = new ItemPresenter(new BookRepository(), new MediaItemRepository(),
                window);
            window.LoadWindow();
            Application.Run(window);
        }
    }//class
}
