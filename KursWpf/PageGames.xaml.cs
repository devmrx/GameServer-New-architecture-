using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GameServerCore.MLogic;
using GameServerCore.MLogic.Games;

namespace KursWpf {
    /// <summary>
    /// Логика взаимодействия для PageGames.xaml
    /// </summary>
    public partial class PageGames : Page {

        public PageGames(IServer<GameServer, Account> server) {
            InitializeComponent();

            GamesList.ItemsSource = server.GetAllGames();

        }

        private void WrapPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}
