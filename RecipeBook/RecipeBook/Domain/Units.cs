using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeBook.Gui;

namespace RecipeBook.Domain
{
    [Flags]
    public enum UnitType : int
    {
        Unit,

        [GuiInfo(DisplayText = "Weight (standard)")]
        WeightStandard,

        [GuiInfo(DisplayText = "Volume (standard)")]
        VolumeStandard,
    }

    public enum Unit : long
    {
        [UnitInfo(UnitType = UnitType.Unit, Value = 1, IsRecipeDefault = true, IsListDefault = true)]
        Item,

        // standard weight
        [GuiInfo(DisplayTextShort = "oz.")]
        [UnitInfo(UnitType = UnitType.WeightStandard, Value = 1, IsRecipeDefault = true)]
        Ounce,

        [GuiInfo(DisplayTextShort = "lb.", DisplayTextShortPlural = "lbs.")]
        [UnitInfo(UnitType = UnitType.WeightStandard, Value = 16, IsListDefault = true)]
        Pound,

        // standard volume
        [GuiInfo(DisplayTextShort = "tsp.")]
        [UnitInfo(UnitType = UnitType.VolumeStandard, Value = 1, IsRecipeDefault = true)]
        Teaspoon,

        [GuiInfo(DisplayTextShort = "tbsp.")]
        [UnitInfo(UnitType = UnitType.VolumeStandard, Value = 3)]
        Tablespoon,

        [GuiInfo(DisplayTextShort = "fl. oz.")]
        [UnitInfo(UnitType = UnitType.VolumeStandard, Value = 6)]
        FluidOunce,

        [UnitInfo(UnitType = UnitType.VolumeStandard, Value = 48, IsListDefault = true)]
        Cup,

        [GuiInfo(DisplayTextShort = "pt.")]
        [UnitInfo(UnitType = UnitType.VolumeStandard, Value = 96)]
        Pint,

        [GuiInfo(DisplayTextShort = "qt.")]
        [UnitInfo(UnitType = UnitType.VolumeStandard, Value = 192)]
        Quart,

        [GuiInfo(DisplayTextShort = "ga.")]
        [UnitInfo(UnitType = UnitType.VolumeStandard, Value = 768)]
        Gallon,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class UnitInfoAttribute : TargetedAttribute
    {
        public UnitType UnitType { get; set; }
        public double Value { get; set; }
        public bool IsRecipeDefault { get; set; }
        public bool IsListDefault { get; set; }
    }

    public static class UnitUtils
    {
        public static UnitInfoAttribute Info(this Unit unit)
        {
            return unit.GetAttribute<UnitInfoAttribute>();
        }

        public static double Convert(this double value, Unit fromUnit, Unit toUnit)
        {
            var fromInfo = fromUnit.Info();
            var toInfo = toUnit.Info();

            Utils.Assert(fromInfo.UnitType == toInfo.UnitType);

            return (value * fromInfo.Value) / toInfo.Value;
        }

        public static IEnumerable<UnitInfoAttribute> UnitInfos(this UnitType unitType)
        {
            return from Unit unit in Enum.GetValues(typeof(Unit))
                   let info = unit.Info()
                   where info.UnitType == unitType
                   orderby info.Value
                   select info;
        }

        public static IEnumerable<Unit> Units(this UnitType unitType)
        {
            return unitType.UnitInfos().Select(i => (Unit)i.Target);
        }

        public static Unit RecipeDefault(this UnitType unitType)
        {
            return (Unit)unitType.UnitInfos()
                .Where(i => i.IsRecipeDefault)
                .Concat(unitType.UnitInfos())
                .First()
                .Target;
        }

        public static Unit ListDefault(this UnitType unitType)
        {
            return (Unit)unitType.UnitInfos()
                .Where(i => i.IsListDefault)
                .Concat(unitType.UnitInfos())
                .First()
                .Target;
        }

        public static Unit ParseUnit(string value)
        {
            Unit result;
            if (Enum.TryParse<Unit>(value, out result))
            {
                return result;
            }

            result = (from u in Enum.GetValues(typeof(Unit)).Cast<Unit>()
                      let info = u.GuiInfo()
                      where value == info.DisplayText || value == info.DisplayTextShort
                      select u)
                     .First();

            return result;
        }
    }
}
