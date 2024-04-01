using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Переложить код в UI
namespace GameManager
{
    public class GoalsManager : MonoBehaviour
    {
        private Dictionary<string, int> _goals;
        private TMP_Text _goalsCounter;
        private Dictionary<string, string> _specialNames = new()
        {
            { "BounceEnemy", "SAWS" },
            { "FollowingEnemy", "GHOSTS" },
            { "ShootingEnemy", "TRUNKS" },
            { "SpikeEnemy", "SPIKES" }
        };

        private void Awake()
        {
            GameObject goalsObj = GameObject.FindGameObjectWithTag("GoalsCounter");
            _goalsCounter = goalsObj.GetComponent<TMP_Text>();
        }

        public void UpdateGoals(in Dictionary<string, int> goals)
        {
            string buffer = "";
            foreach (var (key, value) in goals)
            {
                buffer += $"{_specialNames[key]} LEFT:{value}\n\n";
            }
            _goalsCounter.text = buffer;
        }

        public void UpdateTimer(int timeLeft)
        {
            _goalsCounter.text = $"SURVIVE {timeLeft} SECONDS\n\n";
        }
    }
}
