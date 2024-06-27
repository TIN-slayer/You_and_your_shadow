using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class SneakerEnemy : MonoBehaviour
    {
        private Transform _ghostOrbitAim;
        private Transform _playerTransform;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private float _regularSpeed;
        [SerializeField] private float _boostSpeed;
        private bool _killerMode = false;
        [SerializeField] private float _killerTime;
        private float _invDelay = 0.05f;
        private float _invTime = 0.6f;


        private void Start()
        {
            _ghostOrbitAim = GameObject.FindWithTag("GhostOrbitAim").transform;
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void OnEnable()
        {
            PlayerTeleport.Teleport += KillPlayer;
        }

        private void OnDisable()
        {
            PlayerTeleport.Teleport -= KillPlayer;
        }

        private void Update()
        {
            if (_ghostOrbitAim != null && !_killerMode)
            {
                transform.position = Vector2.MoveTowards(transform.position, _ghostOrbitAim.position, _regularSpeed * Time.deltaTime);
            }
            else if (_playerTransform != null && _killerMode)
            {
                transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _boostSpeed * Time.deltaTime);
            }
        }

        private void KillPlayer(TeleportArgs teleportArgs)
        {
            StartCoroutine(KillerMode());
            StartCoroutine(GetInvilnarable());
        }

        private IEnumerator KillerMode()
        {
            _killerMode = true;
            yield return new WaitForSeconds(_killerTime);
            _killerMode = false;
        }

        private IEnumerator GetInvilnarable()
        {
            yield return new WaitForSeconds(_invDelay);
            _health.GuardState = true;
            yield return new WaitForSeconds(_invTime);
            _health.GuardState = false;
        }
    }
}
