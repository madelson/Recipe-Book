using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeBook.Domain;
using System.Windows.Forms;
using RecipeBook.Data;

namespace RecipeBook.Gui
{
    public static class Dialogs
    {
        public static bool SaveWithUniqueName<T>(T domainObject, bool allowOverwrite) where T : DomainObject
        {
            var sameNameObject = RecipeBooks.Current.GetAll<T>().FirstOrDefault(o => string.Compare(o.Name, domainObject.Name, ignoreCase: true) == 0 && o.Id != domainObject.Id);
            if (sameNameObject != null)
            {
                if (!allowOverwrite)
                {
                    Utils.Alert(string.Format("An {0} named \"{1}\" already exists!", sameNameObject.GuiInfo().DisplayText, domainObject.Name));
                    return false;
                }
                else if (!Utils.IsUserSure(string.Format("An {0} named \"{1}\" already exists. Would you like to overwrite it?", sameNameObject.GuiInfo().DisplayText, domainObject.Name)))
                {
                    return false;
                }
                else
                {
                    RecipeBooks.Current.Remove(sameNameObject);
                }
            }

            RecipeBooks.Current.Add(domainObject);
            return true;
        }

        public static T Edit<C, T>(T domainObject = null, bool allowOverwrite = false)
            where C : EditorControl<C, T>, new()
            where T : DomainObject, new()
        {
            var caption = string.Format((domainObject != null ? "Create New" : "Edit") + " {0}", typeof(T).GuiInfo().DisplayText.Capitalize());

            T ret = null;
            using (var form = new Form { Text = caption, AutoSize = true })
            using (var editor = new C())
            {
                editor.SetDomainObject(domainObject);

                editor.SaveButtonClicked += ie =>
                {
                    IList<string> errorMessages;
                    if (editor.TryGetDomainObject(out ret, out errorMessages))
                    {
                        if (SaveWithUniqueName(ret, allowOverwrite: allowOverwrite))
                        {
                            form.Close();
                        }
                    }
                    else
                    {
                        Utils.Alert(string.Join(Environment.NewLine, errorMessages.ToArray()));
                    }
                };

                editor.CancelButtonClicked += c => form.Close();

                form.Controls.Add(editor);
                form.ShowDialog();
            }

            return ret;
        }
    }
}
