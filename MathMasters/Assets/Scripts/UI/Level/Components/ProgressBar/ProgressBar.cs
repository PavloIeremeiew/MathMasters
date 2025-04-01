using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace MathMasters.UI
{
    public class ProgressBar : MonoBehaviour
    {
        private const float MIN_FILL = 5f;
        private const float MAX_FILL = 100f;

        private const int COUNT = 10;
        private const int START_COUNT = 0;

        private float _deltaFill;

        [Inject]private ProgressBarUI _progressBarUI;

        public void Init(int count = COUNT, int startValue = START_COUNT)
        {
            SetUpDeltaFill(count);
            SetProgress(startValue);
        }
     
        private void SetUpDeltaFill(int count)
        {
            _deltaFill = (MAX_FILL - MIN_FILL) / count;
        }

        public void SetProgress(int value)
        {
            float targetFill = CalcutValue(value);
            _progressBarUI.Set(targetFill);
        }
        private float CalcutValue(int value)
        {
            return MIN_FILL + value * _deltaFill;
        }
    }
}
