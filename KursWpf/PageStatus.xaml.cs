﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace KursWpf {
    /// <summary>
    /// Логика взаимодействия для PageStatus.xaml
    /// </summary>
    public partial class PageStatus : Page, INotifyPropertyChanged
    {
        private IServer<GameServer, Account> _server;
        private ManualResetEvent _resetEvent = new ManualResetEvent(false);

        private double _lastLecture;
        private double _trend;

        //public SeriesCollection SeriesCollection { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public Func<ChartPoint, string> PointLabel { get; set; }

        public PageStatus(IServer<GameServer, Account> server) {
            InitializeComponent();

            _server = server;

            LastHourSeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue(),
                        new ObservableValue()
                    }
                }
            };
            _trend = 8;

            PointLabel = chartPoint => $"{chartPoint.Y} ({chartPoint.Participation:P})";

            DataContext = this;

            Task.Run(() => {
                //var r = new Random();
                while (true) {
                    _resetEvent.WaitOne();
                    Thread.Sleep(1000);
                    //_trend += (r.NextDouble() > 0.3 ? 1 : -1) * r.Next(0, 5);
                    _trend = GC.GetTotalMemory(false) / (int)Math.Pow(1024, 2);
                    Application.Current.Dispatcher.Invoke(() => {
                        LastHourSeries[0].Values.Add(new ObservableValue(_trend));
                        LastHourSeries[0].Values.RemoveAt(0);
                        SetLecture();
                    });
                }
            });


            HostName.Content = String.Format((string)HostName.Content, _server.HostName);
            Ip.Content = String.Format((string)Ip.Content, _server.Ip);
            ServerWork.Content = $"Сервер: {(_server.ServerWork ? "включен" : "выключен")}";

        }


        public SeriesCollection LastHourSeries { get; set; }

        public double LastLecture {
            get => _lastLecture;
            set {
                _lastLecture = value;
                OnPropertyChanged("LastLecture");
            }
        }

        private void SetLecture() {
            var target = ((ChartValues<ObservableValue>)LastHourSeries[0].Values).Last().Value;
            var step = (target - _lastLecture) / 4;


            Task.Run(() => {
                for (var i = 0; i < 4; i++) {
                    Thread.Sleep(100);
                    LastLecture += step;
                }
                LastLecture = target;
            });

        }


        private void SetCountGamers()
        {
            Chess.Values = new ChartValues<int>(new int[] { _server.GetAllGames().FirstOrDefault(g => g.Id == 0)._listGamers.Count });
            Csgo.Values = new ChartValues<int>(new int[]{ _server.GetAllGames().FirstOrDefault(g => g.Id == 1)._listGamers.Count});
            Dota2.Values = new ChartValues<int>(new int[] { _server.GetAllGames().FirstOrDefault(g => g.Id == 2)._listGamers.Count });
            Overwatch.Values = new ChartValues<int>(new int[] { _server.GetAllGames().FirstOrDefault(g => g.Id == 3)._listGamers.Count });
            Pubg.Values = new ChartValues<int>(new int[] { _server.GetAllGames().FirstOrDefault(g => g.Id == 4)._listGamers.Count });
            Wow.Values = new ChartValues<int>(new int[] { _server.GetAllGames().FirstOrDefault(g => g.Id == 5)._listGamers.Count });

        }


        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //[DllImport("Kernel32")]
        //public static extern void AllocConsole();

        //[DllImport("Kernel32")]
        //public static extern void FreeConsole();


        private void ButtonStart_Click(object sender, RoutedEventArgs e) {


            if (!_server.ServerWork)
            {
                _server.Start();
                _resetEvent.Set();
                SetCountGamers();

                CountGamers.Content = String.Format((string)CountGamers.Content, _server.GetAllAccounts().Count);
                CountGames.Content = String.Format((string)CountGames.Content, _server.GetAllGames().Count);

                int countGameSessions = 0;

                foreach (var game in _server.GetAllGames())
                {
                    countGameSessions += game.GameSessions.Count;
                }
                
                CountSessions.Content = $"Количество игровых сессий в текущий момент: {countGameSessions}";
                ServerWork.Content = $"Сервер: {(_server.ServerWork ? "включен" : "выключен")}";
                // AllocConsole();
                //Console.WriteLine("test");


                //PointLabel = chartPoint => $"{chartPoint.Y} ({chartPoint.Participation:P})";

                //DataContext = this;
            }
        }


        private void RamCount() {
            PerformanceCounter ram;
            ram = new PerformanceCounter("Memory", "Available MBytes");
            MessageBox.Show(ram.NextValue() + "MB");
        }

        //public void Status() {
        //    //var gc = GC.GetTotalMemory(false);
        //    MessageBox.Show(GC.GetTotalMemory(false) / (int)Math.Pow(1024, 2) + " MB");

        //}

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {


            if (_server.ServerWork) {
                _server.Stop();

                _resetEvent.Reset();
                ServerWork.Content = $"Сервер: {(_server.ServerWork ? "включен" : "выключен")}";
                //FreeConsole();
            }
        }

        

    }
}
