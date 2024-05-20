namespace TransportationLab2
{
    partial class CityVisual
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
            clientsCityListBox = new ListBox();
            cityLabel = new Label();
            vehiclesCityListBox = new ListBox();
            SuspendLayout();
            // 
            // clientsCityListBox
            // 
            clientsCityListBox.FormattingEnabled = true;
            clientsCityListBox.Location = new Point(20, 96);
            clientsCityListBox.Name = "clientsCityListBox";
            clientsCityListBox.Size = new Size(233, 324);
            clientsCityListBox.TabIndex = 1;
            // 
            // cityLabel
            // 
            cityLabel.AutoSize = true;
            cityLabel.Font = new Font("Segoe UI", 18F);
            cityLabel.Location = new Point(20, 22);
            cityLabel.Name = "cityLabel";
            cityLabel.Size = new Size(147, 41);
            cityLabel.TabIndex = 2;
            cityLabel.Text = "Clients in ";
            // 
            // vehiclesCityListBox
            // 
            vehiclesCityListBox.FormattingEnabled = true;
            vehiclesCityListBox.Location = new Point(308, 96);
            vehiclesCityListBox.Name = "vehiclesCityListBox";
            vehiclesCityListBox.Size = new Size(233, 324);
            vehiclesCityListBox.TabIndex = 3;
            // 
            // CityVisual
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(561, 450);
            Controls.Add(vehiclesCityListBox);
            Controls.Add(cityLabel);
            Controls.Add(clientsCityListBox);
            Name = "CityVisual";
            Text = "CityVisual";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public ListBox clientsCityListBox;
        public Label cityLabel;
        public ListBox vehiclesCityListBox;
    }
}