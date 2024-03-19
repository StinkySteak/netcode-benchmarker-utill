using System.IO;
using System.Text;
using UnityEngine;

namespace StinkySteak.NetcodeBenchmark
{
    public class FrametimeLogger
    {
        private SimulationTimer.SimulationTimer _timerServerLog;
        private float _intervalServerLog = 1f;
        private string _filePath;

        private const int STRING_BUILDER_CAPACITY = 100;
        private StringBuilder _stringBuilder = new(STRING_BUILDER_CAPACITY);

        private INetcodeBenchmarkRunner _runner;

        public void Initialize(INetcodeBenchmarkRunner netcodeBenchmarkRunner)
        {
            _runner = netcodeBenchmarkRunner;
            _filePath = Application.persistentDataPath + $"/{Application.productName}ServerOutput.txt";
        }

        /// <summary>
        /// Tick update/cycle
        /// </summary>
        public void MonoUpdate()
        {
            if (!_timerServerLog.IsExpiredOrNotRunning()) return;

            int connectedClients = _runner.GetConnectedClients();
            float frameTime = _runner.GetAverageFrameTime();

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Average FrameTime: {0}ms. Connected Clients: {1}\n", frameTime, connectedClients);

            File.AppendAllText(_filePath, _stringBuilder.ToString());

            _timerServerLog = SimulationTimer.SimulationTimer.CreateFromSeconds(_intervalServerLog);
        }
    }
   
}