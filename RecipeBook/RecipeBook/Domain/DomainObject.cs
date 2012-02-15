using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RecipeBook.Domain
{
    public abstract class DomainObject : ICloneable
    {
        private Guid id = Guid.Empty;
        public Guid Id 
        {
            get { return this.id; }
            set
            {
                if (this.id != Guid.Empty) 
                {
                    throw new Exception("Id property set twice!");
                }

                this.id = value;
            }
        }

        public string Name { get; set; }

        [XmlIgnore]
        public IRecipeBook RecipeBook { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as DomainObject;

            return that != null && this.Id == that.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public abstract object Clone();
    }
}
