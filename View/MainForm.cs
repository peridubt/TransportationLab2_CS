using TransportationLab2.Controller;

namespace TransportationLab2
{
    public partial class MainForm : Form
    {
        private Manager _manager;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowError(ManagerException ex)
        {
            MessageBox.Show(ex.Message, "Error!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowCity(string city)
        {
            var window = new CityVisual();
            window.CityName = city;
            window.cityLabel.Text += window.CityName;
            AddElementsToView(window);
            window.Show();
        }

        public void MainForm_Load(object sender, EventArgs e)
        {
            _manager = new(ref clientsView, ref vehiclesView);
            CenterToScreen();
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
            catch (ManagerException ex)
            {
                ShowError(ex);
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
                ShowError(ex);
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
                ShowError(ex);
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

        private void AddElementsToView(CityVisual window)
        {
            if (window.CityName != "Moscow")
            {
                var clients = _manager.Clients;
                foreach (var client in clients.Where(client =>
                             client.City.ToString() == window.CityName))
                {
                    window.clientsCityListBox.Items.Add(client.ToString());
                }

                var activeVehicles = _manager.ActiveVehicles;
                foreach (var vehicle in activeVehicles.Where(vehicle =>
                             vehicle.TargetCity.ToString() == window.CityName))
                {
                    window.vehiclesCityListBox.Items.Add(vehicle.BoxInfo());
                }

                return;
            }

            window.clientsCityListBox.Visible = false;
            window.vehiclesCityListBox.Location = window.clientsCityListBox.Location;
            var vehicles = _manager.Vehicles;
            foreach (var vehicle in vehicles)
            {
                window.vehiclesCityListBox.Items.Add(vehicle.BoxInfo());
            }
        }

        private void SpbLabel_Click(object sender, EventArgs e)
        {
            ShowCity("Saint Petersburg");
        }

        private void KznLabel_Click(object sender, EventArgs e)
        {
            ShowCity("Kazan");
        }

        private void SmrLabel_Click(object sender, EventArgs e)
        {
            ShowCity("Samara");
        }

        private void VlgLabel_Click(object sender, EventArgs e)
        {
            ShowCity("Volgograd");
        }

        private void MskLabel_Click(object sender, EventArgs e)
        {
            ShowCity("Moscow");
        }
    }
}