using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Timers;
using static Dane.Data;
using Timer = System.Timers.Timer;

namespace Logika
{
    internal class Logs
    {
        private readonly string _logFilePath;
        private readonly Timer _timer;
        private readonly List<Ball> _balls;
        private readonly List<object> _logEntries;

        public Logs(List<Ball> balls, string logFilePath)
        {
            _balls = balls;
            _logFilePath = logFilePath;
            _logEntries = new List<object>();

            ClearLogFile();

            _timer = new Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void ClearLogFile()
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, false))
            {
                writer.WriteLine("[]");
            }
        }

        private void TimerElapsed(Object source, ElapsedEventArgs e)
        {
            var logEntry = new
            {
                TimeStamp = e.SignalTime,
                Balls = _balls
            };

            _logEntries.Add(logEntry);

            string existingLogContent;
            using (StreamReader reader = new StreamReader(_logFilePath))
            {
                existingLogContent = reader.ReadToEnd();
            }

            var existingLogEntries = JsonSerializer.Deserialize<List<object>>(existingLogContent);

            existingLogEntries.Add(logEntry);

            string jsonString = JsonSerializer.Serialize(existingLogEntries, new JsonSerializerOptions { WriteIndented = true });
            using (StreamWriter writer = new StreamWriter(_logFilePath, false))
            {
                writer.WriteLine(jsonString);
            }
        }

        public void StopTimer()
        {
            _timer.Stop();
        }
    }
}
