using System;
using System.IO;

namespace GameServerCore.MLogic {
    public class LogWriter : IDisposable
    {
        private static LogWriter _logWriter;
        private readonly string _directoryName = "logs";
        private string _fileName;
        private StreamWriter sw;

        private LogWriter()
        {
            CreateFileDir();
        }

        private void CreateFileDir()
        {
            string fullPath;

            if (_fileName == null)
                _fileName = "gameServer.log" + DateTime.Now.ToString("HHmmss dd-MM-yyyy") + ".txt";

            if (!Directory.Exists(_directoryName))
                Directory.CreateDirectory(_directoryName);

            fullPath = _directoryName + "\\" + _fileName;

            sw = new StreamWriter(fullPath, true, System.Text.Encoding.Default);
        }


        public static LogWriter GetInst() 
        {
            return _logWriter ?? (_logWriter = new LogWriter());
        }

        public void Dispose()
        {           
            sw.Close();
        }

        public async void WriteFileL(string msg) 
        {
            // TODO: Прописать вызовы для всех основных событий на сервере(вызов + описание)

            string logText = DateTime.Now.ToString("T") + " - " + msg;

            await sw.WriteLineAsync(logText);
            sw.Flush();
        }
    }
}
