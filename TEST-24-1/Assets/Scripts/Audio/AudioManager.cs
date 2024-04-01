using Enemies;
using GameManager;
using Player;
using UImanager;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _gameMusicSource;
        [SerializeField] private AudioSource _explosionSource;
        [SerializeField] private AudioSource _shootSource;
        [SerializeField] private AudioSource _clickSource;
        [SerializeField] private AudioSource _winSource;
        [SerializeField] private AudioSource _loseSource;
        [SerializeField] private AudioClip _explosion;
        [SerializeField] private AudioClip _win;
        [SerializeField] private AudioClip _lose;
        [SerializeField] private AudioClip _click;
        private bool _isAudioPaused = false;
        private int shooters = 0;

        private void Start()
        {
            foreach (var button in FindObjectsOfType<Button>(true))
            {
                button.onClick.AddListener(PlayClick);
            }
        }
        private void Update()
        {
            if (!_gameMusicSource.isPlaying && !_isAudioPaused)
            {
                _gameMusicSource.Play();
            }
            if (!_shootSource.isPlaying && shooters > 0 && !_isAudioPaused)
            {
                _shootSource.Play();
            }
            if (shooters <= 0)
            {
                _shootSource.Stop();
            }
        }

        private void OnEnable()
        {
            GeneralMenus.GamePause += PauseAudio;
            GeneralMenus.GameResume += PlayAudio;
            PlayerTeleport.Teleport += PlayExplosionSFX;
            ScenarioManager.Win += PlayWin;
            PlayerHealth.Lose += PlayLose;
            ShootingEnemyController.StartedShoot += AddShooter;
            EnemyHealth.EnemyDied += ManageEnemyDeath;
        }
        private void OnDisable()
        {
            GeneralMenus.GamePause -= PauseAudio;
            GeneralMenus.GameResume -= PlayAudio;
            PlayerTeleport.Teleport -= PlayExplosionSFX;
            ScenarioManager.Win -= PlayWin;
            PlayerHealth.Lose -= PlayLose;
            ShootingEnemyController.StartedShoot -= AddShooter;
            EnemyHealth.EnemyDied -= ManageEnemyDeath;
        }

        private void PauseAudio()
        {
            _gameMusicSource.Pause();
            _explosionSource.Pause();
            _shootSource.Pause();
            _isAudioPaused = true;
        }

        private void PlayAudio()
        {
            _gameMusicSource.UnPause();
            _explosionSource.UnPause();
            _shootSource.UnPause();
            _isAudioPaused = false;
        }

        private void PlayExplosionSFX(Vector3 buffer)
        {
            _explosionSource.PlayOneShot(_explosion);
        }

        private void PlayWin()
        {
            _winSource.PlayOneShot(_win);
        }
        private void PlayLose()
        {
            _loseSource.PlayOneShot(_lose);
        }

        private void AddShooter()
        {
            ++shooters;
        }
        private void ManageEnemyDeath(string tag)
        {
            if (tag == "ShootingEnemy")
            {
                --shooters;
            }
        }

        private void PlayClick()
        {
            _clickSource.PlayOneShot(_click);
        }
    }
}
