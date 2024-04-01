using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class UImixer : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        private string[] mixers = { "master", "music", "sfx" };

        private void Start()
        {
            LevelsData data = SaveSystem.LoadProgress();
            for (int i = 0; i < mixers.Length; i++)
            {
                float volume = Mathf.Log10(data.GetVolume(i)) * 30;
                print($"{mixers[i]} {volume}");
                _audioMixer.SetFloat(mixers[i], volume);
            }
        }
    }
}
