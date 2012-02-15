using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeBook.Gui;

namespace RecipeBook.Domain
{
    public enum Category : long
    {
        Produce,
        Dairy,

        [GuiInfo(DisplayText = "Sauces, Spices, & Condiments")]
        SaucesSpicesAndCondiments,        
        Canned,
        [GuiInfo(DisplayText = "Pasta and Rice")]
        PastaAndRice,

        Cereal,
        
        Baking,
        Snacks,
        Beverages,

        [GuiInfo(DisplayText = "Bread & Other Breakfast Items")]
        BreadAndOtherBreakfast,

        Meat,
        Frozen,
        Other
    }
}
