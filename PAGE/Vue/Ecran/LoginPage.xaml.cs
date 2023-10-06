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

namespace PAGE.Vue.Ressources
{
    /// <summary>
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public event EventHandler ReturnToMainWindow;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void CloseLoginWindow(object sender, RoutedEventArgs e)
        {
            ReturnToMainWindow?.Invoke(this, e);
        }

    }
}
