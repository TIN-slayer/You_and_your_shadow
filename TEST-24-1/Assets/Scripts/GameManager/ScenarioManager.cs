using Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UImanager;
using General;
using Tamplates;
using Player;
using Orbs;
namespace GameManager
{
    public class ScenarioManager : MonoBehaviour
    {
        public static event Action Win;
        private GoalsManager _goalsUI;
        private ObjectSpawner _spawner;
        [SerializeField] private Scenario _scenario;
        public Dictionary<string, int> _goalsDict = new();
        private float timer = 0;
        private bool _gameRunning = true;

        private void Awake()
        {
            _goalsUI = GetComponent<GoalsManager>();
            _spawner = GetComponent<ObjectSpawner>();
            // Сделано с учетом нулевого уровня!!!!!!!!!!!!
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
            OrbCollection.OrbCollected += ManageOrbCollection;
        }

        private void OnDisable()
        {
            EnemyHealth.EnemyDied -= ManageEnemyDeath;
            OrbCollection.OrbCollected -= ManageOrbCollection;
        }

        private void ManageOrbCollection(string orbTag)
        {
            _spawner.SpawnOrb(orbTag);
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
