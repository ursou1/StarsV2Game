﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StarsV2
{
    /// <summary>
    /// Логика взаимодействия для Skins.xaml
    /// </summary>
    public partial class Skins : Window
    {
        
        public Skins()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameMenu gameMenu = new GameMenu();
            gameMenu.Show();
            Close();
        }

        
    }
}
