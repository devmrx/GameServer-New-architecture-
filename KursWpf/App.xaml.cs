using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KursWpf {
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application {

        Mutex _mutex;

        private void App_Startup(object sender, StartupEventArgs e) {
            string mutName = "GameServer";
            _mutex = new Mutex(true, mutName, out bool createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Приложение уже запущено!");
                this.Shutdown();
            }
        }
    }
}
