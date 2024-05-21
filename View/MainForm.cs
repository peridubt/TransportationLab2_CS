using TransportationLab2.Controller;
using TransportationLab2.Model;

#region Логика работы моей программы
/*
Автоматичекски добавляются (рандомные) грузовики: максимально — 5 шт.
Автоматически добавляются (рандомные) клиенты : максимально — 10 шт.
В форме есть конпка "Старт", которая запускает процесс выдачи заказов
незанятым грузовикам (происходит каждые 5 секунд).
Один грузовик может иметь один заказ. Кнопка "Стоп" приостанавливает процесс выдачи заказов,
но сами заказы не отменяет.
У каждого грузовика есть клиент, которому надо доставить груз. Сам клиент он подписывается на event,
который в будущем уведомит его о доставке и запустит (через Invoke) процесс передачи заказа.
После доставки клиент отписывается от данного event.
Один поток — это один грузовик. Сам поток запускает бесконечный цикл,
где грузовик запрашивает каждые 100 мс, не поступил ли ему новый заказ.
При положительном результате он сразу начинает своё движение.

Из MVC у меня:
Model — класс Manager + классы различных сущностей;
Controller — класс Animation;
View — классы различных винформ.
Из фабрики: фабрика грузов различных типов (я решил сделать 4 груза).
Наблюдатель: события, которые уведомляют клиентов.
 */
#endregion

namespace TransportationLab2.View
{
    public partial class MainForm : Form
    {
        private Manager _manager;
        private Dictionary<string, PictureBox> _citiesPBox;

        public MainForm()
        {
            InitializeComponent();
        }

        private static void ShowError(ManagerException ex)
        {
            MessageBox.Show(ex.Message, @"Error!",
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

        private void MainForm_Load(object sender, EventArgs e) // Инициализация нужных полей
        {
            _citiesPBox = new Dictionary<string, PictureBox> // Словарь городов, где ключ - это имя города, 
                // а значение - PictureBox (используется для определения координат картинки на винформе)
            {
                [vlgPictureBox.Name] = vlgPictureBox,
                [kznPictureBox.Name] = kznPictureBox,
                [mskPictureBox.Name] = mskPictureBox,
                [spbPictureBox.Name] = spbPictureBox,
                [smrPictureBox.Name] = smrPictureBox,
            };
            Animation.MessageHandler = messagesTextBox; // Передаём контролу TextBox из формы
            Animation.CitiesAvatars = _citiesPBox; // Передаём контролу словарь городов
            _manager = new Manager();
            for (var i = 0; i < 5; ++i)
                Controls.Add(Animation.VehicleAvatars[i]); // После создания
            
            CenterToScreen();
        }

        private void ViewWarehouseButton_Click(object sender, EventArgs e)
        {
            var warehouseWindow = new WarehouseVisual();
            warehouseWindow.warehouseTextBox.Text = _manager.ViewWarehouse();
            warehouseWindow.Show();
        }

        private void AddElementsToView(CityVisual window)
        {
            if (window.CityName != "Moscow")
            {
                var clients = _manager.Clients;
                foreach (var client in clients.Where(client =>
                             client.City?.ToString() == window.CityName))
                {
                    window.clientsCityListBox.Items.Add(client.BoxInfo());
                }

                var activeVehicles = _manager.ActiveVehicles;
                foreach (var vehicle in activeVehicles.Where(vehicle =>
                             vehicle.TargetCity?.ToString() == window.CityName))
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

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                _manager.Start();
            }
            catch (ManagerException ex)
            {
                ShowError(ex);
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                _manager.Stop();
            }
            catch (ManagerException ex)
            {
                ShowError(ex);
            }
        }
    }
}