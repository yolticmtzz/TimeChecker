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
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.IO.Ports;
using System.Windows.Threading;

/**
 * Die Klasse MainWindow MainWindow.xaml ist die Interaktionslogik für MainWindow.xaml.
 * Man kann mit dieser WPF Applikation eine serielle Verbindung zu einem Arduino Board aufbauen.
 * Der eingelesen RFID Code und den Status des Batches wird angezeigt.
 * 
 * Methoden: 
 * 
 * _readSerialData() - List die RFID Daten des Batches über die serielle Verbindung ein.
 * 
 * InsertCheckIn() - Timestamp CheckIN der Datenbank hinzufügen
 * 
 * InsertCheckOut() - Timestamp CheckOUT der Datenbank hinzufügen
 * 
 * ComboBox_SelectionChanged() - Übergibt den COM-Port des PC's an die Combox
 * 
 * Connect_Click() - Baut eine serielle Verbindung auf
 * 
 * Disconnect_Click() - Baut eine serielle Verbindung ab
 * 
 * 
 * @Author Jose Panov
 * @Version 2022.01.01
 */


namespace TimeCheckerWPF_Arduino
{

    public partial class MainWindow : Window
    {
        // Serialport instanziieren
        SerialPort serialport = new SerialPort();
        // DispatcherTimer instanziieren
        private readonly DispatcherTimer _readSerialDataTimer = new DispatcherTimer();

        // Interval und Tick Event von DispatcherTimer deklarieren
        public MainWindow()
        {
            InitializeComponent();
            _readSerialDataTimer.Interval = TimeSpan.FromMilliseconds(500);
            _readSerialDataTimer.Tick += _readSerialData;
        }

        // Context aus der Datenbank holen
        ApplicationDbContext _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
        .Options);

        /// <summary>
        /// List die RFID Daten des Batches über die serielle Verbindung ein.
        /// Gibt die eingelesenen Daten in der RichTextBox aus.
        /// Bei CheckIN wird die Methode InsertCheckIn() aufgerufen.
        /// Bei CheckOUT wird die Methode InsertCheckOUT() aufgerufen.
        /// Status text auf "Connected" setzen wenn ProgressBar = 100 ist.
        /// </summary>
        /// 
        /// <param name="sender">
        /// Objekt Ereignishandler
        /// </param>
        /// 
        /// <param name="e">
        /// Dummy base class
        /// </param>
        private void _readSerialData(object sender, EventArgs e)
        {
            string readSerial = serialport.ReadExisting();
            RichTextBox.AppendText(readSerial);

            if (readSerial.Contains("CheckIN"))
            {
                InsertCheckIn();
                readSerial = "";
            }
            if (readSerial.Contains("CheckOUT"))
            {
                InsertCheckOut();
                readSerial = "";
            }
            if (ProgressBar.Value == 100)
            {
                Status_text.Text = "Connected";
            }
        }

        /// <summary>
        /// Timestamp CheckIN der Datenbank hinzufügen
        /// </summary>
        private void InsertCheckIn()
        {
            var record = new Timeentry()
            {
                Type = 1,
                DateTime = DateTime.Now,
                Comment = "Batch-IN",
                User = "Dummy User 77",
            };

            _context.Timeentry.Add(record);
            _context.SaveChanges();
        }

        /// <summary>
        /// Timestamp CheckOUT der Datenbank hinzufügen
        /// </summary>
        private void InsertCheckOut()
        {
            var record = new Timeentry()
            {
                Type = 2,
                DateTime = DateTime.Now,
                Comment = "Batch-OUT",
                User = "Dummy User 77",
            };

            _context.Timeentry.Add(record);
            _context.SaveChanges();
        }

        /// <summary>
        /// Übergibt den COM-Port des PC's an die Combox
        /// </summary>
        /// 
        /// <param name="sender">
        /// Objekt Ereignishandler
        /// </param>
        /// 
        /// <param name="e">
        /// Dummy base class
        /// </param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedcomboitem = sender as ComboBox;
            string name = selectedcomboitem.SelectedItem as string;
        }

        /// <summary>
        /// Baut eine serielle Verbindung auf
        /// _readSerialDataTimer auf "Start" setzen
        /// </summary>
        /// 
        /// <param name="sender">
        /// Objekt Ereignishandler
        /// </param>
        /// 
        /// <param name="e">
        /// Dummy base class
        /// </param>
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string portName = COM.SelectedItem as string;
                serialport.PortName = portName;
                serialport.BaudRate = 9600;
                serialport.Open();

                for (int i = 0; i < 101; i++)
                {
                    ProgressBar.Value = i;

                }
                
                _readSerialDataTimer.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Please give a valid port number or check your connection!");
            }
        }

        /// <summary>
        /// Baut eine serielle Verbindung ab
        /// _readSerialDataTimer auf "Stop" setzen
        /// </summary>
        /// 
        /// <param name="sender">
        /// Objekt Ereignishandler
        /// </param>
        /// 
        /// <param name="e">
        /// Dummy base class
        /// </param>
        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serialport.Close();
                Status_text.Text = "Disconnected";
                ProgressBar.Value = 0;
                _readSerialDataTimer.Stop();
                RichTextBox.Document.Blocks.Clear();

            }
            catch (Exception)
            {
                MessageBox.Show("First connect and then disconnect!");
            }
        }
    }
}
