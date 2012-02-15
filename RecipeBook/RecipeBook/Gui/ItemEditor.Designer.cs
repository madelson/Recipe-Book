namespace RecipeBook.Gui
{
    partial class ItemEditor
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
            this.categoryLabel = new System.Windows.Forms.Label();
            this.categoryDropDown = new System.Windows.Forms.ComboBox();
            this.unitLabel = new System.Windows.Forms.Label();
            this.unitsDropDown = new System.Windows.Forms.ComboBox();
            this.defaultUnitLabel = new System.Windows.Forms.Label();
            this.defaultUnitInRecipesLabel = new System.Windows.Forms.Label();
            this.defaultUnitInListLabel = new System.Windows.Forms.Label();
            this.defaultUnitInRecipesDropDown = new System.Windows.Forms.ComboBox();
            this.defaultUnitInListDropDown = new System.Windows.Forms.ComboBox();
            this.assumeIsInStockCheckBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 7);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(59, 7);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(232, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(3, 36);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(49, 13);
            this.categoryLabel.TabIndex = 2;
            this.categoryLabel.Text = "Category";
            // 
            // categoryDropDown
            // 
            this.categoryDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.Location = new System.Drawing.Point(59, 36);
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.Size = new System.Drawing.Size(232, 21);
            this.categoryDropDown.TabIndex = 3;
            // 
            // unitLabel
            // 
            this.unitLabel.AutoSize = true;
            this.unitLabel.Location = new System.Drawing.Point(3, 67);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(31, 13);
            this.unitLabel.TabIndex = 4;
            this.unitLabel.Text = "Units";
            // 
            // unitsDropDown
            // 
            this.unitsDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitsDropDown.FormattingEnabled = true;
            this.unitsDropDown.Location = new System.Drawing.Point(59, 67);
            this.unitsDropDown.Name = "unitsDropDown";
            this.unitsDropDown.Size = new System.Drawing.Size(232, 21);
            this.unitsDropDown.TabIndex = 5;
            // 
            // defaultUnitLabel
            // 
            this.defaultUnitLabel.AutoSize = true;
            this.defaultUnitLabel.Location = new System.Drawing.Point(3, 101);
            this.defaultUnitLabel.Name = "defaultUnitLabel";
            this.defaultUnitLabel.Size = new System.Drawing.Size(63, 13);
            this.defaultUnitLabel.TabIndex = 6;
            this.defaultUnitLabel.Text = "Default Unit";
            // 
            // defaultUnitInRecipesLabel
            // 
            this.defaultUnitInRecipesLabel.AutoSize = true;
            this.defaultUnitInRecipesLabel.Location = new System.Drawing.Point(73, 101);
            this.defaultUnitInRecipesLabel.Name = "defaultUnitInRecipesLabel";
            this.defaultUnitInRecipesLabel.Size = new System.Drawing.Size(69, 13);
            this.defaultUnitInRecipesLabel.TabIndex = 7;
            this.defaultUnitInRecipesLabel.Text = "... in Recipes";
            // 
            // defaultUnitInListLabel
            // 
            this.defaultUnitInListLabel.AutoSize = true;
            this.defaultUnitInListLabel.Location = new System.Drawing.Point(73, 131);
            this.defaultUnitInListLabel.Name = "defaultUnitInListLabel";
            this.defaultUnitInListLabel.Size = new System.Drawing.Size(46, 13);
            this.defaultUnitInListLabel.TabIndex = 8;
            this.defaultUnitInListLabel.Text = "... in List";
            // 
            // defaultUnitInRecipesDropDown
            // 
            this.defaultUnitInRecipesDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultUnitInRecipesDropDown.FormattingEnabled = true;
            this.defaultUnitInRecipesDropDown.Location = new System.Drawing.Point(149, 101);
            this.defaultUnitInRecipesDropDown.Name = "defaultUnitInRecipesDropDown";
            this.defaultUnitInRecipesDropDown.Size = new System.Drawing.Size(142, 21);
            this.defaultUnitInRecipesDropDown.TabIndex = 9;
            // 
            // defaultUnitInListDropDown
            // 
            this.defaultUnitInListDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultUnitInListDropDown.FormattingEnabled = true;
            this.defaultUnitInListDropDown.Location = new System.Drawing.Point(149, 131);
            this.defaultUnitInListDropDown.Name = "defaultUnitInListDropDown";
            this.defaultUnitInListDropDown.Size = new System.Drawing.Size(142, 21);
            this.defaultUnitInListDropDown.TabIndex = 10;
            // 
            // assumeIsInStockCheckBox
            // 
            this.assumeIsInStockCheckBox.AutoSize = true;
            this.assumeIsInStockCheckBox.Location = new System.Drawing.Point(6, 166);
            this.assumeIsInStockCheckBox.Name = "assumeIsInStockCheckBox";
            this.assumeIsInStockCheckBox.Size = new System.Drawing.Size(110, 17);
            this.assumeIsInStockCheckBox.TabIndex = 11;
            this.assumeIsInStockCheckBox.Text = "I usually have this";
            this.assumeIsInStockCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(215, 189);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(134, 189);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.assumeIsInStockCheckBox);
            this.Controls.Add(this.defaultUnitInListDropDown);
            this.Controls.Add(this.defaultUnitInRecipesDropDown);
            this.Controls.Add(this.defaultUnitInListLabel);
            this.Controls.Add(this.defaultUnitInRecipesLabel);
            this.Controls.Add(this.defaultUnitLabel);
            this.Controls.Add(this.unitsDropDown);
            this.Controls.Add(this.unitLabel);
            this.Controls.Add(this.categoryDropDown);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Name = "ItemEditor";
            this.Size = new System.Drawing.Size(301, 218);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.ComboBox categoryDropDown;
        private System.Windows.Forms.Label unitLabel;
        private System.Windows.Forms.ComboBox unitsDropDown;
        private System.Windows.Forms.Label defaultUnitLabel;
        private System.Windows.Forms.Label defaultUnitInRecipesLabel;
        private System.Windows.Forms.Label defaultUnitInListLabel;
        private System.Windows.Forms.ComboBox defaultUnitInRecipesDropDown;
        private System.Windows.Forms.ComboBox defaultUnitInListDropDown;
        private System.Windows.Forms.CheckBox assumeIsInStockCheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
