namespace TransportationLab2
{
    public partial class MainForm : Form
    {
        private Controller.Manager _manager;
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
                PictureBox vehiclePBox = new();
                _manager.CreateVehicle(ref vehiclePBox);
                Controls.Add(vehiclePBox);
                vehiclePBox.BringToFront();
                // Swap(vehiclePBox, mapPictureBox);
            }
            catch (Controller.ManagerException ex)
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
            catch (Controller.ManagerException ex)
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
            catch (Controller.ManagerException ex)
            {
                MessageBox.Show(ex.Message, "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewWarehouseButton_Click(object sender, EventArgs e)
        {
            var warehouseWindow = new WarehouseVisual();
            warehouseWindow.warehouseTextBox.Text = _manager.ViewWarehouse();
            warehouseWindow.Show();
        }

        private void RestockWarehouseButton_Click(object sender, EventArgs e)
        {
            _manager.RestockWarehouse();
        }
    }
}
