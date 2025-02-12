using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MathMasters
{
    public class ProgressBar : MonoBehaviour
    {
        private const float MIN_FILL = 0.05f;
        private const float MAX_FILL = 1f;
        private const float ANIMATION_TIME = 0.7f;

        private const int COUNT = 10;
        private const int START_COUNT = 0;
        

        private float _deltaFill;

        [SerializeField] private Image _progress;

        public void Init(int count = COUNT,int startValue = START_COUNT)
        {
            _deltaFill = (MAX_FILL - MIN_FILL) / count;
            SetProgress(startValue);
        }

        public void SetProgress(int value)
        {
            float targetFill = CalcutValue(value);
            AnimChenges(targetFill);
        }
        private float CalcutValue(int value)
        {
            return MIN_FILL + value * _deltaFill;
        }
        private void AnimChenges(float value)
        {
            _progress.DOKill();
            _progress.DOFillAmount(value, ANIMATION_TIME).SetEase(Ease.OutQuad);
        }
    }
}
