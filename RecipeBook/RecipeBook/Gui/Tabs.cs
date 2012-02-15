using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecipeBook.Data;
using RecipeBook.Domain;
using System.IO;

namespace RecipeBook.Gui
{
    public partial class Tabs : UserControl
    {
        public const string ADD_TO_LIST = "Add to list", REMOVE_FROM_LIST = "Remove from list";

        public Tabs()
        {
            InitializeComponent();

            this.InitializeRecipesTab();
            this.InitializeItemsTab();
            this.InitializeShoppingListTab();
        }

        private void InitializeRecipesTab()
        {
            this.deleteRecipeColumn.CellClicked((g, e) =>
            {
                var recipeId = (Guid)g.Rows[e.RowIndex].Cells[this.recipeIdColumn.Index].Value;
                var recipe = RecipeBooks.Current.Recipes.SingleOrDefault(r => r.Id == recipeId);
                if (recipe == null)
                {
                    Utils.Alert("Recipe was already deleted!");
                }
                else if (Utils.IsUserSure(string.Format("Delete recipe \"{0}\"?", g.Rows[e.RowIndex].Cells[this.recipeNameColumn.Index].Value)))
                {
                    RecipeBooks.Current.RemoveFromShoppingList(recipe);
                    RecipeBooks.Current.Remove(recipe);
                    g.Rows.RemoveAt(e.RowIndex);
                }
            });

            this.addRecipeToListColumn.CellClicked((g, e) =>
            {
                var cell = g.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var recipeId = (Guid)g.Rows[e.RowIndex].Cells[this.recipeIdColumn.Index].Value;
                var recipe = RecipeBooks.Current.Get<Recipe>(recipeId);
                if (cell.Value.ToString() == ADD_TO_LIST)
                {
                    var quantity = double.Parse(g.Rows[e.RowIndex].Cells[this.recipeQuantityColumn.Index].Value.ToString());
                    using (var form = new Form { AutoSize = true, Text = string.Format("Select ingredients for {0}{1}", quantity != 1.0 ? quantity + "x " : string.Empty, recipe.Name) })
                    using (var recipeEditor = new RecipeEditor { CheckBoxMode = true })
                    {
                        recipeEditor.SetRecipe(recipe);
                        recipeEditor.SaveButtonClicked += re =>
                        {
                            re.GetIngredients(onlySelected: true)
                                .ForEach(i => RecipeBooks.Current.Add(new ShoppingListItem
                                {
                                    Id = Guid.NewGuid(),
                                    RecipeId = recipeId,
                                    ItemId = i.ItemId,
                                    Quantity = i.Quantity * quantity,
                                    Unit = i.Unit
                                }));
                            form.Close();

                            cell.Value = REMOVE_FROM_LIST;
                        };

                        form.Controls.Add(recipeEditor);
                        form.ShowDialog();
                    }
                }
                else
                {
                    RecipeBooks.Current.RemoveFromShoppingList(recipe);

                    cell.Value = ADD_TO_LIST;
                }
            });

            this.recipeQuantityColumn.CellValidating((g, e) =>
            {
                double value;
                if (!double.TryParse(e.FormattedValue.ToString(), out value) || value <= 0)
                {
                    Utils.Alert(string.Format("'{0}' is not a valid value for quantity!", e.FormattedValue));
                    e.Cancel = true;
                }
            });

            TypedEventHandler<DataGridView, DataGridViewCellEventArgs> displayRecipe = (g, e) =>
            {
                if (this.recipeEditor.ReadOnly)
                {
                    var recipeId = (Guid)g.Rows[e.RowIndex].Cells[this.recipeIdColumn.Index].Value;
                    var recipe = RecipeBooks.Current.Recipes.Single(r => r.Id == recipeId);
                    this.recipeEditor.SetRecipe(recipe);
                }
            };
            this.recipeGrid.SelectedRowChanged(displayRecipe);

            this.createNewRecipeButton.Click += (o, e) =>
            {
                var newRecipe = Dialogs.Edit<RecipeEditor, Recipe>(allowOverwrite: false);
                if (newRecipe != null)
                {
                    this.recipeGrid.Rows.Add(this.CreateRow(newRecipe));
                }
            };

            this.recipeEditor.SaveButtonClicked += re =>
            {
                Recipe recipe;
                IList<string> errorMessages;
                if (re.TryGetRecipe(out recipe, out errorMessages))
                {
                    if (Dialogs.SaveWithUniqueName(recipe, allowOverwrite: false))
                    {
                        re.ReadOnly = true;
                    }
                }
                else
                {
                    Utils.Alert(string.Join(Environment.NewLine, errorMessages.ToArray()));
                }
            };

            this.recipeSearchTextBox.TextChanged += (o, e) =>
            {
                var text = this.recipeSearchTextBox.Text.ToLower();

                var matches = (from r in RecipeBooks.Current.Recipes
                               where r.Name.ToLower().Contains(text)
                                   || r.Source.ToLower().Contains(text)
                                   || r.Steps.ToLower().Contains(text)
                                   || r.Notes.ToLower().Contains(text)
                                   || r.Ingredients.Any(i => r.RecipeBook.Get<Item>(i.ItemId).Name.ToLower().Contains(text))
                               select r.Id)
                               .ToSet();

                this.recipeGrid.Rows
                    .Cast<DataGridViewRow>()
                    .ForEach(r => r.Visible = matches.Contains((Guid)r.Cells[this.recipeIdColumn.Index].Value));
            };

            Action refreshRecipes = () =>
            {
                if (RecipeBooks.Current != null)
                {
                    this.recipeGrid.Rows.Clear();
                    this.recipeGrid.Rows.AddRange(RecipeBooks.Current.Recipes.Select(this.CreateRow).ToArray());
                    if (this.recipeGrid.Rows.Count > 0)
                    {
                        displayRecipe(this.recipeGrid, new DataGridViewCellEventArgs(0, 0));
                    }
                }
            };

            this.tabControl.Selected += (o, e) =>
            {
                if (e.TabPage == this.recipesTab)
                {
                    refreshRecipes();
                }
            };

            refreshRecipes();
        }

