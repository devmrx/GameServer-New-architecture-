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
using GameServerCore.MLogic;
using GameServerCore.MLogic.Games;

namespace KursWpf
{
    /// <summary>
    /// Логика взаимодействия для PageSettings.xaml
    /// </summary>
    public partial class PageSettings : Page
    {
        private IServer<GameServer, Account> _server;

        public PageSettings(IServer<GameServer, Account> server)
        {
            InitializeComponent();
            _server = server;

            LogWriteTriger.IsChecked = server.LogWriterOnOff;
        }

        private void LogWriteTriger_OnClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(LogWriteTriger.IsChecked.ToString());
            _server.LogWriterOnOff = LogWriteTriger.IsChecked ?? false;

            _server.SaveSettings();
        }

        

        
    }
}
