using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UImanager
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _mixerName;
        private Slider _volumeSlider;
        private int _mixerNum;

        private void Awake()
        {
            _volumeSlider = GetComponent<Slider>();
            _mixerNum = _mixerName switch
            {
                "master" => 0,
                "music" => 1,
                "sfx" => 2,
                _ => 0
            };
        }

        private void Start()
        {

            LevelsData data = SaveSystem.LoadProgress();
            _volumeSlider.value = data.GetVolume(_mixerNum);
            SetVolume();
        }

        public void SetVolume()
        {
            float volume = Mathf.Log10(_volumeSlider.value) * 30;
            _audioMixer.SetFloat(_mixerName, volume);
            LevelsData data = SaveSystem.LoadProgress();
            data.SetVolume(_mixerNum, _volumeSlider.value);
            SaveSystem.SaveProgress(data);
        }
    }
}
