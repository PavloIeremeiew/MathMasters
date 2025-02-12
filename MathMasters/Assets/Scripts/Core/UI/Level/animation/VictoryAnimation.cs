using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathMasters
{
    public class VictoryAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _hero; 
        [SerializeField] private float _firstAimTime=0.75f; 
        [SerializeField] private float _secondAimTime=0.25f; 

        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private void Start()
        {
            _startPosition = new Vector2(-Screen.width, _hero.anchoredPosition.y);
            _endPosition = new Vector2(0, _hero.anchoredPosition.y);

            AnimateHero();
        }

        private void AnimateHero()
        {
            _hero.anchoredPosition = _startPosition; 

            _hero.DOAnchorPosX(_endPosition.x, _firstAimTime).SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    _hero.DOAnchorPosY(_hero.anchoredPosition.y + 50, _secondAimTime).SetEase(Ease.OutQuad)
                        .SetLoops(2, LoopType.Yoyo);
                });
        }
    }
}
