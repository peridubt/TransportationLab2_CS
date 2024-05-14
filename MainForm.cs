namespace TransportationLab2
{
    public partial class MainForm : Form
    {
        private readonly Manager.Manager _manager = new();
        // public readonly PictureBox TruckPictureBox = new();
        public MainForm()
        {
            InitializeComponent();
        }

        private void AddVehicleButton_Click(object sender, EventArgs e)
        {
            try
            {
                _manager.CreateVehicle();
                /*TruckPictureBox.Image = Image.FromFile("C:\\Users\\leoni\\OneDrive\\Рабочий стол\\My Labs" +
                                                       "\\CSharp\\TransportationLab2\\Resources\\truck.png");
                TruckPictureBox.Visible = true;
                TruckPictureBox.Location = new Point(505, 380);
                TruckPictureBox.Size = new Size(500, 300);
                TruckPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                TruckPictureBox.Show();*/
            }
            catch (ManagerException ex)
            {
                MessageBox.Show(ex.Message, "Error!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddClientButton_Click(object sender, EventArgs e)
        {
            try
            {
                _manager.CreateClient();
            }
            catch (ManagerException ex)
            {
                MessageBox.Show(ex.Message, "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                _manager.CreateOrder();
            }
            catch (ManagerException ex)
            {
                MessageBox.Show(ex.Message, "Error!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewWarehouseButton_Click(object sender, EventArgs e)
        {

        }

        private void MskPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void SpbPictureBox_Click(object sender, EventArgs e)
        {

        }
        
        private void KznPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void SmrPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void VlgPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
