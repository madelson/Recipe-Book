using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Xml.Serialization;
using RecipeBook.Gui;

namespace RecipeBook.Domain
{
    [GuiInfo(DisplayText = "shopping list entry")]
    public class ShoppingListItem : DomainObject
    {
        [XmlIgnore]
        public Recipe Recipe
        {
            get { return this.RecipeId.HasValue ? this.RecipeBook.Get<Recipe>(this.RecipeId.Value) : null; }
        }

        public Guid? RecipeId { get; set; }

        [XmlIgnore]
        public Item Item { get { return this.RecipeBook.Get<Item>(this.ItemId); } }

        public Guid ItemId { get; set; }

        public double Quantity { get; set; }

        public Unit Unit { get; set; }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
