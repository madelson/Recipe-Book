using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecipeBook.Domain;
using RecipeBook.Data;

namespace RecipeBook.Gui
{
    public partial class RecipeEditor : EditorControl<RecipeEditor, Recipe>
    {
        private Recipe recipe;
        private bool readOnly = false, checkBoxMode = false, showSaveAsEditWhenReadOnly = false;

        public bool ShowSaveAsEditWhenReadOnly
        {
            get { return this.showSaveAsEditWhenReadOnly; }
            set
            {
                this.showSaveAsEditWhenReadOnly = value;
                this.ReadOnly = this.ReadOnly;
            }
        }

        public bool ReadOnly
        {
            get { return this.readOnly; }
            set
            {
                if (this.ShowSaveAsEditWhenReadOnly)
                {
                    this.saveButton.Text = value ? "Edit" : "Save";
                    this.saveButton.Visible = this.recipe != null;
                }
                this.Controls.OfType<Button>()
                    .Where(b => !this.ShowSaveAsEditWhenReadOnly || this.recipe == null || b != this.saveButton)
                    .Concat(new Control[] { this.itemNameComboBox, this.quantityComboBox, this.quantityLabel, this.unitsDropDown, this.unitsLabel })
                    .ForEach(c => c.Visible = !value);
                this.Controls.OfType<TextBox>().ForEach(tb => tb.ReadOnly = value);
                this.RemoveButtonColumn.Visible = !value;
                this.ingredientsGrid.Columns
                    .Cast<DataGridViewColumn>()
                    .Where(c => c != this.checkBoxColumn) // for some reason this is necessary for checkbox mode to work
                    .ForEach(c => c.ReadOnly = value);
                this.readOnly = value;
            }
        }

        public bool CheckBoxMode
        {
            get { return this.checkBoxMode; }
            set
            {
                this.ReadOnly = value;
                this.checkBoxColumn.Visible = value;
                if (value)
                {
                    this.ShowSaveAsEditWhenReadOnly = false;
                    this.saveButton.Visible = true;
                    this.saveButton.Text = "Submit";
                }

                this.checkBoxMode = value;                
            }
        }

        public RecipeEditor()
        {
            InitializeComponent();

            Dictionary<string, Item> itemsByName;
            if (RecipeBooks.Current != null)
            {
                itemsByName = RecipeBooks.Current.Items.ToDictionary(i => i.Name, i => i, Utils.CaseInsensitiveComparer);
            }
            else
            {
                itemsByName = new Dictionary<string, Item>();
            }
            this.itemNameComboBox.Items.AddRange(itemsByName.Keys.ToArray());
            this.itemNameComboBox.AddAutoComplete(o => o.ToString());
            this.itemNameComboBox.SelectedIndexChanged += (o, e) =>
            {
                if (this.itemNameComboBox.SelectedIndex >= 0)
                {
                    var item = itemsByName[this.itemNameComboBox.SelectedItem.ToString()];
                    if (this.unitsDropDown.Items.Count == 0 ||
                        ((Unit)this.unitsDropDown.Items[0]).Info().UnitType != item.UnitType)
                    {
                        this.unitsDropDown.Items.Clear();
                        var units = item.UnitType.Units().Cast<object>().ToArray();
                        this.unitsDropDown.Items.AddRange(units);
                        this.unitsDropDown.SelectedIndex = units.IndexWhere(u => u.Equals(item.DefaultRecipeUnit));
                    }
                }
            };

            this.quantityComboBox.Items.AddRange(Constants.DefaultQuantities.Cast<object>().ToArray());
            this.quantityComboBox.SelectedIndex = Constants.DefaultQuantities.IndexWhere(o => o.Equals(1));

            this.addButton.Click += (o, e) =>
            {
                if (!itemsByName.ContainsKey(this.itemNameComboBox.Text))
                {
                    Utils.Alert(string.IsNullOrWhiteSpace(this.itemNameComboBox.Text) 
                        ? "No item selected!"
                        : string.Format("There is no item named '{0}'!", this.itemNameComboBox.Text));
                    this.itemNameComboBox.SelectAndFocus();
                    return;
                }
                else if (this.ingredientsGrid.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => Utils.CaseInsensitiveComparer.Equals(this.itemNameComboBox.Text, r.Cells[this.itemColumn.Index].Value.ToString())) != null)
                {
                    Utils.Alert(string.Format("{0} is already in the recipe!", this.itemNameComboBox.Text));
                    this.itemNameComboBox.SelectAndFocus();
                    return;
                }

                double quantity;
                if (!double.TryParse(this.quantityComboBox.Text, out quantity) ||
                    quantity <= 0)
                {
                    Utils.Alert(string.Format("'{0}' is not a valid value for quantity!", this.quantityComboBox.Text));
                    this.quantityComboBox.SelectAndFocus();
                    return;
                }

                if (this.unitsDropDown.SelectedIndex < 0)
                {
                    Utils.Alert("Please select a unit!");
                    return;
                }

                this.AddIngredient(RecipeBooks.Current.Items.First(i => i.Name == this.itemNameComboBox.Text), quantity, (Unit)this.unitsDropDown.SelectedItem);
                this.itemNameComboBox.SelectAndFocus();
            };

            this.RemoveButtonColumn.CellClicked((g, e) => g.Rows.RemoveAt(e.RowIndex));

