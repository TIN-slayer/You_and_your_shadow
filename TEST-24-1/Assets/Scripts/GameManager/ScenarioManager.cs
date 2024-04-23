using Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UImanager;
using Core;
using Tamplates;
using Player;
namespace GameManager
{
    public class ScenarioManager : MonoBehaviour
    {
        public static event Action Win;
        private GoalsManager _goalsUI;
        private EnemySpawner _spawner;
        [SerializeField] private Scenario _scenario;
        public Dictionary<string, int> _goalsDict = new();
        private float timer = 0;
        private bool _gameRunning = true;

        private void Awake()
        {
            _goalsUI = GetComponent<GoalsManager>();
            _spawner = GetComponent<EnemySpawner>();
            // ������� � ������ �������� ������!!!!!!!!!!!!
            if (!_scenario._isTimer)
            {
                foreach (var el in _scenario._goals)
                {
                    _goalsDict[el.name] = el.val;
                }
            }
            else
            {
                timer = _scenario._timeGoal;
            }
        }

        private void Start()
        {
            if (!_scenario._isTimer)
            {
                _goalsUI.UpdateGoals(_goalsDict);
            }
        }

        private void Update()
        {
            if (_scenario._isTimer)
            {
                timer -= Time.deltaTime;
                _goalsUI.UpdateTimer(Mathf.RoundToInt(timer));
                if (timer <= 0 && _gameRunning)
                {
                    _gameRunning = false;
                    Win.Invoke();
                }
            }
        }

        private void OnEnable()
        {
            EnemyHealth.EnemyDied += ManageEnemyDeath;
        }

        private void OnDisable()
        {
            EnemyHealth.EnemyDied -= ManageEnemyDeath;
        }

        private void ManageEnemyDeath(string enemyTag)
        {
            _spawner.SpawnEnemy(enemyTag);
            if (!_scenario._isTimer)
            {
                if (_goalsDict.ContainsKey(enemyTag))
                {
                    if (_goalsDict[enemyTag] > 0)
                    {
                        --_goalsDict[enemyTag];
                        _goalsUI.UpdateGoals(_goalsDict);
                    }
                }
                bool goalCompleted = true;
                foreach (var goal in _goalsDict)
                {
                    if (goal.Value > 0)
                    {
                        goalCompleted = false;
                    }
                }
                if (goalCompleted)
                {
                    Win.Invoke();
                }
            }
        }
    }
}