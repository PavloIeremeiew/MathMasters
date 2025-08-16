using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathMasters.Services
{
    public interface ITimer 
    {
        public bool IsRunning { get; }
        public TimeSpan ElapsedTime { get; }

        public void Start();
        public void Stop();
        public void Reset();
        public string GetElapsedTime();
    }
}
