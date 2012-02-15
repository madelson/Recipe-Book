using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RecipeBook
{
    public static class Utils
    {
        private class CaseInsensitiveEqualityComparer : IEqualityComparer<string>
        {
            public static readonly CaseInsensitiveEqualityComparer instance = new CaseInsensitiveEqualityComparer();

            bool IEqualityComparer<string>.Equals(string x, string y)
            {
                return string.Compare(x, y, ignoreCase: true) == 0;
            }

            int IEqualityComparer<string>.GetHashCode(string s)
            {
                return s.ToLower().GetHashCode();
            }
        }

        public static IEqualityComparer<string> CaseInsensitiveComparer { get { return CaseInsensitiveEqualityComparer.instance; } }

        public static bool IsUserSure(string question, string caption = "Are you sure?")
        {
            return MessageBox.Show(question, caption, MessageBoxButtons.OKCancel) == DialogResult.OK;
        }

        public static void Alert(string message, string caption = "", MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
        }

        public static void Assert(this bool condition, string errorMessage = "Assertion Failed!")
        {
            if (!condition)
            {
                throw errorMessage.AsError();
            }
        }

        public static T TryInvoke<T>(this Func<T> func, out Exception exception)
        {
            T retVal;

            try
            {
                retVal = func();
                exception = null;
            }
            catch (Exception ex)
            {
                Utils.Alert("failure!" + ex);
                retVal = default(T);
                exception = ex;
            }

            return retVal;
        }

        public static Exception TryInvoke(this Action action)
        {
            Exception ex;
            Func<object> func = () => { action(); return null; };
            func.TryInvoke(out ex);

            return ex;
        }

        public static void LogAnyErrors(params object[] errorObjects)
        {
            if (!errorObjects.IsNullOrEmpty())
            {
                Utils.TryInvoke(() => System.IO.File.WriteAllLines(string.Format("Recipe Book Error @ {0:MM.dd.yyyy (H.mm.ss)}.txt", DateTime.Now),
                    errorObjects.Select(o => o.ToString())));
            }
        }
    }

    public delegate void TypedEventHandler<TControl>(TControl sender);
    public delegate void TypedEventHandler<TControl, TEventArgs>(TControl sender, TEventArgs args);

    public class TargetedAttribute : Attribute
    {
        public object Target { get; set; }
    }

    public static class Extensions
    {
        private static readonly Dictionary<Control, Control> readOnlyControls = new Dictionary<Control, Control>();

        public static void SetReadOnly(this Control control, bool value)
        {
            if (control is Label)
            {
                return;
            }
            
            var readOnlyProperty = control
                .GetType()
                .GetProperties()
                .FirstOrDefault(p => p.Name == "ReadOnly" && p.PropertyType == typeof(bool));

            if (readOnlyProperty != null)
            {
                readOnlyProperty.SetValue(control, value, null);
            }
            else if (value)
            {
                Control readOnlyControl;
                if (readOnlyControls.ContainsKey(control))
                {
                    readOnlyControl = readOnlyControls[control];
                }
                else
                {
                    if (control is CheckBox)
                    {
                        var checkBox = (CheckBox)control;
                        var readOnlyCheckBox = new CheckBox { Checked = checkBox.Checked };
                        checkBox.CheckedChanged += (o, e) => readOnlyCheckBox.Checked = checkBox.Checked;
                        readOnlyCheckBox.CheckedChanged += (o, e) => readOnlyCheckBox.Checked = checkBox.Checked;
                        readOnlyControl = readOnlyCheckBox;
                    }
                    else
                    {
                        readOnlyControl = new Label { UseMnemonic = false };
                    }

                    readOnlyControls[control] = readOnlyControl;
                    control.TextChanged += (o, e) => { readOnlyControl.Text = control.Text; };
                    control.Disposed += (o, e) => { readOnlyControl.Dispose(); readOnlyControls.Remove(control); };
                    readOnlyControl.Disposed += (o, e) => control.Dispose();
                }

                if (!control.Parent.Controls.Contains(readOnlyControl))
                {
                    control.Parent.Controls.Add(readOnlyControl);
                }

                readOnlyControl.Location = control.Location;
                readOnlyControl.Size = control.Size;
                readOnlyControl.Text = control.Text;
                control.Visible = false;
                readOnlyControl.Visible = true;
            }
            else
            {
                if (!readOnlyControls.ContainsKey(control))
                {
                    return;
                }
                readOnlyControls[control].Visible = false;
                control.Visible = true;
            }
        }

        private static readonly Dictionary<Keys, bool> autoCompleteIgnoreKeys = 
            new Keys[] { Keys.Back, Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.Delete, Keys.PageUp, Keys.PageDown, Keys.Home, Keys.End, Keys.ShiftKey }
            .ToDictionary(k => k, k => true);

        public static void AddAutoComplete(this ComboBox comboBox, Func<object, string> getItemText)
        {
            comboBox.KeyUp += (o, e) =>
            {
                // Do nothing for certain keys, such as navigation keys.
                if (autoCompleteIgnoreKeys.ContainsKey(e.KeyCode))
                {
                    return;
                }

                // Store the actual text that has been typed.
                var actual = comboBox.Text;

                var lowerCaseActual = actual.ToLower();
                var matchIndex = comboBox.Items.Cast<object>().IndexWhere(i => getItemText(i).ToLower().StartsWith(lowerCaseActual));

                if (matchIndex >= 0 && !string.IsNullOrEmpty(actual))
                {
                    // Select this item from the list.
                    comboBox.SelectedIndex = matchIndex;

                    // Select the portion of the text that was automatically
                    // added so that additional typing replaces it.
                    var match = getItemText(comboBox.Items[matchIndex]);

                    if (string.Compare(actual, match, ignoreCase: true) != 0)
                    {
                        comboBox.Text = actual + match.Substring(actual.Length);
                        comboBox.SelectionStart = actual.Length;
                        comboBox.SelectionLength = match.Length;
                    }
                }
                else
                {
                    comboBox.SelectedIndex = -1;
                }
            };
        }

        public static void SelectAndFocus(this Control control)
        {
            control.Focus();
            dynamic dynamicControl = control;
            dynamicControl.SelectionStart = 0;
            dynamicControl.SelectionLength = control.Text.Length;
        }

        public static void RowClicked(this DataGridView grid, TypedEventHandler<DataGridView, DataGridViewCellEventArgs> handler)
        {
            grid.CellClick += (o, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    handler(grid, e);
                }
            };
        }

        public static void SelectedRowChanged(this DataGridView grid, TypedEventHandler<DataGridView, DataGridViewCellEventArgs> handler)
        {
            var oldSelectedRowIndex = grid.CurrentCell != null ? grid.CurrentCell.RowIndex : -1;
            grid.CurrentCellChanged += (o, e) =>
            {
                if (grid.CurrentCell == null)
                {
                    return;
                }

                var selectionChanged = oldSelectedRowIndex != grid.CurrentCell.RowIndex;
                oldSelectedRowIndex = grid.CurrentCell.RowIndex;
                if (selectionChanged)
                {
                    handler(grid, new DataGridViewCellEventArgs(grid.CurrentCell.ColumnIndex, grid.CurrentCell.RowIndex));
                }
            };
        }

        public static void CellClicked(this DataGridViewColumn column, TypedEventHandler<DataGridView, DataGridViewCellEventArgs> handler)
        {
            column.DataGridView.RowClicked((g, e) => 
            {
                if (e.ColumnIndex == column.Index)
                {
                    handler(g, e);
                }
            });
        }

        public static void CellValidating(this DataGridViewColumn column, TypedEventHandler<DataGridView, DataGridViewCellValidatingEventArgs> handler)
        {
            column.DataGridView.CellValidating += (o, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == column.Index)
                {
                    handler(column.DataGridView, e);
                }
            };
        }

        public static dynamic AsDynamic(this object obj)
        {
            return obj;
        }

        public static HashSet<T> ToSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static Exception AsError(this string errorMessage)
        {
            return new Exception(errorMessage);
        }

        public static T GetAttribute<T>(this object objOrType, bool alwaysTreatAsObject = false)
            where T : Attribute
        {
            var type = objOrType is Type && !alwaysTreatAsObject
                ? (Type)objOrType
                : objOrType.GetType();
            
            var member = type.GetMember(objOrType.ToString()).FirstOrDefault() ?? type;

            Attribute attribute = null;
            if (member != null)
            {
                attribute = (Attribute)member.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            }
            if (attribute == null)
            {
                attribute = (Attribute)type.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            }

            if (attribute is TargetedAttribute && (object)type != objOrType)
            {
                ((TargetedAttribute)attribute).Target = objOrType;
            }

            return (T)attribute;
        }

        public static string Name(this object objOrType, bool alwaysTreatAsObject = false)
        {
            if (objOrType == null)
            {
                return null;
            }

            var type = objOrType is Type && !alwaysTreatAsObject
                ? (Type)objOrType
                : objOrType.GetType();

            var member = type.GetMember(objOrType.ToString()).FirstOrDefault();

            return (member == null ? type.Name : objOrType.ToString()).SplitCamelCase();
        }

        public static string Pluralize(this string s)
        {
            return string.IsNullOrEmpty(s) || !Char.IsLetterOrDigit(s[s.Length - 1])
                ? s
                : s.EndsWith("y")
                    ? s + "ies"
                    : s.EndsWith("s")
                        ? s + "es"
                        : s + "s";
        }

        public static string Capitalize(this string s)
        {
            return string.IsNullOrEmpty(s) ? s : Char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string SplitCamelCase(this string s, string separator = " ")
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var sb = new StringBuilder();
            foreach (var c in s)
            {
                sb.Append(Char.IsLower(c) || sb.Length == 0 ? (object)c : separator + c);
            }

            return sb.ToString();
        }

        public static int IndexWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, int startIndex = 0)
        {
            return (enumerable
                .Skip(startIndex)
                .Select((v, i) => new { Value = v, Index = i })
                .Where(t => predicate(t.Value))
                .FirstOrDefault() ?? new { Value = default(T), Index = -1 }).Index;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static TEnum ValueOrDefault<TEnum>(this TEnum value, TEnum defaultValue = default(TEnum))
            where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Contains(value)
                ? value
                : defaultValue;
        }

        public static string ToTwoColumns(this string textColumn, int numSpacesBetweenColumns)
        {            
            var lines = textColumn.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var firstHalf = (lines.Length / 2) + (lines.Length % 2);
            var columnOne = lines.Take(firstHalf);
            var columnTwo = lines.Skip(firstHalf).SkipWhile(string.IsNullOrWhiteSpace);
            if (columnTwo.Count() < firstHalf)
            {
                columnTwo = columnTwo.Concat(Enumerable.Repeat(string.Empty, firstHalf - columnTwo.Count()));
            }
            var firstColumnLength = lines.Take(firstHalf).Max(s => s.Length) + numSpacesBetweenColumns;

            return string.Join(
                Environment.NewLine,
                columnOne.Zip(
                    columnTwo,
                    (r1, r2) => r1 + string.Join(string.Empty, Enumerable.Repeat(" ", firstColumnLength - r1.Length)) + r2
                )
            );
        }
    }
}