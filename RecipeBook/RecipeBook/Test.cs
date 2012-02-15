using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using RecipeBook.Domain;
using RecipeBook.Data;
using RecipeBook.Gui;
using System.Xml.Serialization;

namespace RecipeBook
{
    public class Test
    {
        public static void Run()
        {
            //var constr = @"Data Source=RecipeBookDatabase.sdf";
            //var cn = new System.Data.SqlServerCe.SqlCeConnection(constr);
            //System.Data.IDbConnection e = cn;
            //System.Data.Linq.DataContext dc = new System.Data.Linq.DataContext(cn);
            //if (!dc.DatabaseExists())
            //{
            //    dc.GetTable<Recipe>();
            //    dc.CreateDatabase();
            //    dc.SubmitChanges();
            //}

            //var db = new RecipeBookDatabase();
            //db.DeleteDatabase();
            //if (!db.DatabaseExists())
            //{
            //    db.CreateDatabase();
            //}

            //var units = UnitType.VolumeStandard.Units().ToArray();
            //var cups = (4.0).Convert(Unit.Gallon, Unit.Quart);

            //var strings = string.Join(Environment.NewLine, units.Select(u => u.GuiInfo()).Select(i => string.Join(", ", i.DisplayText, i.DisplayTextPlural, i.DisplayTextShort, i.DisplayTextShortPlural)));

            //RecipeBooks.SetCurrent();

            //var entities = new object[][] { RecipeBooks.Current.Recipes.ToArray() };

            //RecipeBooks.UpdateCurrent();

            //var db = RecipeBooks.Current;

            //Recipe r = new Recipe { Name = "hello " + DateTime.Now.Second };
            //Recipe r2 = new Recipe { Name = "x" + DateTime.Now.Second, Source = string.Join(",", Enumerable.Range(1, 1000)) };

            //var list = RecipeBooks.Current.Recipes.ToArray();
            //db.Save(r);
            //db.Save(r2);

            //var list2 = db.Recipes.Where(x => x.Source != null && x.Source.Contains("999")).ToArray();
            //var list3 = db.Recipes.ToArray();

            //db.Items.ForEach(i => db.Delete(i));
            //Item it = new Item { Name = "milk", Category = Category.Dairy, UnitType = UnitType.VolumeStandard };
            //db.Save(it);

            //it = new Item { Name = "broc", Category = Category.Produce, UnitType = UnitType.Unit };
            //db.Save(it);

            var item = new Item { Name = "broc", Category = Category.Produce, UnitType = UnitType.Unit, Id = Guid.NewGuid() };
            var it2 = new Item { Name = "milk", Category = Category.Dairy, UnitType = UnitType.VolumeStandard, Id = Guid.NewGuid() };

            var cont = new Container();
            //cont.Items.Add(item);
            //cont.Items.Add(it2);
            var recipe = new Recipe { Name = "hello", Notes = "notes", Source = "s", Steps = "st", Id = Guid.NewGuid() };
            var ing = new Ingredient { Quantity = 10, Unit = Domain.Unit.Cup, ItemId = item.Id };
            //recipe.Ingredients = new HashSet<Ingredient> { ing };
            recipe.Ingredients = new List<Ingredient>();
            recipe.Ingredients.Add(new Ingredient());
            //cont.Ingredients.Add(ing);
            //cont.Recipes.Add(recipe);

            //var s = new XmlSerializer(typeof(Container));


            //if (System.IO.File.Exists("items.xml") && System.IO.File.ReadAllText("items.xml").Length > 0)
            //    using (var st = new System.IO.BufferedStream(System.IO.File.OpenRead("items.xml")))
            //    {
            //        var c = (Container)s.Deserialize(st);
            //        cont.Items.AddRange(c.Items);
            //    }

            //using (var st = new System.IO.BufferedStream(System.IO.File.OpenWrite("items.xml")))
            //{
            //    s.Serialize(st, cont);
            //}

            RecipeBooks.SetCurrent();
            RecipeBooks.Current.Add(item);
            RecipeBooks.Current.Add(it2);
            RecipeBooks.Current.Add(recipe);
            RecipeBooks.Current.Save();
        }

        public class Container
        {
            public List<Item> Items { get; set; }
            public List<Recipe> Recipes { get; set; }
            public List<Ingredient> Ingredients { get; set; }

            public Container()
            {
                this.Items = new List<Item>();
                this.Recipes = new List<Recipe>();
                this.Ingredients = new List<Ingredient>();
            }
        }
    }
}
