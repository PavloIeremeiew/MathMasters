using DG.Tweening;
using MathMasters.UI;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MathMasters
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private  float _unlockAminTime = 0.7f;
        [SerializeField] private  float _doneAminTime = 0.7f;
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _lock;
        [SerializeField] private Sprite _done;
        [SerializeField] private Sprite _active;
        [SerializeField] private Image _unlockAnimImage;
        [SerializeField] private Image _doneAnimImage;

        private Image _image;

        private void Awake()
        {
            _image = _button.GetComponent<Image>();
        }

        public void Active(UnityAction action)
        {
            setActive(action, _active);
        }

        public void Lock()
        {
            _button.onClick.RemoveAllListeners();
            _image.sprite = _lock;
            _button.interactable = false;
        }
        public void Done(UnityAction action)
        {
            setActive(action, _done);
        }
        private void setActive(UnityAction action, Sprite sprite)
        {
            _button.onClick.AddListener(action);
            _image.sprite = sprite;
            _button.interactable = true;
        }
        public void BeforUnLockAnim()
        {
            _unlockAnimImage.fillAmount = 1;
            _unlockAnimImage.gameObject.SetActive(true);
        }

        public void UnLockAnim()
        {
            Anim(_unlockAnimImage, _unlockAminTime, AfterUnlockAnim);
        }
        private void AfterUnlockAnim()
        {
        }
        public void DoneAnim(UnityAction action)
        {
            Anim(_doneAnimImage, _doneAminTime,()=> AfterDoneAnim(action));
        }
        private void AfterDoneAnim(UnityAction action)
        {
            action?.Invoke();
        }
        private void Anim(Image image, float time, UnityAction action)
        {

            image.fillAmount = 1;
            image.gameObject.SetActive(true);
            image.DOFillAmount(0, time).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                image.gameObject.SetActive(false);
                action?.Invoke();
            });
        }


    }
}
