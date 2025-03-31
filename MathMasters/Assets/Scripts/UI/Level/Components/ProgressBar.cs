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
        private const float ANIMATION_TIME = 0.7f;
        private const string VISUAL_ELEMENT_CLASS = "progress-bar";

        private const int COUNT = 10;
        private const int START_COUNT = 0;

        private float _deltaFill;
        private VisualElement _progressVE;
        [Inject] private UIDocument _uIDocument;

        public void Init(int count = COUNT, int startValue = START_COUNT)
        {
            SetUpElement();
            SetUpDeltaFill(count);
            SetProgress(startValue);
        }
        private void SetUpElement()
        {
            VisualElement root = _uIDocument.rootVisualElement;
            _progressVE = root.Q<VisualElement>(VISUAL_ELEMENT_CLASS);
        }
        private void SetUpDeltaFill(int count)
        {
            _deltaFill = (MAX_FILL - MIN_FILL) / count;
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
            DOTween.Kill(_progressVE);
            DOTween
                .To(
                    WightGetter,
                    WidthSetter,
                    value,
                    ANIMATION_TIME
                )
                .SetEase(Ease.OutQuad)
                .SetId(_progressVE);
        }

        private float WightGetter() => 
            _progressVE.style.width.value.value;

        private void WidthSetter(float x) =>
            _progressVE.style.width = new Length(x, LengthUnit.Percent);

    }
}
