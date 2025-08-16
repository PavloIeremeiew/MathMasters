using MathMasters.Services;
using NUnit.Framework;

namespace Tests
{
    public class TimerTests
    {
        private Timer _timer = new Timer();

        [Test]
        public void TimerStartsCorrectly()
        {
            _timer.Start();
            Assert.IsTrue(_timer.IsRunning, "Timer should be running after Start() is called.");
        }

        [Test]
        public void TimerStopsCorrectly()
        {
            _timer.Start();
            _timer.Stop();
            Assert.IsFalse(_timer.IsRunning, "Timer should not be running after Stop() is called.");
        }

        [Test]
        public void IsRunningReturnsCorrectValue()
        {
            Assert.IsFalse(_timer.IsRunning, "Timer should not be running by default.");
            _timer.Start();
            Assert.IsTrue(_timer.IsRunning, "Timer should be running after Start() is called.");
            _timer.Stop();
            Assert.IsFalse(_timer.IsRunning, "Timer should not be running after Stop() is called.");
        }
    }
}
