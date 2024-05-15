namespace TransportationLab2
{
    public partial class MainForm : Form
    {
        private Manager.Manager _manager;
        public MainForm()
        {
            InitializeComponent();
        }

        public void MainForm_Load(object sender, EventArgs e)
        {
            _manager = new(ref clientsView, ref vehiclesView);
        }

        private void AddVehicleButton_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox vehcilePBox = new();
                _manager.CreateVehicle(ref vehcilePBox);
                Controls.Add(vehcilePBox);
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

        private void RestockWarehouseButton_Click(object sender, EventArgs e)
        {
            _manager.RestockWarehouse();
        }
    }
}
