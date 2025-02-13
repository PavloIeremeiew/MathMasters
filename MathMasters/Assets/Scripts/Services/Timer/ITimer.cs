using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathMasters.Services
{
    public interface ITimer 
    {
        public void Start();
        public void Stop();
        public string GetElapsedTime();
    }
}
