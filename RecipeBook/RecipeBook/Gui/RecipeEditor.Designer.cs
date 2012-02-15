namespace RecipeBook.Gui
{
    partial class RecipeEditor
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.itemNameComboBox = new System.Windows.Forms.ComboBox();
            this.ingredientsLabel = new System.Windows.Forms.Label();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.quantityComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.ingredientsGrid = new System.Windows.Forms.DataGridView();
            this.itemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitsColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RemoveButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.createNewItemButton = new System.Windows.Forms.Button();
            this.stepsLabel = new System.Windows.Forms.Label();
            this.notesLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.stepsTextBox = new System.Windows.Forms.TextBox();
            this.unitsDropDown = new System.Windows.Forms.ComboBox();
            this.unitsLabel = new System.Windows.Forms.Label();
            this.checkBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ingredientsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(4, 4);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(80, 4);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(231, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(4, 41);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(41, 13);
            this.sourceLabel.TabIndex = 2;
            this.sourceLabel.Text = "Source";
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.Location = new System.Drawing.Point(80, 41);
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.Size = new System.Drawing.Size(231, 20);
            this.sourceTextBox.TabIndex = 3;
            // 
            // itemNameComboBox
            // 
            this.itemNameComboBox.FormattingEnabled = true;
            this.itemNameComboBox.Location = new System.Drawing.Point(10, 108);
            this.itemNameComboBox.Name = "itemNameComboBox";
            this.itemNameComboBox.Size = new System.Drawing.Size(158, 21);
            this.itemNameComboBox.TabIndex = 4;
            // 
            // ingredientsLabel
            // 
            this.ingredientsLabel.AutoSize = true;
            this.ingredientsLabel.Location = new System.Drawing.Point(7, 82);
            this.ingredientsLabel.Name = "ingredientsLabel";
            this.ingredientsLabel.Size = new System.Drawing.Size(59, 13);
            this.ingredientsLabel.TabIndex = 5;
            this.ingredientsLabel.Text = "Ingredients";
            // 
            // quantityLabel
            // 
            this.quantityLabel.AutoSize = true;
            this.quantityLabel.Location = new System.Drawing.Point(174, 83);
            this.quantityLabel.Name = "quantityLabel";
            this.quantityLabel.Size = new System.Drawing.Size(46, 13);
            this.quantityLabel.TabIndex = 6;
            this.quantityLabel.Text = "Quantity";
            // 
            // quantityComboBox
            // 
            this.quantityComboBox.FormattingEnabled = true;
            this.quantityComboBox.Location = new System.Drawing.Point(177, 108);
            this.quantityComboBox.Name = "quantityComboBox";
            this.quantityComboBox.Size = new System.Drawing.Size(57, 21);
            this.quantityComboBox.TabIndex = 7;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(336, 106);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(76, 23);
            this.addButton.TabIndex = 8;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // ingredientsGrid
            // 
            this.ingredientsGrid.AllowUserToAddRows = false;
            this.ingredientsGrid.AllowUserToDeleteRows = false;
            this.ingredientsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ingredientsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemColumn,
            this.QuantityColumn,
            this.UnitsColumn,
            this.RemoveButtonColumn,
            this.checkBoxColumn});
            this.ingredientsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ingredientsGrid.Location = new System.Drawing.Point(10, 136);
            this.ingredientsGrid.Name = "ingredientsGrid";
            this.ingredientsGrid.Size = new System.Drawing.Size(402, 340);
            this.ingredientsGrid.TabIndex = 9;
            // 
            // itemColumn
            // 
            this.itemColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.itemColumn.HeaderText = "Item";
            this.itemColumn.Name = "itemColumn";
            this.itemColumn.ReadOnly = true;
            // 
            // QuantityColumn
            // 
            this.QuantityColumn.HeaderText = "Quantity";
            this.QuantityColumn.Name = "QuantityColumn";
            this.QuantityColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.QuantityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QuantityColumn.Width = 57;
            // 
            // UnitsColumn
            // 
            this.UnitsColumn.HeaderText = "Units";
            this.UnitsColumn.Name = "UnitsColumn";
            this.UnitsColumn.Width = 90;
            // 
            // RemoveButtonColumn
            // 
            this.RemoveButtonColumn.HeaderText = "";
            this.RemoveButtonColumn.Name = "RemoveButtonColumn";
            this.RemoveButtonColumn.Width = 76;
            // 
            // createNewItemButton
            // 
            this.createNewItemButton.Location = new System.Drawing.Point(310, 482);
            this.createNewItemButton.Name = "createNewItemButton";
            this.createNewItemButton.Size = new System.Drawing.Size(102, 23);
            this.createNewItemButton.TabIndex = 10;
            this.createNewItemButton.Text = "Create New Item";
            this.createNewItemButton.UseVisualStyleBackColor = true;
            // 
            // stepsLabel
            // 
            this.stepsLabel.AutoSize = true;
            this.stepsLabel.Location = new System.Drawing.Point(432, 4);
            this.stepsLabel.Name = "stepsLabel";
            this.stepsLabel.Size = new System.Drawing.Size(34, 13);
            this.stepsLabel.TabIndex = 11;
            this.stepsLabel.Text = "Steps";
            // 
            // notesLabel
            // 
            this.notesLabel.AutoSize = true;
            this.notesLabel.Location = new System.Drawing.Point(432, 347);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(35, 13);
            this.notesLabel.TabIndex = 12;
            this.notesLabel.Text = "Notes";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(708, 482);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(102, 23);
            this.saveButton.TabIndex = 13;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(600, 482);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(102, 23);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // notesTextBox
            // 
            this.notesTextBox.Location = new System.Drawing.Point(474, 347);
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.Size = new System.Drawing.Size(336, 129);
            this.notesTextBox.TabIndex = 15;
            // 
            // stepsTextBox
            // 
            this.stepsTextBox.Location = new System.Drawing.Point(474, 4);
            this.stepsTextBox.Multiline = true;
            this.stepsTextBox.Name = "stepsTextBox";
            this.stepsTextBox.Size = new System.Drawing.Size(334, 328);
            this.stepsTextBox.TabIndex = 16;
            // 
            // unitsDropDown
            // 
            this.unitsDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitsDropDown.FormattingEnabled = true;
            this.unitsDropDown.Location = new System.Drawing.Point(240, 108);
            this.unitsDropDown.Name = "unitsDropDown";
            this.unitsDropDown.Size = new System.Drawing.Size(90, 21);
            this.unitsDropDown.TabIndex = 17;
            // 
            // unitsLabel
            // 
            this.unitsLabel.AutoSize = true;
            this.unitsLabel.Location = new System.Drawing.Point(237, 83);
            this.unitsLabel.Name = "unitsLabel";
            this.unitsLabel.Size = new System.Drawing.Size(31, 13);
            this.unitsLabel.TabIndex = 18;
            this.unitsLabel.Text = "Units";
            // 
            // checkBoxColumn
            // 
            this.checkBoxColumn.HeaderText = "Add to List";
            this.checkBoxColumn.Name = "checkBoxColumn";
            this.checkBoxColumn.Visible = false;
            // 
            // RecipeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.unitsLabel);
            this.Controls.Add(this.unitsDropDown);
            this.Controls.Add(this.stepsTextBox);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.notesLabel);
            this.Controls.Add(this.stepsLabel);
            this.Controls.Add(this.createNewItemButton);
            this.Controls.Add(this.ingredientsGrid);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.quantityComboBox);
            this.Controls.Add(this.quantityLabel);
            this.Controls.Add(this.ingredientsLabel);
            this.Controls.Add(this.itemNameComboBox);
            this.Controls.Add(this.sourceTextBox);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Name = "RecipeEditor";
            this.Size = new System.Drawing.Size(814, 509);
            ((System.ComponentModel.ISupportInitialize)(this.ingredientsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.TextBox sourceTextBox;
        private System.Windows.Forms.ComboBox itemNameComboBox;
        private System.Windows.Forms.Label ingredientsLabel;
        private System.Windows.Forms.Label quantityLabel;
        private System.Windows.Forms.ComboBox quantityComboBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridView ingredientsGrid;
        private System.Windows.Forms.Button createNewItemButton;
        private System.Windows.Forms.Label stepsLabel;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox notesTextBox;
        private System.Windows.Forms.TextBox stepsTextBox;
        private System.Windows.Forms.ComboBox unitsDropDown;
        private System.Windows.Forms.Label unitsLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn UnitsColumn;
        private System.Windows.Forms.DataGridViewButtonColumn RemoveButtonColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkBoxColumn;
    }
}
