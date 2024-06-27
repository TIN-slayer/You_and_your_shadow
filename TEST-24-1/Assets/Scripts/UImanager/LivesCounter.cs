using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player;

namespace UImanager
{
    public class LivesCounter : MonoBehaviour
    {
        private TMP_Text _livesCounter;

        private void Awake()
        {
            _livesCounter = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            PlayerHealth.LivesChanged += UpdateLives;
        }

        private void OnDisable()
        {
            PlayerHealth.LivesChanged -= UpdateLives;
        }

        public void UpdateLives(LivesChangedArgs arg)
        {
            _livesCounter.text = $"LIVES: {arg.lives}";
        }
    }
}
