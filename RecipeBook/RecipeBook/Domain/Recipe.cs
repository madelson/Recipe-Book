using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Xml.Serialization;
using RecipeBook.Gui;

namespace RecipeBook.Domain
{
    [GuiInfo(DisplayText = "recipe")]
    public class Recipe : DomainObject
    {
        public string Source { get; set; }

        public string Steps { get; set; }

        public string Notes { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public override object Clone()
        {
            var copy = (Recipe)this.MemberwiseClone();
            copy.Ingredients = this.Ingredients
                .Select(i => i.Clone())
                .Cast<Ingredient>()
                .ToList();
            
            return copy;
        }
    }
}
