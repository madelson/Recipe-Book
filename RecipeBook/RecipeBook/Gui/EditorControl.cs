using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeBook.Domain;
using System.Windows.Forms;

namespace RecipeBook.Gui
{
    public abstract class EditorControl<C, T> : UserControl
        where C : EditorControl<C, T>, new()
        where T : DomainObject, new()
    {
        public event TypedEventHandler<C> SaveButtonClicked;
        public event TypedEventHandler<C> CancelButtonClicked;

        public abstract void SetDomainObject(T domainObject);        
        public abstract bool TryGetDomainObject(out T domainObject, out IList<string> errorMessages);

        public void RaiseSaveButtonClicked(C control)
        {
            if (this.SaveButtonClicked != null)
            {
                this.SaveButtonClicked(control);
            }
        }

        public void RaiseCancelButtonClicked(C control)
        {
            if (this.CancelButtonClicked != null)
            {
                this.CancelButtonClicked(control);
            }
        }
    }
}
