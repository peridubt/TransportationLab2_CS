using System.Windows.Forms;

namespace TransportationLab2
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            System.Environment.Exit(1);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            addClientButton = new Button();
            addVehicleButton = new Button();
            vehiclesLabel = new Label();
            clientsLabel = new Label();
            vehiclesListView = new ListView();
            clientsListView = new ListView();
            spbPictureBox = new PictureBox();
            kznPictureBox = new PictureBox();
            smrPictureBox = new PictureBox();
            vlgPictureBox = new PictureBox();
            mskPictureBox = new PictureBox();
            viewWarehouseButton = new Button();
            createOrderButton = new Button();
            mapPictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)spbPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kznPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)smrPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)vlgPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mskPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mapPictureBox).BeginInit();
            SuspendLayout();
            // 
            // addClientButton
            // 
            addClientButton.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            addClientButton.Location = new Point(1, 547);
            addClientButton.Name = "addClientButton";
            addClientButton.Size = new Size(215, 66);
            addClientButton.TabIndex = 1;
            addClientButton.Text = "Add new client";
            addClientButton.UseVisualStyleBackColor = true;
            addClientButton.Click += AddClientButton_Click;
            // 
            // addVehicleButton
            // 
            addVehicleButton.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            addVehicleButton.Location = new Point(1, 109);
            addVehicleButton.Name = "addVehicleButton";
            addVehicleButton.Size = new Size(215, 66);
            addVehicleButton.TabIndex = 2;
            addVehicleButton.Text = "Add new vehicle";
            addVehicleButton.UseVisualStyleBackColor = true;
            addVehicleButton.Click += AddVehicleButton_Click;
            // 
            // vehiclesLabel
            // 
            vehiclesLabel.AutoSize = true;
            vehiclesLabel.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            vehiclesLabel.Location = new Point(1, 178);
            vehiclesLabel.Name = "vehiclesLabel";
            vehiclesLabel.Size = new Size(187, 30);
            vehiclesLabel.TabIndex = 4;
            vehiclesLabel.Text = "Available vehicles:";
            // 
            // clientsLabel
            // 
            clientsLabel.AutoSize = true;
            clientsLabel.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            clientsLabel.Location = new Point(1, 616);
            clientsLabel.Name = "clientsLabel";
            clientsLabel.Size = new Size(171, 30);
            clientsLabel.TabIndex = 5;
            clientsLabel.Text = "Available clients:";
            // 
            // vehiclesListView
            // 
            vehiclesListView.Location = new Point(1, 212);
            vehiclesListView.Name = "vehiclesListView";
            vehiclesListView.Size = new Size(215, 230);
            vehiclesListView.TabIndex = 6;
            vehiclesListView.UseCompatibleStateImageBehavior = false;
            // 
            // clientsListView
            // 
            clientsListView.Location = new Point(1, 649);
            clientsListView.Name = "clientsListView";
            clientsListView.Size = new Size(215, 230);
            clientsListView.TabIndex = 7;
            clientsListView.UseCompatibleStateImageBehavior = false;
            // 
            // spbPictureBox
            // 
            spbPictureBox.BackColor = Color.Transparent;
            spbPictureBox.BackgroundImageLayout = ImageLayout.None;
            spbPictureBox.Image = (Image)resources.GetObject("spbPictureBox.Image");
            spbPictureBox.Location = new Point(338, 109);
            spbPictureBox.Name = "spbPictureBox";
            spbPictureBox.Size = new Size(39, 62);
            spbPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            spbPictureBox.TabIndex = 9;
            spbPictureBox.TabStop = false;
            spbPictureBox.Click += SpbPictureBox_Click;
            // 
            // kznPictureBox
            // 
            kznPictureBox.BackColor = Color.Transparent;
            kznPictureBox.BackgroundImageLayout = ImageLayout.None;
            kznPictureBox.Image = (Image)resources.GetObject("kznPictureBox.Image");
            kznPictureBox.Location = new Point(910, 354);
            kznPictureBox.Name = "kznPictureBox";
            kznPictureBox.Size = new Size(39, 62);
            kznPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            kznPictureBox.TabIndex = 10;
            kznPictureBox.TabStop = false;
            kznPictureBox.Click += KznPictureBox_Click;
            // 
            // smrPictureBox
            // 
            smrPictureBox.BackColor = Color.Transparent;
            smrPictureBox.BackgroundImageLayout = ImageLayout.None;
            smrPictureBox.Image = (Image)resources.GetObject("smrPictureBox.Image");
            smrPictureBox.Location = new Point(971, 511);
            smrPictureBox.Name = "smrPictureBox";
            smrPictureBox.Size = new Size(39, 62);
            smrPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            smrPictureBox.TabIndex = 11;
            smrPictureBox.TabStop = false;
            smrPictureBox.Click += SmrPictureBox_Click;
            // 
            // vlgPictureBox
            // 
            vlgPictureBox.BackColor = Color.Transparent;
            vlgPictureBox.BackgroundImageLayout = ImageLayout.None;
            vlgPictureBox.Image = (Image)resources.GetObject("vlgPictureBox.Image");
            vlgPictureBox.Location = new Point(797, 798);
            vlgPictureBox.Name = "vlgPictureBox";
            vlgPictureBox.Size = new Size(39, 62);
            vlgPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            vlgPictureBox.TabIndex = 12;
            vlgPictureBox.TabStop = false;
            vlgPictureBox.Click += VlgPictureBox_Click;
            // 
            // mskPictureBox
            // 
            mskPictureBox.BackColor = Color.Transparent;
            mskPictureBox.BackgroundImageLayout = ImageLayout.None;
            mskPictureBox.Image = (Image)resources.GetObject("mskPictureBox.Image");
            mskPictureBox.Location = new Point(525, 380);
            mskPictureBox.Name = "mskPictureBox";
            mskPictureBox.Size = new Size(39, 62);
            mskPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            mskPictureBox.TabIndex = 13;
            mskPictureBox.TabStop = false;
            mskPictureBox.Click += MskPictureBox_Click;
            // 
            // viewWarehouseButton
            // 
            viewWarehouseButton.Font = new Font("Segoe UI", 13.8F);
            viewWarehouseButton.Location = new Point(254, 25);
            viewWarehouseButton.Name = "viewWarehouseButton";
            viewWarehouseButton.Size = new Size(215, 66);
            viewWarehouseButton.TabIndex = 14;
            viewWarehouseButton.Text = "View warehouse";
            viewWarehouseButton.UseVisualStyleBackColor = true;
            viewWarehouseButton.Click += ViewWarehouseButton_Click;
            // 
            // createOrderButton
            // 
            createOrderButton.Font = new Font("Segoe UI", 13.8F);
            createOrderButton.Location = new Point(525, 25);
            createOrderButton.Name = "createOrderButton";
            createOrderButton.Size = new Size(215, 66);
            createOrderButton.TabIndex = 15;
            createOrderButton.Text = "Create order";
            createOrderButton.UseVisualStyleBackColor = true;
            createOrderButton.Click += CreateOrderButton_Click;
            // 
            // mapPictureBox
            // 
            mapPictureBox.Image = (Image)resources.GetObject("mapPictureBox.Image");
            mapPictureBox.Location = new Point(254, 109);
            mapPictureBox.Name = "mapPictureBox";
            mapPictureBox.Size = new Size(929, 800);
            mapPictureBox.TabIndex = 16;
            mapPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 911);
            Controls.Add(createOrderButton);
            Controls.Add(viewWarehouseButton);
            Controls.Add(mskPictureBox);
            Controls.Add(vlgPictureBox);
            Controls.Add(smrPictureBox);
            Controls.Add(kznPictureBox);
            Controls.Add(spbPictureBox);
            Controls.Add(clientsListView);
            Controls.Add(vehiclesListView);
            Controls.Add(clientsLabel);
            Controls.Add(vehiclesLabel);
            Controls.Add(addVehicleButton);
            Controls.Add(addClientButton);
            Controls.Add(mapPictureBox);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)spbPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)kznPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)smrPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)vlgPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)mskPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)mapPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button addClientButton;
        private Button addVehicleButton;
        private Label vehiclesLabel;
        private Label clientsLabel;
        private ListView vehiclesListView;
        private ListView clientsListView;
        private PictureBox spbPictureBox;
        private PictureBox kznPictureBox;
        private PictureBox smrPictureBox;
        private PictureBox vlgPictureBox;
        private PictureBox mskPictureBox;
        private Button viewWarehouseButton;
        private Button createOrderButton;
        private PictureBox mapPictureBox;
    }
}
