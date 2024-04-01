using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class UIAudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _clickSource;
        [SerializeField] private AudioClip _click;

        private void Start()
        {
            foreach (var button in FindObjectsOfType<Button>(true))
            {
                button.onClick.AddListener(PlayClick);
            }
        }

        private void PlayClick()
        {
            _clickSource.PlayOneShot(_click);
        }
    }
}
