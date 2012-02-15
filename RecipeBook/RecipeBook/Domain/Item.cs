using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Xml.Serialization;
using RecipeBook.Gui;

namespace RecipeBook.Domain
{
    [GuiInfo(DisplayText = "item")]
    public class Item : DomainObject
    {
        public Category Category { get; set; }

        public UnitType UnitType { get; set; }

        public Unit DefaultRecipeUnit { get; set; }

        public Unit DefaultBuyUnit { get; set; }

        public bool AssumeIsInStock { get; set; }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
