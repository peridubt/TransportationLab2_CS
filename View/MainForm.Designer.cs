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
            spbPictureBox = new PictureBox();
            kznPictureBox = new PictureBox();
            smrPictureBox = new PictureBox();
            vlgPictureBox = new PictureBox();
            mskPictureBox = new PictureBox();
            viewWarehouseButton = new Button();
            spbLabel = new Label();
            mskLabel = new Label();
            kaznLabel = new Label();
            smrLabel = new Label();
            vlgLabel = new Label();
            messagesTextBox = new TextBox();
            messageLogLabel = new Label();
            startButton = new Button();
            ((System.ComponentModel.ISupportInitialize)spbPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kznPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)smrPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)vlgPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mskPictureBox).BeginInit();
            SuspendLayout();
            // 
            // spbPictureBox
            // 
            spbPictureBox.BackColor = Color.Transparent;
            spbPictureBox.BackgroundImageLayout = ImageLayout.None;
            spbPictureBox.Image = (Image)resources.GetObject("spbPictureBox.Image");
            spbPictureBox.Location = new Point(360, 163);
            spbPictureBox.Name = "spbPictureBox";
            spbPictureBox.Size = new Size(39, 62);
            spbPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            spbPictureBox.TabIndex = 9;
            spbPictureBox.TabStop = false;
            // 
            // kznPictureBox
            // 
            kznPictureBox.BackColor = Color.Transparent;
            kznPictureBox.BackgroundImageLayout = ImageLayout.None;
            kznPictureBox.Image = (Image)resources.GetObject("kznPictureBox.Image");
            kznPictureBox.Location = new Point(929, 408);
            kznPictureBox.Name = "kznPictureBox";
            kznPictureBox.Size = new Size(39, 62);
            kznPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            kznPictureBox.TabIndex = 10;
            kznPictureBox.TabStop = false;
            // 
            // smrPictureBox
            // 
            smrPictureBox.BackColor = Color.Transparent;
            smrPictureBox.BackgroundImageLayout = ImageLayout.None;
            smrPictureBox.Image = (Image)resources.GetObject("smrPictureBox.Image");
            smrPictureBox.Location = new Point(988, 551);
            smrPictureBox.Name = "smrPictureBox";
            smrPictureBox.Size = new Size(39, 62);
            smrPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            smrPictureBox.TabIndex = 11;
            smrPictureBox.TabStop = false;
            // 
            // vlgPictureBox
            // 
            vlgPictureBox.BackColor = Color.Transparent;
            vlgPictureBox.BackgroundImageLayout = ImageLayout.None;
            vlgPictureBox.Image = (Image)resources.GetObject("vlgPictureBox.Image");
            vlgPictureBox.Location = new Point(811, 837);
            vlgPictureBox.Name = "vlgPictureBox";
            vlgPictureBox.Size = new Size(39, 62);
            vlgPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            vlgPictureBox.TabIndex = 12;
            vlgPictureBox.TabStop = false;
            // 
            // mskPictureBox
            // 
            mskPictureBox.BackColor = Color.Transparent;
            mskPictureBox.BackgroundImageLayout = ImageLayout.None;
            mskPictureBox.Image = (Image)resources.GetObject("mskPictureBox.Image");
            mskPictureBox.Location = new Point(544, 426);
            mskPictureBox.Name = "mskPictureBox";
            mskPictureBox.Size = new Size(39, 62);
            mskPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            mskPictureBox.TabIndex = 13;
            mskPictureBox.TabStop = false;
            // 
            // viewWarehouseButton
            // 
            viewWarehouseButton.Font = new Font("Segoe UI", 13.8F);
            viewWarehouseButton.Location = new Point(3, 242);
            viewWarehouseButton.Name = "viewWarehouseButton";
            viewWarehouseButton.Size = new Size(269, 78);
            viewWarehouseButton.TabIndex = 14;
            viewWarehouseButton.Text = "View warehouse";
            viewWarehouseButton.UseVisualStyleBackColor = true;
            viewWarehouseButton.Click += ViewWarehouseButton_Click;
            // 
            // spbLabel
            // 
            spbLabel.AutoSize = true;
            spbLabel.BackColor = Color.Transparent;
            spbLabel.Cursor = Cursors.Hand;
            spbLabel.Location = new Point(340, 228);
            spbLabel.Name = "spbLabel";
            spbLabel.Size = new Size(155, 28);
            spbLabel.TabIndex = 18;
            spbLabel.Text = "Saint Petersburg";
            spbLabel.Click += SpbLabel_Click;
            // 
            // mskLabel
            // 
            mskLabel.AutoSize = true;
            mskLabel.BackColor = Color.Transparent;
            mskLabel.Cursor = Cursors.Hand;
            mskLabel.Location = new Point(528, 395);
            mskLabel.Name = "mskLabel";
            mskLabel.Size = new Size(85, 28);
            mskLabel.TabIndex = 19;
            mskLabel.Text = "Moscow";
            mskLabel.Click += MskLabel_Click;
            // 
            // kaznLabel
            // 
            kaznLabel.AutoSize = true;
            kaznLabel.BackColor = Color.Transparent;
            kaznLabel.Cursor = Cursors.Hand;
            kaznLabel.Location = new Point(918, 377);
            kaznLabel.Name = "kaznLabel";
            kaznLabel.Size = new Size(64, 28);
            kaznLabel.TabIndex = 20;
            kaznLabel.Text = "Kazan";
            kaznLabel.Click += KznLabel_Click;
            // 
            // smrLabel
            // 
            smrLabel.AutoSize = true;
            smrLabel.BackColor = Color.Transparent;
            smrLabel.Cursor = Cursors.Hand;
            smrLabel.Location = new Point(970, 520);
            smrLabel.Name = "smrLabel";
            smrLabel.Size = new Size(77, 28);
            smrLabel.TabIndex = 21;
            smrLabel.Text = "Samara";
            smrLabel.Click += SmrLabel_Click;
            // 
            // vlgLabel
            // 
            vlgLabel.AutoSize = true;
            vlgLabel.BackColor = Color.Transparent;
            vlgLabel.Cursor = Cursors.Hand;
            vlgLabel.Location = new Point(785, 806);
            vlgLabel.Name = "vlgLabel";
            vlgLabel.Size = new Size(105, 28);
            vlgLabel.TabIndex = 22;
            vlgLabel.Text = "Volgograd";
            vlgLabel.Click += VlgLabel_Click;
            // 
            // messagesTextBox
            // 
            messagesTextBox.Font = new Font("Segoe UI", 8F);
            messagesTextBox.Location = new Point(3, 51);
            messagesTextBox.Multiline = true;
            messagesTextBox.Name = "messagesTextBox";
            messagesTextBox.ReadOnly = true;
            messagesTextBox.ScrollBars = ScrollBars.Vertical;
            messagesTextBox.Size = new Size(1067, 101);
            messagesTextBox.TabIndex = 23;
            // 
            // messageLogLabel
            // 
            messageLogLabel.AutoSize = true;
            messageLogLabel.BackColor = Color.Transparent;
            messageLogLabel.Font = new Font("Segoe UI", 16F);
            messageLogLabel.Location = new Point(3, 11);
            messageLogLabel.Name = "messageLogLabel";
            messageLogLabel.Size = new Size(167, 37);
            messageLogLabel.TabIndex = 24;
            messageLogLabel.Text = "Message log";
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 13.8F);
            startButton.Location = new Point(3, 158);
            startButton.Name = "startButton";
            startButton.Size = new Size(269, 78);
            startButton.TabIndex = 25;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1082, 911);
            Controls.Add(startButton);
            Controls.Add(messageLogLabel);
            Controls.Add(messagesTextBox);
            Controls.Add(vlgLabel);
            Controls.Add(smrLabel);
            Controls.Add(kaznLabel);
            Controls.Add(mskLabel);
            Controls.Add(spbLabel);
            Controls.Add(viewWarehouseButton);
            Controls.Add(mskPictureBox);
            Controls.Add(vlgPictureBox);
            Controls.Add(smrPictureBox);
            Controls.Add(kznPictureBox);
            Controls.Add(spbPictureBox);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MainForm";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)spbPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)kznPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)smrPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)vlgPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)mskPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox spbPictureBox;
        private PictureBox kznPictureBox;
        private PictureBox smrPictureBox;
        private PictureBox vlgPictureBox;
        private PictureBox mskPictureBox;
        private Button viewWarehouseButton;
        private Label spbLabel;
        private Label mskLabel;
        private Label kaznLabel;
        private Label smrLabel;
        private Label vlgLabel;
        private TextBox messagesTextBox;
        private Label messageLogLabel;
        private Button startButton;
    }
}
