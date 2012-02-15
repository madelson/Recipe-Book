using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeBook.Domain
{
    public static class Constants
    {
        private static readonly IEnumerable<double> defaultQuantities = new[] { 0.25, .33, .5, 1, 2 };

        public static IEnumerable<double> DefaultQuantities
        {
            get { return defaultQuantities; }
        }
    }
}
