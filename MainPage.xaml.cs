using GK.Logic;
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

namespace GK
{
    /// <summary>
    /// Logika interakcji dla klasy MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public int a { get; set; } = Consts.DefaultA;
        public int n { get; set; } = Consts.DefaultN;        
        public int c { get; set; } = Consts.DefaultC;        
        
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void playWithAiButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame(new Game(a, c, n));
        }

        private void StartGame(Game game)
        {
            this.NavigationService.Navigate(new GameBoard(game));
        }
    }
}
