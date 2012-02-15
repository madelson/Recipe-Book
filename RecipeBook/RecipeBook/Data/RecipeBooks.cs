using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecipeBook.Domain;
using System.Xml.Serialization;
using System.IO;

namespace RecipeBook.Data
{
    public class RecipeBooks
    {
        public class RecipeBookImpl : IRecipeBook
        {
            public const string DefaultSavePath = "RecipeBook.xml";

            private string savePath;

            [XmlIgnore]
            public string SavePath 
            { 
                get { return this.savePath ?? DefaultSavePath; }
                set { this.savePath = value; }
            }

            private IDictionary<Guid, DomainObject> domainObjects = new Dictionary<Guid, DomainObject>();
            private static readonly XmlSerializer serializer = new XmlSerializer(typeof(RecipeBookImpl));

            public Recipe[] RecipesCollection
            {
                get { return this.Recipes.ToArray(); }
                set { this.SetAll(value); }
            }

            public Item[] ItemsCollection
            {
                get { return this.Items.ToArray(); }
                set { this.SetAll(value); }
            }

            public ShoppingListItem[] ShoppingListItemsCollection
            {
                get { return this.ShoppingList.ToArray(); }
                set { this.SetAll(value); }
            }

            [XmlIgnore]
            public IEnumerable<Recipe> Recipes
            {
                get { return this.GetAll<Recipe>(); }
            }

            [XmlIgnore]            
            public IEnumerable<Item> Items
            {
                get { return this.GetAll<Item>(); }
            }

            [XmlIgnore]
            public IEnumerable<ShoppingListItem> ShoppingList
            {
                get { return this.GetAll<ShoppingListItem>(); }
            }

            public IEnumerable<T> GetAll<T>()
                where T : DomainObject
            {
                return this.domainObjects.Values.OfType<T>();
            }

            public void SetAll<T>(IEnumerable<T> values)
                where T : DomainObject
            {
                this.GetAll<T>().ToArray().ForEach(o => this.Remove(o.Id));
                if (values != null)
                {
                    values.ForEach(o => this.Add(o));
                }
            }

            public T Get<T>(Guid id) where T : DomainObject
            {
                return (T)this.domainObjects[id];
            }

            public void Add<T>(T domainObject) where T : DomainObject
            {
                Utils.Assert(domainObject.Id != Guid.Empty);

                if (this.domainObjects.ContainsKey(domainObject.Id))
                {
                    this.Remove(domainObject.Id);
                }

                domainObject.RecipeBook = this;
                this.domainObjects[domainObject.Id] = domainObject;
            }

            public void Save()
            {
                var directory = Path.GetDirectoryName(this.SavePath);

                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var tempPath = Path.Combine(directory, string.Format("{0}-{1}-temp.xml", Path.GetFileNameWithoutExtension(this.SavePath), DateTime.Now.Ticks));

                using (var stream = File.OpenWrite(tempPath))
                {
                    serializer.Serialize(stream, this);
                }

                if (File.Exists(this.SavePath))
                {
                    var backupPath = string.Format("{0}.bak", this.SavePath);
                    if (File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                    }
                    File.Move(this.SavePath, backupPath);
                    File.SetAttributes(backupPath, FileAttributes.Hidden);
                }
                File.Move(tempPath, this.SavePath);
            }

            public static RecipeBookImpl Load(string path)
            {
                var savePath = path ?? DefaultSavePath;

                RecipeBookImpl recipeBook;

                if (File.Exists(savePath))
                {
                    using (var stream = File.OpenRead(savePath))
                    {
                        recipeBook = (RecipeBookImpl)serializer.Deserialize(stream);
                        recipeBook.domainObjects.ForEach(o => o.Value.RecipeBook = recipeBook);
                        recipeBook.EnsureConsistency();
                    }
                }
                else
                {
                    recipeBook = new RecipeBookImpl();
                }

                recipeBook.SavePath = savePath;

                return recipeBook;
            }

            public void Remove(Guid id)
            {
                if (this.domainObjects.ContainsKey(id))
                {
                    this.domainObjects[id].RecipeBook = null;
                    this.domainObjects.Remove(id);
                }
            }

            private void EnsureConsistency()
            {
                var errors = new List<string>();

                // check units
                foreach (var badIngredient in this.Recipes
                    .SelectMany(r => r.Ingredients)
                    .Where(i => !this.Get<Item>(i.ItemId).UnitType.Units().Contains(i.Unit))) 
                {
                    errors.Add(string.Format("Ingredient with item {0} ({1}) had bad unit {2}", 
                        this.Get<Item>(badIngredient.ItemId).Name, 
                        this.Get<Item>(badIngredient.ItemId).UnitType,
                        badIngredient.Unit));
                    badIngredient.Unit = this.Get<Item>(badIngredient.ItemId).DefaultRecipeUnit;
                }

                foreach (var badShoppingListItem in this.ShoppingList
                    .Where(sli => !sli.Item.UnitType.Units().Contains(sli.Unit))) 
                {
                    errors.Add(string.Format("Shopping list item with item {0} ({1}) had bad unit {2}",
                        badShoppingListItem.Item.Name,
                        badShoppingListItem.Item.UnitType,
                        badShoppingListItem.Unit));
                    badShoppingListItem.Unit = this.Get<Item>(badShoppingListItem.ItemId).DefaultBuyUnit;
                }

                Utils.LogAnyErrors(errors.ToArray());
            }
        }

        public static IRecipeBook Current { get; private set; }

        private RecipeBooks() { }

        public static void SetCurrent(string savePath = null)
        {
            Current = RecipeBookImpl.Load(savePath);
        }
    }
}
