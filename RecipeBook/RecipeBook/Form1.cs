using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecipeBook.Domain;
using RecipeBook.Gui;

namespace RecipeBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.itemEditor1.SetItem(new RecipeBook.Domain.Item() { Name = "bob", Category = Domain.Category.Frozen, AssumeIsInStock = true, UnitType = Domain.UnitType.VolumeStandard, DefaultBuyUnit = Domain.Unit.Cup, DefaultRecipeUnit = Domain.Unit.FluidOunce });
            Domain.Item item;
            IList<string> msgs;
            this.itemEditor1.TryGetItem(out item, out msgs);

            var test = new Button { Text = "test" };
            test.Click += (o, e) =>
            {
                this.itemEditor1.SetReadOnly(!this.itemEditor1.ReadOnly);

                if (new Random().Next() % 2 == 0)
                {
                    this.recipeEditor1.SetReadOnly(!this.recipeEditor1.ReadOnly);
                    Recipe r; IList<string> errs;
                    this.recipeEditor1.TryGetRecipe(out r, out errs);
                    if (r != null)
                        Utils.Alert("Got recipe with " + r.Ingredients.Count + " ings ");
                }
                else
                {
                    this.recipeEditor1.CheckBoxMode = !this.recipeEditor1.CheckBoxMode;
                    Utils.Alert(this.recipeEditor1.GetIngredients().Count.ToString());
                }
            };
            this.Controls.Add(test);

            var tabs = new Tabs { Dock = DockStyle.Fill };
            this.Controls.Add(tabs);
            tabs.BringToFront();
            this.Width += 200;
            this.Height += 200;
        }
    }
}
