using System;

namespace MathMasters.Services
{
    public class Timer : ITimer
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private bool _isRunning;

        public void Start()
        {
            _startTime = DateTime.Now;
            _isRunning = true;
        }

        public void Stop()
        {
            if (_isRunning)
            {
                _endTime = DateTime.Now;
                _isRunning = false;
            }
        }

        public string GetElapsedTime()
        {
            TimeSpan elapsed = _isRunning ? DateTime.Now - _startTime : _endTime - _startTime;
            return $"{(int)elapsed.TotalMinutes:D2}:{elapsed.Seconds:D2}";
        }
    }
}
