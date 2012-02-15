using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Xml.Serialization;

namespace RecipeBook.Domain
{
    public class Ingredient : ICloneable
    {
        public Guid ItemId { get; set; }

        public double Quantity { get; set; }

        public Unit Unit { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
