﻿using System;
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

namespace KursWpf
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class PageUsers : Page
    {
        private IServer<GameServer, Account> _server;
        private ObservableCollection<Account> accounts;

        public PageUsers(IServer<GameServer, Account> server)
        {
            InitializeComponent();
            _server = server;
            List<Account> accountsList = _server.GetAllAccounts();

            if(accountsList == null) return;
            accounts = new ObservableCollection<Account>(accountsList);  // need?
            //AccountList.ItemsSource = accounts;
            AccountList.ItemsSource = accounts;
            //MessageBox.Show(_server.Status());
        }
    }
}
