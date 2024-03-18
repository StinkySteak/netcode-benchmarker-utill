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

        public void Initialize()
        {
            _filePath = Application.persistentDataPath + $"/{Application.productName}ServerOutput.txt";
        }

        public void MonoUpdate(int connectedClients, float averageFrameTime)
        {
            if (!_timerServerLog.IsExpiredOrNotRunning()) return;

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Average FrameTime: {0}ms. Connected Clients: {1}\n", averageFrameTime, connectedClients);

            File.AppendAllText(_filePath, _stringBuilder.ToString());

            _timerServerLog = SimulationTimer.SimulationTimer.CreateFromSeconds(_intervalServerLog);
        }
    }
}