        private void InitializeItemsTab()
        {
            this.deleteItemColumn.CellClicked((g, e) =>
            {
                var itemId = (Guid)g.Rows[e.RowIndex].Cells[this.itemIdColumn.Index].Value;
                var item = RecipeBooks.Current.Items.SingleOrDefault(r => r.Id == itemId);
                if (item == null)
                {
                    Utils.Alert("Recipe was already deleted!");
                    return;
                }

                var usages = RecipeBooks.Current.Recipes
                    .Where(r => r.Ingredients.Any(i => i.ItemId == itemId))
                    .ToArray();
                if (usages.Length > 0)
                {
                    Utils.Alert(string.Format("Cannot delete {0} since it is currently part of {1} recipe(s) ({2})",
                        item.Name, usages.Length, string.Join(", ", usages.Select(r => r.Name))));
                }
                else if (Utils.IsUserSure(string.Format("Delete item \"{0}\"?", g.Rows[e.RowIndex].Cells[this.itemNameColumn.Index].Value)))
                {
                    RecipeBooks.Current.RemoveFromShoppingList(item);
                    RecipeBooks.Current.Remove(item);
                    g.Rows.RemoveAt(e.RowIndex);
                }
            });

            this.addItemToListColumn.CellClicked((g, e) =>
            {
                var cell = g.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var itemId = (Guid)g.Rows[e.RowIndex].Cells[this.itemIdColumn.Index].Value;
                if (cell.Value.ToString() == ADD_TO_LIST)
                {
                    var quantity = double.Parse(g.Rows[e.RowIndex].Cells[this.itemQuantityColumn.Index].Value.ToString());
                    var unit = UnitUtils.ParseUnit(g.Rows[e.RowIndex].Cells[this.itemUnitsColumn.Index].Value.ToString());
                    RecipeBooks.Current.Add(new ShoppingListItem
                    {
                        Id = Guid.NewGuid(),
                        RecipeId = null,
                        ItemId = itemId,
                        Quantity = quantity,
                        Unit = unit
                    });

                    cell.Value = REMOVE_FROM_LIST;
                }
                else
                {
                    var item = RecipeBooks.Current.Get<Item>(itemId);
                    RecipeBooks.Current.RemoveFromShoppingList(item, excludeIfInRecipe: true);

                    cell.Value = ADD_TO_LIST;
                }
            });

            this.itemQuantityColumn.CellValidating((g, e) =>
            {
                double value;
                if (!double.TryParse(e.FormattedValue.ToString(), out value) || value <= 0)
                {
                    Utils.Alert(string.Format("'{0}' is not a valid value for quantity!", e.FormattedValue));
                    e.Cancel = true;
                }
            });

            TypedEventHandler<DataGridView, DataGridViewCellEventArgs> displayItem = (g, e) =>
            {
                if (this.itemEditor.ReadOnly)
                {
                    var itemId = (Guid)g.Rows[e.RowIndex].Cells[this.itemIdColumn.Index].Value;
                    var item = RecipeBooks.Current.Get<Item>(itemId);
                    this.itemEditor.SetItem(item);
                }
            };
            this.itemGrid.SelectedRowChanged(displayItem);

            this.createNewItemButton.Click += (o, e) =>
            {
                var newItem = Dialogs.Edit<ItemEditor, Item>(allowOverwrite: false);
                if (newItem != null)
                {
                    this.itemGrid.Rows.Add(this.CreateRow(newItem));
                }
            };

            this.itemEditor.SaveButtonClicked += ie =>
            {
                Item item;
                IList<string> errorMessages;
                if (ie.TryGetItem(out item, out errorMessages))
                {
                    if (Dialogs.SaveWithUniqueName(item, allowOverwrite: false))
                    {
                        ie.ReadOnly = true;
                    }
                }
                else
                {
                    Utils.Alert(string.Join(Environment.NewLine, errorMessages.ToArray()));
                }
            };

            this.itemSearchTextBox.TextChanged += (o, e) =>
            {
                var text = this.itemSearchTextBox.Text.ToLower();

                var matches = RecipeBooks.Current.Items
                    .Where(i => i.Name.ToLower().Contains(text))
                    .Select(i => i.Id)
                    .ToSet();

                this.itemGrid.Rows
                    .Cast<DataGridViewRow>()
                    .ForEach(r => r.Visible = matches.Contains((Guid)r.Cells[this.itemIdColumn.Index].Value));
            };

            this.tabControl.Selected += (o, e) =>
            {
                if (e.TabPage == this.itemsTab && RecipeBooks.Current != null)
                {
                    this.itemGrid.Rows.Clear();
                    this.itemGrid.Rows.AddRange(RecipeBooks.Current.Items.Select(this.CreateRow).ToArray());
                    if (this.itemGrid.Rows.Count > 0)
                    {
                        displayItem(this.itemGrid, new DataGridViewCellEventArgs(0, 0));
                    }
                }
            };
        }

