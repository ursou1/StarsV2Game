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
    /// Логика взаимодействия для Skins.xaml
    /// </summary>
    public partial class Skins : Window
    {
        SoundPlayer ready = new SoundPlayer(Environment.CurrentDirectory + "/Music/ready.wav");
        public Skins()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameMenu gameMenu = new GameMenu();
            ready.Play();
            gameMenu.Show();
            Close();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            
            game.Show();
            Close();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Game2 game2 = new Game2();
            
            game2.Show();
            Close();
            
        }
    }
}