            this.QuantityColumn.CellValidating((o, e) =>
            {
                double value;
                if (!double.TryParse(e.FormattedValue.ToString(), out value) || value <= 0)
                {
                    Utils.Alert(string.Format("'{0}' is not a valid value for quantity!", e.FormattedValue));
                    e.Cancel = true;
                }
            });

            this.createNewItemButton.Click += (o, e) =>
            {
                Item newItem = Dialogs.Edit<ItemEditor, Item>(allowOverwrite: false);
                if (newItem != null)
                {
                    itemsByName[newItem.Name] = newItem;
                    this.itemNameComboBox.Items.Add(newItem.Name);
                    this.itemNameComboBox.SelectedIndex = this.itemNameComboBox.Items.Count - 1;
                    this.quantityComboBox.SelectedIndex = this.quantityComboBox.Items
                        .Cast<double>()
                        .IndexWhere(d => d == 1);
                }
            };

            this.saveButton.Click += (o, e) =>
            {
                if (this.ShowSaveAsEditWhenReadOnly && this.ReadOnly)
                {
                    if (RecipeBooks.Current.IsInShoppingList(this.recipe))
                    {
                        Utils.Alert("A recipe cannot be edited while it is in the shopping list. Remove it from the shopping list first!");
                    }
                    else
                    {
                        this.ReadOnly = false;
                    }
                }
                else
                {
                    this.RaiseSaveButtonClicked(this);
                }
            };

            this.cancelButton.Click += (o, e) =>
            {
                if (this.ShowSaveAsEditWhenReadOnly)
                {
                    this.ReadOnly = true;
                    this.SetRecipe(this.recipe);
                }
                else
                {
                    this.RaiseCancelButtonClicked(this);
                }
            };
        }

        public void SetRecipe(Recipe recipe)
        {
            this.itemNameComboBox.SelectedIndex = this.unitsDropDown.SelectedIndex = -1;
            this.quantityComboBox.SelectedIndex = Constants.DefaultQuantities.IndexWhere(o => o.Equals(1));
            this.ingredientsGrid.Rows.Clear();

            if (recipe == null)
            {
                this.nameTextBox.Text = this.sourceTextBox.Text = this.stepsTextBox.Text = this.notesTextBox.Text = string.Empty;
            }
            else
            {
                this.nameTextBox.Text = recipe.Name;
                this.sourceTextBox.Text = recipe.Source;
                this.stepsTextBox.Text = recipe.Steps;
                this.notesTextBox.Text = recipe.Notes;
                recipe.Ingredients.ForEach(i => this.AddIngredient(recipe.RecipeBook.Get<Item>(i.ItemId), i.Quantity, i.Unit));
            }

            this.recipe = recipe;

            if (this.ShowSaveAsEditWhenReadOnly)
            {
                this.ReadOnly = true;
            }
        }

        public bool TryGetRecipe(out Recipe recipe, out IList<string> errorMessages)
        {
            errorMessages = new List<string>();

            var name = this.nameTextBox.Text;
            if (string.IsNullOrEmpty(name))
            {
                errorMessages.Add("Please enter a name");
            }

            if (errorMessages.Count > 0) 
            {
                recipe = null;
                return false;
            }

            var ingredients = this.GetIngredients(onlySelected: false);

            recipe = this.recipe == null ? (this.recipe = new Recipe { Id = Guid.NewGuid() }) : this.recipe;
            recipe.Name = name;
            recipe.Source = this.sourceTextBox.Text;
            recipe.Steps = this.stepsTextBox.Text;
            recipe.Notes = this.notesTextBox.Text;
            recipe.Ingredients = ingredients;

            return true;
        }

        public List<Ingredient> GetIngredients(bool onlySelected = true)
        {
            var selectedIngredients = (from r in this.ingredientsGrid.Rows.Cast<DataGridViewRow>()
                                      join i in RecipeBooks.Current.Items on
                                        r.Cells[this.itemColumn.Index].Value.ToString().ToLower()
                                        equals
                                        i.Name.ToLower()
                                      where !onlySelected || (bool)r.Cells[this.checkBoxColumn.Index].Value
                                      select new Ingredient
                                      {
                                          ItemId = i.Id,
                                          Quantity = double.Parse(r.Cells[this.QuantityColumn.Index].Value.ToString()),
                                          Unit = UnitUtils.ParseUnit(r.Cells[this.UnitsColumn.Index].Value.ToString())
                                      })
                                      .ToList();

            return selectedIngredients;
        }

        private void AddIngredient(Item item, double quantity, Unit units)
        {
            var row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Name });

            row.Cells.Add(new DataGridViewTextBoxCell { Value = quantity });

            var unitCell = new DataGridViewComboBoxCell { DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing };
            unitCell.Items.AddRange(item.UnitType.Units().Select(u => u.ToString()).ToArray());
            unitCell.Value = unitCell.Items.Cast<string>().First(s => s == units.ToString());
            row.Cells.Add(unitCell);

            row.Cells.Add(new DataGridViewButtonCell { Value = "Remove" });
            row.Cells.Add(new DataGridViewCheckBoxCell { FalseValue = false, TrueValue = true, Value = !item.AssumeIsInStock });

            this.ingredientsGrid.Rows.Add(row);
        }

        public override void SetDomainObject(Recipe domainObject)
        {
            this.SetRecipe(recipe);
        }

        public override bool TryGetDomainObject(out Recipe domainObject, out IList<string> errorMessages)
        {
            return this.TryGetRecipe(out domainObject, out errorMessages);
        }
    }
}
