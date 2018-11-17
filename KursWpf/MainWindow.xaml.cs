using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using GameServerCore.MLogic;
using GameServerCore.MLogic.Games;

namespace KursWpf {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IServer<GameServer, Account> _server;
        private PageStatus pageStatus;

        public MainWindow() {

            InitializeComponent();

            _server = new Server();
            _server.SetMethodLog(LogWriter.GetInst().WriteFileL);
            _server.SetMethodQuestionOutput(QuestionWindow);


        }

        private void Frame_Navigated(object sender, NavigationEventArgs e) {

        }

        private void BtnClickStatusPage(object sender, RoutedEventArgs e) {

            if(pageStatus == null) pageStatus = new PageStatus(_server);

            Main.Content = pageStatus;
        }



        private void BtnClickGamesPage(object sender, RoutedEventArgs e) {
            Main.Content = new PageGames(_server);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

            Main.Content = new PageUsers(_server);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Main.Content = new PageSettings();
        }

        private void Window_Closed(object sender, EventArgs e) {

            LogWriter.GetInst().Dispose();
        }

        public bool QuestionWindow(string question) {
            DialogWindow dialogWindow = new DialogWindow(question);

            return dialogWindow.ShowDialog() == true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

            if (_server.ServerWork) {
                _server.Stop();
            }
  
        }
    }
}