        private void InitializeShoppingListTab()
        {
            var listText = string.Empty;
            Action<bool> refreshList = askToOverwrite =>
            {
                if (listText != this.shoppingListTextBox.Text
                    && (!askToOverwrite || !Utils.IsUserSure("Do you want to overwrite you changes with an updated version of the shopping list?")))
                {
                    return;
                }

                var entries = this.GetEntries();
                listText = entries.GroupBy(e => e.Item.Category)
                    .OrderBy(g => g.Key)
                    .Aggregate(new StringBuilder(),
                    (sb, g) =>
                    {
                        sb.AppendLine(g.Key.GuiInfo().DisplayText.Capitalize());
                        g.OrderBy(e => e.Item.Name.ToLower())
                            .ForEach(e =>
                            {
                                sb.AppendFormat("__ {0} ({1} {2})", e.Item.Name, e.Quantity, e.Unit.GuiInfo().DisplayTextShortFor(e.Quantity))
                                  .AppendLine();
                            });

                        return sb.AppendLine();
                    })
                    .ToString();

                this.shoppingListTextBox.Text = listText;
            };

            this.refreshListButton.Click += (o, e) => refreshList(true);
            this.tabControl.Selected += (o, e) =>
            {
                if (e.TabPage == this.listTab)
                {
                    refreshList(false);
                }
            };

            this.exportListButton.Click += (o, e) =>
            {
                try
                {
                    var tempName = "ShoppingList" + DateTime.Now.Ticks + ".txt";
                    var tempPath = Path.Combine(Path.GetTempPath(), tempName);
                    using (var writer = new StreamWriter(File.OpenWrite(tempPath)))
                    {
                        writer.WriteLine(this.useTwoColumnsBox.Checked 
                            ? this.shoppingListTextBox.Text.ToTwoColumns(numSpacesBetweenColumns: 4) 
                            : this.shoppingListTextBox.Text);
                    }

                    System.Diagnostics.Process.Start(tempPath);
                }
                catch (Exception ex)
                {
                    Utils.Alert(string.Format("Export failed! ({0}: {1})", ex.GetType().Name, ex.Message));
                }
            };

            this.clearListButton.Click += (o, e) =>
            {
                if (Utils.IsUserSure("Clear shopping list?"))
                {
                    RecipeBooks.Current.ShoppingList
                        .ToArray()
                        .ForEach(RecipeBooks.Current.Remove);
                    refreshList(true);
                }
            };
        }

