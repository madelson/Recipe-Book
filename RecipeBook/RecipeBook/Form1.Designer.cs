namespace RecipeBook
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.itemEditor1 = new RecipeBook.Gui.ItemEditor();
            this.recipeEditor1 = new RecipeBook.Gui.RecipeEditor();
            this.SuspendLayout();
            // 
            // itemEditor1
            // 
            this.itemEditor1.Location = new System.Drawing.Point(13, 13);
            this.itemEditor1.Name = "itemEditor1";
            this.itemEditor1.ReadOnly = false;
            this.itemEditor1.Size = new System.Drawing.Size(306, 225);
            this.itemEditor1.TabIndex = 0;
            // 
            // recipeEditor1
            // 
            this.recipeEditor1.Location = new System.Drawing.Point(326, 13);
            this.recipeEditor1.Name = "recipeEditor1";
            this.recipeEditor1.Size = new System.Drawing.Size(814, 509);
            this.recipeEditor1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 574);
            this.Controls.Add(this.recipeEditor1);
            this.Controls.Add(this.itemEditor1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Gui.ItemEditor itemEditor1;
        private Gui.RecipeEditor recipeEditor1;
    }
}

