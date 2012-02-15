using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeBook.Gui
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class)]
    public class GuiInfoAttribute : TargetedAttribute
    {
        private string displayText,
            displayTextPlural,
            displayTextShort,
            displayTextShortPlural;

        public string DisplayText 
        {
            get { return this.displayText ?? this.Target.Name() ?? this.displayTextShort; } 
            set { this.displayText = value; }
        }

        public string DisplayTextPlural
        {
            get { return this.displayTextPlural ?? this.displayText.Pluralize() ?? this.Target.Name().Pluralize() ?? this.displayTextShortPlural ?? this.displayTextShort.Pluralize(); }
            set { this.displayTextPlural = value; }
        }

        public string DisplayTextShort
        {
            get { return this.displayTextShort ?? this.displayText ?? this.Target.Name(); }
            set { this.displayTextShort = value; }
        }

        public string DisplayTextShortPlural
        {
            get { return this.displayTextShortPlural ?? this.displayTextShort.Pluralize() ?? this.displayTextPlural ?? this.displayText.Pluralize() ?? this.Target.Name().Pluralize(); }
            set { this.displayTextShortPlural = value; }
        }
    }

    public static class GuiFieldExtensions
    {
        public static GuiInfoAttribute GuiInfo(this object obj)
        {
            return obj.GetAttribute<GuiInfoAttribute>() ?? new GuiInfoAttribute { Target = obj };
        }

        public static string DisplayTextFor(this GuiInfoAttribute attribute, double quantity) 
        {
            return quantity == 1.0 ? attribute.DisplayText : attribute.DisplayTextPlural;
        }

        public static string DisplayTextShortFor(this GuiInfoAttribute attribute, double quantity)
        {
            return quantity == 1.0 ? attribute.DisplayTextShort : attribute.DisplayTextShortPlural;
        }
    }
}
