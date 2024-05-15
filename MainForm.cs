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

        private void AddClientsToView(CityVisual window)
        {
            var clients = _manager.Clients;
            foreach (var client in clients) 
            {
                if (client.City.ToString() == window.CityName)
                    window.clientsCityListBox.Items.Add(client.ToString());
            }
        }

        private void SpbLabel_Click(object sender, EventArgs e)
        {
            var window = new CityVisual();
            window.CityName = "Saint Petersburg";
            window.cityLabel.Text += window.CityName;
            AddClientsToView(window);
            window.Show();
        }

        private void KznLabel_Click(object sender, EventArgs e)
        {
            var window = new CityVisual();
            window.CityName = "Kazan";
            window.cityLabel.Text += window.CityName;
            AddClientsToView(window);
            window.Show();
        }

        private void SmrLabel_Click(object sender, EventArgs e)
        {
            var window = new CityVisual();
            window.CityName = "Samara";
            window.cityLabel.Text += window.CityName;
            AddClientsToView(window);
            window.Show();
        }

        private void VlgLabel_Click(object sender, EventArgs e)
        {
            var window = new CityVisual();
            window.CityName = "Volgograd";
            window.cityLabel.Text += window.CityName;
            AddClientsToView(window);
            window.Show();
        }
    }
}
