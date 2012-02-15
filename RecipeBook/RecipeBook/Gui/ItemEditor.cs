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
    public partial class ItemEditor : EditorControl<ItemEditor, Item>
    {
        private Item item;
        private bool readOnly, showSaveAsEditWhenReadOnly = false;

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
                    this.saveButton.Visible = this.cancelButton.Visible = this.item != null;
                }
                else
                {
                    this.saveButton.Visible = this.cancelButton.Visible = !value;
                }
                
                this.Controls
                    .Cast<Control>()
                    .Where(c => !(c is Button))
                    .ForEach(c => c.SetReadOnly(value));

                this.readOnly = value;
            }
        }

        public ItemEditor()
        {
            InitializeComponent();

            var x = Enum.GetValues(typeof(Category)).Cast<Category>().ToArray();

            this.categoryDropDown.Items.AddRange(Enum.GetValues(typeof(Category))
                .Cast<Category>()
                .Select(c => c.DisplayPointer(c.GuiInfo().DisplayText))
                .ToArray());

            this.unitsDropDown.Items.AddRange(Enum.GetValues(typeof(UnitType))
                .Cast<UnitType>()
                .Select(ut => ut.DisplayPointer(ut.GuiInfo().DisplayText))
                .ToArray());

            this.unitsDropDown.SelectedIndexChanged += (o, e) =>
            {
                if (this.unitsDropDown.SelectedIndex < 0)
                {
                    this.defaultUnitInRecipesDropDown.SelectedIndex = -1;
                    this.defaultUnitInListDropDown.SelectedIndex = -1;
                    return;
                }

                var unitType = ((DisplayPointer<UnitType>)this.unitsDropDown.SelectedItem).Item;

                if (this.defaultUnitInRecipesDropDown.Items.Count > 0 &&
                    ((DisplayPointer<UnitInfoAttribute>)this.defaultUnitInRecipesDropDown.Items[0]).Item.UnitType == unitType)
                {
                    return;
                }

                var units = unitType
                    .UnitInfos()
                    .Select(ui => ui.DisplayPointer(ui.Target.GuiInfo().DisplayText))
                    .ToArray();

                this.defaultUnitInRecipesDropDown.Items.Clear();
                this.defaultUnitInRecipesDropDown.Items.AddRange(units);
                this.defaultUnitInRecipesDropDown.SelectedIndex = units.IndexWhere(u => u.Item.IsRecipeDefault);

                this.defaultUnitInListDropDown.Items.Clear();
                this.defaultUnitInListDropDown.Items.AddRange(units);
                this.defaultUnitInListDropDown.SelectedIndex = units.IndexWhere(u => u.Item.IsListDefault);
            };

            this.saveButton.Click += (o, e) =>
            {
                if (this.ShowSaveAsEditWhenReadOnly && this.ReadOnly)
                {
                    if (RecipeBooks.Current.IsInShoppingList(this.item))
                    {
                        Utils.Alert("An item cannot be edited while it is in the shopping list (it may have been added as part of a recipe). Remove it from the shopping list first!");
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
                    this.SetItem(this.item);
                }
                else
                {
                    this.RaiseCancelButtonClicked(this);
                }
            };
        }

        public void SetItem(Item item)
        {
            if (item == null)
            {
                this.nameTextBox.Text = string.Empty;
                this.categoryDropDown.SelectedIndex = -1;
                this.unitsDropDown.SelectedIndex = -1;
                this.assumeIsInStockCheckBox.Checked = false;
            }
            else
            {
                this.nameTextBox.Text = item.Name;
                this.categoryDropDown.SelectedIndex = Enum.GetValues(typeof(Category))
                    .Cast<Category>()
                    .IndexWhere(c => c == item.Category);
                this.unitsDropDown.SelectedIndex = Enum.GetValues(typeof(UnitType))
                    .Cast<UnitType>()
                    .IndexWhere(ut => ut == item.UnitType);

                var units = item.UnitType.Units();
                this.defaultUnitInRecipesDropDown.SelectedIndex = units.IndexWhere(u => u == item.DefaultRecipeUnit);
                this.defaultUnitInListDropDown.SelectedIndex = units.IndexWhere(u => u == item.DefaultBuyUnit);

                this.assumeIsInStockCheckBox.Checked = item.AssumeIsInStock;
            }

            this.item = item;

            if (this.ShowSaveAsEditWhenReadOnly)
            {
                this.ReadOnly = true;
            }
        }

        public bool TryGetItem(out Item item, out IList<string> errorMessages)
        {
            errorMessages = new List<string>();

            var name = this.nameTextBox.Text;
            if (string.IsNullOrEmpty(name))
            {
                errorMessages.Add("Please enter a name");
            }

            var category = this.categoryDropDown.SelectedIndex < 0
                ? null
                : (Category?)((DisplayPointer<Category>)this.categoryDropDown.SelectedItem).Item;
            if (category == null)
            {
                errorMessages.Add("Please select a category");
            }

            var unitType = this.unitsDropDown.SelectedIndex < 0
                ? null
                : (UnitType?)((DisplayPointer<UnitType>)this.unitsDropDown.SelectedItem).Item;
            Unit? defaultInRecipeUnit = null, defaultInListUnit = null;
            if (unitType == null)
            {
                errorMessages.Add("Please select units");
            }
            else
            {
                var validUnits = unitType.Value.Units().ToSet();
                var invalidatedRecipes = this.item == null || this.item.RecipeBook == null
                    ? null
                    : this.item.RecipeBook.Recipes
                        .Where(r => r.Ingredients.Any(i => i.ItemId == this.item.Id && !validUnits.Contains(i.Unit)))
                        .ToArray();
                if (!invalidatedRecipes.IsNullOrEmpty())
                {
                    errorMessages.Add(string.Format("Cannot change the units from {0} to {1}, because ({2}) recipe(s) ({3}) have {4} as an ingredient with the original units",
                        this.item.UnitType.GuiInfo().DisplayText, 
                        unitType.Value.GuiInfo().DisplayText, 
                        invalidatedRecipes.Length, 
                        string.Join(", ", invalidatedRecipes.Select(r => r.Name)), 
                        name));
                }
                else
                {
                    defaultInRecipeUnit = this.defaultUnitInRecipesDropDown.SelectedIndex < 0
                        ? null
                        : (Unit?)((DisplayPointer<UnitInfoAttribute>)this.defaultUnitInRecipesDropDown.SelectedItem).Item.Target;

                    defaultInListUnit = this.defaultUnitInListDropDown.SelectedIndex < 0
                        ? null
                        : (Unit?)((DisplayPointer<UnitInfoAttribute>)this.defaultUnitInListDropDown.SelectedItem).Item.Target;
                }
            }

            var assumeIsInStock = this.assumeIsInStockCheckBox.Checked;

            if (errorMessages.Count > 0)
            {
                item = null;
                return false;
            }

            item = this.item == null ? (this.item = new Item() { Id = Guid.NewGuid() }) : this.item;
            item.Name = name;
            item.Category = category.Value;
            item.UnitType = unitType.Value;
            item.DefaultRecipeUnit = defaultInRecipeUnit.Value;
            item.DefaultBuyUnit = defaultInListUnit.Value;
            item.AssumeIsInStock = assumeIsInStock;

            return true;
        }

        public override void SetDomainObject(Item domainObject)
        {
            this.SetItem(domainObject);
        }

        public override bool TryGetDomainObject(out Item domainObject, out IList<string> errorMessages)
        {
            return this.TryGetItem(out domainObject, out errorMessages);
        }
    }
}
