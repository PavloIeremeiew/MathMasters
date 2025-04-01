using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace MathMasters
{
    public class ProgressBarUI
    {
        private const float ANIMATION_TIME = 0.7f;
        private const string VISUAL_ELEMENT_CLASS = "progress-bar";

        private readonly VisualElement _progressVE;

        [Inject]
        public ProgressBarUI(UIDocument uIDocument)
        {
            VisualElement root = uIDocument.rootVisualElement;
            _progressVE = root.Q<VisualElement>(VISUAL_ELEMENT_CLASS);
        }

        public void Set(float value)
        {
            AnimChenges(value);
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
