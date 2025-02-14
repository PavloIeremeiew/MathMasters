using DG.Tweening;
using MathMasters.Entities;
using MathMasters.Services;
using UnityEngine;
using Zenject;

namespace MathMasters.Animation
{
    public class CorrectAnswerAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private RectTransform _hero;
        [SerializeField] private RectTransform _coin;
        [SerializeField] private ParticleSystem _particles;

        [Inject] private SoundManager _soundManager;
        [Inject] private LevelDataSignal _signal;


        private Vector2 _startHeroPos;
        private float _endHeroPosX;
        private Tween _delayedCall;

        private readonly Vector3 _startHeroRotation = Vector3.zero;
        private readonly Vector3 _endHeroRotation = new Vector3(0, 0, -15);

        private readonly Vector3 _startCoinScale = Vector3.zero;
        private readonly float _endCoinScale = 1;
        
        public void Hide()
        {
            _content.SetActive(false);
            _hero.DOKill();
            _coin.DOKill();
            _delayedCall.Kill();
        }

        public void Show()
        {
            _content.SetActive(true);
            _soundManager.PlayCorrectSound();
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            SetUpPlayer();
            SetUpCoin();
            TweenCallback coinAnim = CoinAnim;
            HeroAnim(coinAnim);
        }
        private void SetUpPlayer()
        {
            SetPlayerPos();
            _hero.anchoredPosition = _startHeroPos;
            _hero.rotation = Quaternion.Euler(_startHeroRotation);
        }
        private void SetPlayerPos()
        {
            _startHeroPos = new Vector2(-Screen.width / 2 - _hero.rect.width, _hero.anchoredPosition.y);
            _endHeroPosX = -Screen.width / 2 + _hero.rect.width * 0.25f;
        }
        private void SetUpCoin()
        {
            _coin.localScale = _startCoinScale;
        }

        private void HeroAnim(TweenCallback nextAnim)
        {
            _hero.DOAnchorPosX(_endHeroPosX, 0.8f).SetEase(Ease.OutBack)
                .OnComplete(nextAnim);
            _hero.DORotate(_endHeroRotation, 1f).SetEase(Ease.InOutSine);
        }

        private void CoinAnim()
        {
            if (_signal.IsReplay)
                return;

            _coin.DOScale(_endCoinScale, 0.6f).SetEase(Ease.OutBack).SetDelay(0.3f);
            _delayedCall=DOVirtual.DelayedCall(0.35f, ParticlesAnim);
        }
        private void ParticlesAnim()
        {            
                _soundManager.PlayCoinSound();
                _particles.Play();
        }
    }
}
