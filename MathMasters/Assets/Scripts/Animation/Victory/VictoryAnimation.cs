using DG.Tweening;
using UnityEngine;

namespace MathMasters.Animation
{
    public class VictoryAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _hero;
        [SerializeField] private float _firstAimTime = 0.75f;
        [SerializeField] private float _secondAimTime = 0.25f;
        [SerializeField] private ParticleSystem _particles;

        private Vector2 _startPosition;
        private Vector2 _endPosition;

        public void Show()
        {
            SetUpPosition();
            StartHeroAnimation();
            _particles.Play();
        }
        private void SetUpPosition()
        {
            _startPosition = new Vector2(-Screen.width, _hero.anchoredPosition.y);
            _endPosition = new Vector2(0, _hero.anchoredPosition.y);
        }
        private void StartHeroAnimation()
        {
            _hero.anchoredPosition = _startPosition;
            HeroIntroductionAnimation();
        }

        private void HeroIntroductionAnimation()
        {
            _hero.DOAnchorPosX(_endPosition.x, _firstAimTime).SetEase(Ease.OutExpo)
                .OnComplete(HeroJumpAnimation);
        }

        private void HeroJumpAnimation()
        {
            _hero.DOAnchorPosY(_hero.anchoredPosition.y + 50, _secondAimTime).SetEase(Ease.OutQuad)
                        .SetLoops(2, LoopType.Yoyo);
        }
    }
}