        private DataGridViewRow CreateRow(Recipe recipe)
        {
            var row = new DataGridViewRow();

            row.Cells.Add(new DataGridViewTextBoxCell { Value = recipe.Name });
            row.Cells.Add(new DataGridViewButtonCell { Value = "Delete" });

            var isInShoppingList = RecipeBooks.Current.IsInShoppingList(recipe);
            
            row.Cells.Add(new DataGridViewTextBoxCell
            {
                Value = isInShoppingList
                    ? (from sli in RecipeBooks.Current.ShoppingList
                      where sli.RecipeId == recipe.Id
                      join i in recipe.Ingredients
                        on sli.ItemId equals i.ItemId
                      select sli.Quantity / i.Quantity)
                      .First()
                    : 1
            });
            row.Cells.Add(new DataGridViewButtonCell { Value = isInShoppingList ? REMOVE_FROM_LIST : ADD_TO_LIST });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = recipe.Id });

            return row;
        }

        private DataGridViewRow CreateRow(Item item)
        {
            var row = new DataGridViewRow();

            row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Name });
            row.Cells.Add(new DataGridViewButtonCell { Value = "Delete" });

            var isInShoppingList = RecipeBooks.Current.IsInShoppingList(item, excludeIfInRecipe: true);
            row.Cells.Add(new DataGridViewTextBoxCell
            {
                Value = isInShoppingList
                    ? RecipeBooks.Current.ShoppingList
                        .Where(sli => sli.Item.Id == item.Id && sli.Recipe == null)
                        .Sum(sli => sli.Quantity)
                    : 1
            });

            var unitCell = new DataGridViewComboBoxCell { DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing };
            unitCell.Items.AddRange(item.UnitType.Units().Select(u => u.GuiInfo().DisplayText).ToArray());
            unitCell.Value = item.DefaultBuyUnit.GuiInfo().DisplayText;
            row.Cells.Add(unitCell);

            row.Cells.Add(new DataGridViewButtonCell { Value = isInShoppingList ? REMOVE_FROM_LIST : ADD_TO_LIST });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Id });

            return row;
        }

        private IEnumerable<ShoppingListEntry> GetEntries()
        {
            return from sli in RecipeBooks.Current.ShoppingList
                   group sli by sli.ItemId into itemGroup
                   let item = itemGroup.First().Item
                   let unit = item.DefaultBuyUnit
                   select new ShoppingListEntry
                   {
                       Item = itemGroup.First().Item,
                       Quantity = itemGroup.Sum(sli => sli.Quantity.Convert(sli.Unit, unit)),
                       Unit = unit,
                       Sources = itemGroup.Select(sli => (DomainObject)sli.Recipe ?? item).ToSet()
                   };
        }

        private class ShoppingListEntry
        {
            public Item Item { get; set; }
            public double Quantity { get; set; }
            public Unit Unit { get; set; }
            public IEnumerable<DomainObject> Sources { get; set; }
        }
    }
}
