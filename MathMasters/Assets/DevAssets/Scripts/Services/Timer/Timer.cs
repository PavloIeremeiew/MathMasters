using System;

namespace MathMasters.Services
{
    public class Timer : ITimer
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private bool _isRunning;
        private TimeSpan _elapsedTime;

        public bool IsRunning => _isRunning;
        public TimeSpan ElapsedTime => _elapsedTime;

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
                _elapsedTime += _endTime - _startTime;
            }
        }

        public void Reset()
        {
            _elapsedTime = TimeSpan.Zero;
            _isRunning = false;
        }

        public string GetElapsedTime()
        {
            TimeSpan elapsed = _isRunning ? DateTime.Now - _startTime + _elapsedTime : _elapsedTime;
            return $"{(int)elapsed.TotalMinutes:D2}:{elapsed.Seconds:D2}";
        }
    }
}
