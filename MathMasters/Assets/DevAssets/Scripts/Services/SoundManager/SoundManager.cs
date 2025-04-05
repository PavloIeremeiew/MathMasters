using UnityEngine;

namespace MathMasters.Services
{


    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _effectsSource;

        [Header("Sound Clips")]
        [SerializeField] private AudioClip _coinSound;
        [SerializeField] private AudioClip _winSound;
        [SerializeField] private AudioClip _selectSound;
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioClip _correctSound;
        [SerializeField] private AudioClip _wrongSound;

        public void PlayCoinSound() => PlaySound(_coinSound, 0.2f);
        public void PlayWinSound() => PlaySound(_winSound, 0.4f);
        public void PlaySelectSound() => PlaySound(_selectSound, 0.1f);
        public void PlayClickSound() => PlaySound(_clickSound);
        public void PlayCorrectSound() => PlaySound(_correctSound, 0.2f);
        public void PlayWrongSound() => PlaySound(_wrongSound, 0.4f);

        private void PlaySound(AudioClip clip, float vol = 1)
        {
                _effectsSource.volume = vol;
                _effectsSource.PlayOneShot(clip);
        }
        public void Stop()
        {
            if (_effectsSource.isPlaying)
            {
                _effectsSource.Stop();
            }
        }
    }
}
