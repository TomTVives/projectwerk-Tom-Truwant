using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using static System.Net.Mime.MediaTypeNames;
using projectwerk_Tom_Truwant;

namespace COM_poort___demo_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort _serialPort;
        Sensor _sensor;

        public MainWindow()
        {
            InitializeComponent();

            _serialPort = new SerialPort();          

            cbxComPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxComPorts.Items.Add(s);

            // Bij ontvangen data, een event hanlder oproepen.
            _serialPort.DataReceived += _serialPort_DataReceived;

            _sensor = new Sensor();
            _sensor.Merk = "Arduino";
            _sensor.Meetafstand = 5;
            _sensor.Type = 663170;
        }

        public void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Lees alle data in, tot een new line passeert... ('\n').
            string receivedText = _serialPort.ReadLine().Trim();

            // Geef de ontvangen data via de Dispatcher door naar de UI-thread.
            // Gebruik daarvoor een Action. Actions worden verder behandeld in het vak OOP...
            Dispatcher.Invoke(new Action<string>(UpdateLabel), receivedText);
            
        }

        public void UpdateLabel(string text)
        {          
            //afstandmeting sensor weergeven
            lblReceivedData.Content = $"{text} centimeter resterend";
            
            int afstand;
            afstand = Convert.ToInt32(text);

            if (afstand >= 50)
            {
                lblReceivedData.Background = Brushes.LightGreen;
            }
            if ((afstand > 10) && (afstand < 50))
            {
                lblReceivedData.Background = Brushes.Orange;
            }
            if (afstand < 10)
            {
                lblReceivedData.Background = Brushes.Red;
               
            }
            
            //gegevens sensor weergeven
            merkLabel.Content = _sensor.merksensor();
            typeLabel.Content = _sensor.typesensor();
            afstandLabel.Content = _sensor.afstandsensor();
        }      

        private void cbxComPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                if (cbxComPorts.SelectedItem.ToString() != "None")
                {
                    _serialPort.PortName = cbxComPorts.SelectedItem.ToString();
                    _serialPort.Open();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_serialPort != null)
                _serialPort.Dispose();
        }
    }
}
