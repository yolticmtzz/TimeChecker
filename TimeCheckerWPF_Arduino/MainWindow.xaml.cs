﻿using System;
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




namespace TimeCheckerWPF_Arduino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort sp = new SerialPort();
        public MainWindow()
        {
            InitializeComponent();

        }


        ApplicationDbContext _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
        .Options);

        private void Insert()
        {
            var record = new Timeentry()
            {
                Type = 1,
                DateTime = DateTime.Now,
                Comment = "Batch",

            };

            _context.Timeentry.Add(record);
            _context.SaveChanges();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedcomboitem = sender as ComboBox;
            string name = selectedcomboitem.SelectedItem as string;


        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                string portName = COM.SelectedItem as string;
                sp.PortName = portName;
                sp.BaudRate = 9600;
                sp.Open();
                Status.Text = "Connected";

            }

            catch (Exception)
            {
                MessageBox.Show("Please give a valid port number or check your connection!");
            }


        }


        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                sp.Close();
                Status.Text = "Disconnected";

            }

            catch (Exception)
            {
                MessageBox.Show("First connect and then disconnect!");
            }


        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {

            sp.Write(TextSendBox.Text + '#');
            string s = sp.ReadLine();
            RichTextBox.AppendText(s);
            Insert();
        }
    }

}
