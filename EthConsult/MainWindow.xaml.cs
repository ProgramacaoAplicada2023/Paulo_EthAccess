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

namespace EthConsult
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            frame.Content = Pages.loginPage;
            Pages.mainWindow = this;
        }

        public void SwitchPage(object newPage)
        {
            frame.Content = newPage;
        }
    }
}
