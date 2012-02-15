using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeBook.Gui
{
    public class DisplayPointer<T> : Tuple<T, Func<T, object>>
    {
        public T Item { get { return this.Item1; } }

        public DisplayPointer(T item, Func<T, object> getText)
            : base(item, getText)
        {
        }

        public override string ToString()
        {
            return this.Item2(this.Item).ToString();
        }
    }

    public static class DisplayPointerExtensions
    {
        public static DisplayPointer<T> DisplayPointer<T>(this T item, Func<T, object> getText)
        {
            return new DisplayPointer<T>(item, getText);
        }

        public static DisplayPointer<T> DisplayPointer<T>(this T item, string text)
        {
            return item.DisplayPointer(_ => text);
        }
    }
}
