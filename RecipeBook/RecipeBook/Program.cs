using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RecipeBook.Data;
using RecipeBook.Gui;
using System.IO;

namespace RecipeBook
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RecipeBooks.SetCurrent();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                using (var content = new Tabs())
                using (var form = new Form { Text = "Recipe Book", AutoSize = true })
                {
                    form.Controls.Add(content);
                    Application.ThreadException += (o, e) =>
                    {
                        Utils.Alert("I'm sorry, an unhandled error has occurred in the application", "Oh no!");
                        Utils.LogAnyErrors(e.Exception);
                        Application.ExitThread();
                    };
                    Application.Run(form);
                }
            }
            finally
            {
                Utils.TryInvoke(RecipeBooks.Current.Save);
            }
        }
    }
}
