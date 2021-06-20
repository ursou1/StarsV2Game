using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Packaging;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace StarsV2
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        List<Rectangle> itemRemover = new List<Rectangle>();

        SoundPlayer gimi = new SoundPlayer(Environment.CurrentDirectory + "/Music/musiclvl2.wav");
        SoundPlayer lisi = new SoundPlayer(Environment.CurrentDirectory + "/Music/Lose1.wav");

        Random rand = new Random();
        
        bool moveLeft, moveRight;
        int enemySpriteCounter = 0;
        int enemyCounter = 100;
        int playerSpeed = 10;
        int limit = 50;
        int score = 0;
        int damage = 0;
        int enemySpeed = 10;

        Rect playerHitBox;
        public Game()
        {
            InitializeComponent();
            #region background для элемента canvas
            string path = Environment.CurrentDirectory + "/Sprites/Karta_kosmo.png";
            Canva.Background = new ImageBrush(new BitmapImage(new Uri(path)));
            #endregion

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
            gimi.Play();
            Canva.Focus();
            Canva.MouseLeftButtonDown += Xorek;
            ImageBrush bg = new ImageBrush();
            ImageBrush playerImage = new ImageBrush();
            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/korabl.png"));
            player.Fill = playerImage;
            
        }
        private void GameLoop(object sender, EventArgs e)
        {
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            score += 5;
            enemyCounter -= 10;

            scoreText.Content = "Score: " + score;
            damageText.Content = "Damage " + damage;

            if (enemyCounter < 20)
            {
                MakeEnemies();
                enemyCounter = limit;
            }

            if (moveLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (moveRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }


            foreach (var x in Canva.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    Rect bulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (Canvas.GetTop(x) < 10)
                    {
                        itemRemover.Add(x);
                    }

                    foreach (var y in Canva.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (bulletHitBox.IntersectsWith(enemyHit))
                            {
                                itemRemover.Add(x);
                                itemRemover.Add(y);
                                score++;
                            }
                        }
                    }

                }

                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + enemySpeed);

                    if (Canvas.GetTop(x) > 750)
                    {
                        itemRemover.Add(x);
                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        itemRemover.Add(x);
                        damage += 5;
                    }

                }
            }

            foreach (Rectangle i in itemRemover)
            {
                Canva.Children.Remove(i);
            }

            

            if (damage > 99)
            {
                gameTimer.Stop();
                damageText.Content = "Damage: 100";
                damageText.Foreground = Brushes.Red;
                gimi.Stop();
                lisi.Play();
                MessageBox.Show("Score " + score + " " + Environment.NewLine + "Game end");
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }


        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }

            if (e.Key == Key.Space)
            {
                Rectangle newBullet = new Rectangle
                {

                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red

                };
                
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);
                Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);

                Canva.Children.Add(newBullet);
               
            }

            if (e.Key == Key.Escape)
            {
                GameMenu gameMenu = new GameMenu();
                gameMenu.Show();
                gameTimer.Stop();
                gimi.Stop();
                Close();
            }
        }

        private void MakeEnemies()
        {
            ImageBrush enemySprite = new ImageBrush();

            enemySpriteCounter = rand.Next(1, 5);

            switch (enemySpriteCounter)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/met1.png"));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/met2.png"));
                    break;
                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/met3.png"));
                    break;
                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/met4.png"));
                    break;
                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/met5.png"));
                    break;
            }

            Rectangle newEnemy = new Rectangle
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = enemySprite
            };

            Canvas.SetTop(newEnemy, -100);
            Canvas.SetLeft(newEnemy, rand.Next(30, 870));
            Canva.Children.Add(newEnemy);
        }

        public void Xorek(object sender, MouseButtonEventArgs e)
        {
            Rectangle newBullet = new Rectangle
            {

                Tag = "bullet",
                Height = 20,
                Width = 5,
                Fill = Brushes.White,
                Stroke = Brushes.Red

            };
            
            Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);
            Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);

            Canva.Children.Add(newBullet);
        }

       
    }
}
