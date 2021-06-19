using System;
using System.Collections.Generic;
using System.Media;
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
    /// Логика взаимодействия для GameMenu.xaml
    /// </summary>
    public partial class GameMenu : Window
    {
        SoundPlayer ready = new SoundPlayer(Environment.CurrentDirectory + "/Music/ready.wav");
        public GameMenu()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Skins skins = new Skins();
            ready.Play();
            skins.Show();
            Close();
        }

        
    }
}
