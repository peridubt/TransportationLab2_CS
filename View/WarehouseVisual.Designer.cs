namespace TransportationLab2
{
    partial class WarehouseVisual
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
            warehouseTextBox = new TextBox();
            SuspendLayout();
            // 
            // warehouseTextBox
            // 
            warehouseTextBox.Location = new Point(5, 12);
            warehouseTextBox.Multiline = true;
            warehouseTextBox.Name = "warehouseTextBox";
            warehouseTextBox.ReadOnly = true;
            warehouseTextBox.ScrollBars = ScrollBars.Vertical;
            warehouseTextBox.Size = new Size(613, 435);
            warehouseTextBox.TabIndex = 0;
            // 
            // WarehouseVisual
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 450);
            Controls.Add(warehouseTextBox);
            Name = "WarehouseVisual";
            Text = "WarehouseVisual";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TextBox warehouseTextBox;
    }
}