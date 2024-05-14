namespace TransportationLab2
{
    public partial class MainForm : Form
    {
        private readonly Manager.Manager _manager = new();
        public MainForm()
        {
            InitializeComponent();
        }

        private void AddVehicleButton_Click(object sender, EventArgs e)
        {
            try
            {
                _manager.CreateVehicle();
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
