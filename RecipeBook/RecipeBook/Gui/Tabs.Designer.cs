namespace RecipeBook.Gui
{
    partial class Tabs
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.recipesTab = new System.Windows.Forms.TabPage();
            this.createNewRecipeButton = new System.Windows.Forms.Button();
            this.recipeSearchTextBox = new System.Windows.Forms.TextBox();
            this.recipeSearchLabel = new System.Windows.Forms.Label();
            this.recipeGrid = new System.Windows.Forms.DataGridView();
            this.recipeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteRecipeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.recipeQuantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addRecipeToListColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.recipeIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recipeEditor = new RecipeBook.Gui.RecipeEditor();
            this.itemsTab = new System.Windows.Forms.TabPage();
            this.itemEditor = new RecipeBook.Gui.ItemEditor();
            this.createNewItemButton = new System.Windows.Forms.Button();
            this.itemSearchTextBox = new System.Windows.Forms.TextBox();
            this.itemSearchLabel = new System.Windows.Forms.Label();
            this.itemGrid = new System.Windows.Forms.DataGridView();
            this.itemNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteItemColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.itemQuantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemUnitsColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.addItemToListColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.itemIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listTab = new System.Windows.Forms.TabPage();
            this.clearListButton = new System.Windows.Forms.Button();
            this.useTwoColumnsBox = new System.Windows.Forms.CheckBox();
            this.shoppingListTextBox = new System.Windows.Forms.TextBox();
            this.exportListButton = new System.Windows.Forms.Button();
            this.refreshListButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.recipesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recipeGrid)).BeginInit();
            this.itemsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemGrid)).BeginInit();
            this.listTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.recipesTab);
            this.tabControl.Controls.Add(this.itemsTab);
            this.tabControl.Controls.Add(this.listTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1301, 548);
            this.tabControl.TabIndex = 0;
            // 
            // recipesTab
            // 
            this.recipesTab.Controls.Add(this.createNewRecipeButton);
            this.recipesTab.Controls.Add(this.recipeSearchTextBox);
            this.recipesTab.Controls.Add(this.recipeSearchLabel);
            this.recipesTab.Controls.Add(this.recipeGrid);
            this.recipesTab.Controls.Add(this.recipeEditor);
            this.recipesTab.Location = new System.Drawing.Point(4, 22);
            this.recipesTab.Name = "recipesTab";
            this.recipesTab.Padding = new System.Windows.Forms.Padding(3);
            this.recipesTab.Size = new System.Drawing.Size(1293, 522);
            this.recipesTab.TabIndex = 0;
            this.recipesTab.Text = "Recipes";
            this.recipesTab.UseVisualStyleBackColor = true;
            // 
            // createNewRecipeButton
            // 
            this.createNewRecipeButton.Location = new System.Drawing.Point(340, 10);
            this.createNewRecipeButton.Name = "createNewRecipeButton";
            this.createNewRecipeButton.Size = new System.Drawing.Size(111, 23);
            this.createNewRecipeButton.TabIndex = 4;
            this.createNewRecipeButton.Text = "Create New Recipe";
            this.createNewRecipeButton.UseVisualStyleBackColor = true;
            // 
            // recipeSearchTextBox
            // 
            this.recipeSearchTextBox.Location = new System.Drawing.Point(55, 11);
            this.recipeSearchTextBox.Name = "recipeSearchTextBox";
            this.recipeSearchTextBox.Size = new System.Drawing.Size(278, 20);
            this.recipeSearchTextBox.TabIndex = 3;
            // 
            // recipeSearchLabel
            // 
            this.recipeSearchLabel.AutoSize = true;
            this.recipeSearchLabel.Location = new System.Drawing.Point(7, 11);
            this.recipeSearchLabel.Name = "recipeSearchLabel";
            this.recipeSearchLabel.Size = new System.Drawing.Size(41, 13);
            this.recipeSearchLabel.TabIndex = 2;
            this.recipeSearchLabel.Text = "Search";
            // 
            // recipeGrid
            // 
            this.recipeGrid.AllowUserToAddRows = false;
            this.recipeGrid.AllowUserToDeleteRows = false;
            this.recipeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recipeGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recipeNameColumn,
            this.deleteRecipeColumn,
            this.recipeQuantityColumn,
            this.addRecipeToListColumn,
            this.recipeIdColumn});
            this.recipeGrid.Location = new System.Drawing.Point(7, 40);
            this.recipeGrid.Name = "recipeGrid";
            this.recipeGrid.Size = new System.Drawing.Size(444, 473);
            this.recipeGrid.TabIndex = 1;
            // 
            // recipeNameColumn
            // 
            this.recipeNameColumn.HeaderText = "Recipe";
            this.recipeNameColumn.Name = "recipeNameColumn";
            this.recipeNameColumn.ReadOnly = true;
            this.recipeNameColumn.Width = 200;
            // 
            // deleteRecipeColumn
            // 
            this.deleteRecipeColumn.HeaderText = "";
            this.deleteRecipeColumn.Name = "deleteRecipeColumn";
            this.deleteRecipeColumn.Width = 50;
            // 
            // recipeQuantityColumn
            // 
            this.recipeQuantityColumn.HeaderText = "Quantity";
            this.recipeQuantityColumn.Name = "recipeQuantityColumn";
            this.recipeQuantityColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.recipeQuantityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.recipeQuantityColumn.Width = 50;
            // 
            // addRecipeToListColumn
            // 
            this.addRecipeToListColumn.HeaderText = "";
            this.addRecipeToListColumn.Name = "addRecipeToListColumn";
            // 
            // recipeIdColumn
            // 
            this.recipeIdColumn.HeaderText = "";
            this.recipeIdColumn.Name = "recipeIdColumn";
            this.recipeIdColumn.ReadOnly = true;
            this.recipeIdColumn.Visible = false;
            // 
            // recipeEditor
            // 
            this.recipeEditor.CheckBoxMode = false;
            this.recipeEditor.Location = new System.Drawing.Point(473, 7);
            this.recipeEditor.Name = "recipeEditor";
            this.recipeEditor.ReadOnly = true;
            this.recipeEditor.ShowSaveAsEditWhenReadOnly = true;
            this.recipeEditor.Size = new System.Drawing.Size(814, 509);
            this.recipeEditor.TabIndex = 0;
            // 
            // itemsTab
            // 
            this.itemsTab.Controls.Add(this.itemEditor);
            this.itemsTab.Controls.Add(this.createNewItemButton);
            this.itemsTab.Controls.Add(this.itemSearchTextBox);
            this.itemsTab.Controls.Add(this.itemSearchLabel);
            this.itemsTab.Controls.Add(this.itemGrid);
            this.itemsTab.Location = new System.Drawing.Point(4, 22);
            this.itemsTab.Name = "itemsTab";
            this.itemsTab.Padding = new System.Windows.Forms.Padding(3);
            this.itemsTab.Size = new System.Drawing.Size(1293, 522);
            this.itemsTab.TabIndex = 1;
            this.itemsTab.Text = "Items";
            this.itemsTab.UseVisualStyleBackColor = true;
            // 
            // itemEditor
            // 
            this.itemEditor.Location = new System.Drawing.Point(778, 133);
            this.itemEditor.Name = "itemEditor";
            this.itemEditor.ReadOnly = true;
            this.itemEditor.ShowSaveAsEditWhenReadOnly = true;
            this.itemEditor.Size = new System.Drawing.Size(301, 218);
            this.itemEditor.TabIndex = 9;
            // 
            // createNewItemButton
            // 
            this.createNewItemButton.Location = new System.Drawing.Point(340, 10);
            this.createNewItemButton.Name = "createNewItemButton";
            this.createNewItemButton.Size = new System.Drawing.Size(111, 23);
            this.createNewItemButton.TabIndex = 8;
            this.createNewItemButton.Text = "Create New Item";
            this.createNewItemButton.UseVisualStyleBackColor = true;
            // 
            // itemSearchTextBox
            // 
            this.itemSearchTextBox.Location = new System.Drawing.Point(55, 11);
            this.itemSearchTextBox.Name = "itemSearchTextBox";
            this.itemSearchTextBox.Size = new System.Drawing.Size(278, 20);
            this.itemSearchTextBox.TabIndex = 7;
            // 
            // itemSearchLabel
            // 
            this.itemSearchLabel.AutoSize = true;
            this.itemSearchLabel.Location = new System.Drawing.Point(7, 11);
            this.itemSearchLabel.Name = "itemSearchLabel";
            this.itemSearchLabel.Size = new System.Drawing.Size(41, 13);
            this.itemSearchLabel.TabIndex = 6;
            this.itemSearchLabel.Text = "Search";
            // 
            // itemGrid
            // 
            this.itemGrid.AllowUserToAddRows = false;
            this.itemGrid.AllowUserToDeleteRows = false;
            this.itemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemNameColumn,
            this.deleteItemColumn,
            this.itemQuantityColumn,
            this.itemUnitsColumn,
            this.addItemToListColumn,
            this.itemIdColumn});
            this.itemGrid.Location = new System.Drawing.Point(6, 41);
            this.itemGrid.Name = "itemGrid";
            this.itemGrid.Size = new System.Drawing.Size(543, 473);
            this.itemGrid.TabIndex = 5;
            // 
            // itemNameColumn
            // 
            this.itemNameColumn.HeaderText = "Item";
            this.itemNameColumn.Name = "itemNameColumn";
            this.itemNameColumn.ReadOnly = true;
            this.itemNameColumn.Width = 200;
            // 
            // deleteItemColumn
            // 
            this.deleteItemColumn.HeaderText = "";
            this.deleteItemColumn.Name = "deleteItemColumn";
            this.deleteItemColumn.Width = 50;
            // 
            // itemQuantityColumn
            // 
            this.itemQuantityColumn.HeaderText = "Quantity";
            this.itemQuantityColumn.Name = "itemQuantityColumn";
            this.itemQuantityColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.itemQuantityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.itemQuantityColumn.Width = 50;
            // 
            // itemUnitsColumn
            // 
            this.itemUnitsColumn.HeaderText = "Units";
            this.itemUnitsColumn.Name = "itemUnitsColumn";
            // 
            // addItemToListColumn
            // 
            this.addItemToListColumn.HeaderText = "";
            this.addItemToListColumn.Name = "addItemToListColumn";
            // 
            // itemIdColumn
            // 
            this.itemIdColumn.HeaderText = "";
            this.itemIdColumn.Name = "itemIdColumn";
            this.itemIdColumn.ReadOnly = true;
            this.itemIdColumn.Visible = false;
            // 
            // listTab
            // 
            this.listTab.Controls.Add(this.clearListButton);
            this.listTab.Controls.Add(this.useTwoColumnsBox);
            this.listTab.Controls.Add(this.shoppingListTextBox);
            this.listTab.Controls.Add(this.exportListButton);
            this.listTab.Controls.Add(this.refreshListButton);
            this.listTab.Location = new System.Drawing.Point(4, 22);
            this.listTab.Name = "listTab";
            this.listTab.Padding = new System.Windows.Forms.Padding(3);
            this.listTab.Size = new System.Drawing.Size(1293, 522);
            this.listTab.TabIndex = 2;
            this.listTab.Text = "Shopping List";
            this.listTab.UseVisualStyleBackColor = true;
            // 
            // clearListButton
            // 
            this.clearListButton.Location = new System.Drawing.Point(6, 493);
            this.clearListButton.Name = "clearListButton";
            this.clearListButton.Size = new System.Drawing.Size(154, 23);
            this.clearListButton.TabIndex = 4;
            this.clearListButton.Text = "Clear List";
            this.clearListButton.UseVisualStyleBackColor = true;
            // 
            // useTwoColumnsBox
            // 
            this.useTwoColumnsBox.AutoSize = true;
            this.useTwoColumnsBox.Checked = true;
            this.useTwoColumnsBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useTwoColumnsBox.Location = new System.Drawing.Point(6, 65);
            this.useTwoColumnsBox.Name = "useTwoColumnsBox";
            this.useTwoColumnsBox.Size = new System.Drawing.Size(154, 17);
            this.useTwoColumnsBox.TabIndex = 3;
            this.useTwoColumnsBox.Text = "Export List In Two Columns";
            this.useTwoColumnsBox.UseVisualStyleBackColor = true;
            // 
            // shoppingListTextBox
            // 
            this.shoppingListTextBox.Location = new System.Drawing.Point(166, 6);
            this.shoppingListTextBox.Multiline = true;
            this.shoppingListTextBox.Name = "shoppingListTextBox";
            this.shoppingListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.shoppingListTextBox.Size = new System.Drawing.Size(1121, 510);
            this.shoppingListTextBox.TabIndex = 2;
            // 
            // exportListButton
            // 
            this.exportListButton.Location = new System.Drawing.Point(6, 35);
            this.exportListButton.Name = "exportListButton";
            this.exportListButton.Size = new System.Drawing.Size(154, 23);
            this.exportListButton.TabIndex = 1;
            this.exportListButton.Text = "Open List in Text Editor";
            this.exportListButton.UseVisualStyleBackColor = true;
            // 
            // refreshListButton
            // 
            this.refreshListButton.Location = new System.Drawing.Point(6, 6);
            this.refreshListButton.Name = "refreshListButton";
            this.refreshListButton.Size = new System.Drawing.Size(154, 23);
            this.refreshListButton.TabIndex = 0;
            this.refreshListButton.Text = "Refresh List";
            this.refreshListButton.UseVisualStyleBackColor = true;
            // 
            // Tabs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "Tabs";
            this.Size = new System.Drawing.Size(1301, 548);
            this.tabControl.ResumeLayout(false);
            this.recipesTab.ResumeLayout(false);
            this.recipesTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recipeGrid)).EndInit();
            this.itemsTab.ResumeLayout(false);
            this.itemsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemGrid)).EndInit();
            this.listTab.ResumeLayout(false);
            this.listTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage recipesTab;
        private System.Windows.Forms.TabPage itemsTab;
        private System.Windows.Forms.TabPage listTab;
        private System.Windows.Forms.DataGridView recipeGrid;
        private RecipeEditor recipeEditor;
        private System.Windows.Forms.Label recipeSearchLabel;
        private System.Windows.Forms.Button createNewRecipeButton;
        private System.Windows.Forms.TextBox recipeSearchTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn recipeNameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn deleteRecipeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn recipeQuantityColumn;
        private System.Windows.Forms.DataGridViewButtonColumn addRecipeToListColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn recipeIdColumn;
        private System.Windows.Forms.Button createNewItemButton;
        private System.Windows.Forms.TextBox itemSearchTextBox;
        private System.Windows.Forms.Label itemSearchLabel;
        private System.Windows.Forms.DataGridView itemGrid;
        private ItemEditor itemEditor;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn deleteItemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemQuantityColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn itemUnitsColumn;
        private System.Windows.Forms.DataGridViewButtonColumn addItemToListColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemIdColumn;
        private System.Windows.Forms.Button refreshListButton;
        private System.Windows.Forms.Button exportListButton;
        private System.Windows.Forms.TextBox shoppingListTextBox;
        private System.Windows.Forms.CheckBox useTwoColumnsBox;
        private System.Windows.Forms.Button clearListButton;
    }
}
