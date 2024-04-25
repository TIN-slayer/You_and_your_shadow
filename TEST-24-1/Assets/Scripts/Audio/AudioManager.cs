using Enemies;
using GameManager;
using General;
using Player;
using System.Collections;
using UImanager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private float _explosionFadeTime;
        [SerializeField] private AudioSource _menuMusicSource;
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
        private int _musicMode = 0;
        private bool _isAudioPaused = false;
        private int shooters = 0;

        private static AudioManager instance = null;
        public static AudioManager Instance
        {
            get { return instance; }
        }

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            foreach (var button in FindObjectsOfType<Button>(true))
            {
                button.onClick.AddListener(PlayClick);
            }
            _menuMusicSource.Stop();
            _gameMusicSource.Stop();
            _shootSource.Stop();
            _winSource.Stop();
            _loseSource.Stop();
            _isAudioPaused = false;
            shooters = 0;
            if (SceneManager.GetActiveScene().buildIndex == (int)ScenesEnum.MainMenu)
            {
                _musicMode = 0;
                _menuMusicSource.Play();
            }
            else
            {
                _musicMode = 1;
                _gameMusicSource.Play();
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
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
            SceneManager.sceneLoaded -= OnSceneLoaded;
            GeneralMenus.GamePause -= PauseAudio;
            GeneralMenus.GameResume -= PlayAudio;
            PlayerTeleport.Teleport -= PlayExplosionSFX;
            ScenarioManager.Win -= PlayWin;
            PlayerHealth.Lose -= PlayLose;
            ShootingEnemyController.StartedShoot -= AddShooter;
            EnemyHealth.EnemyDied -= ManageEnemyDeath;
        }

        private void Update()
        {
            if (_musicMode == 1)
            {
                if (!_shootSource.isPlaying && shooters > 0 && !_isAudioPaused)
                {
                    _shootSource.Play();
                }
                if (shooters <= 0)
                {
                    _shootSource.Stop();
                }
            }
        }

        private void PauseAudio()
        {
            StartCoroutine(FadeVolume(_explosionSource, _explosionFadeTime));
            _shootSource.Pause();
            _isAudioPaused = true;
        }

        private void PlayAudio()
        {
            _shootSource.UnPause();
            _isAudioPaused = false;
        }

        private void PlayExplosionSFX(Vector3 buffer)
        {
            _explosionSource.PlayOneShot(_explosion);
        }

        private void PlayWin()
        {
            _gameMusicSource.Stop();
            _winSource.PlayOneShot(_win);
        }

        private void PlayLose()
        {
            _gameMusicSource.Stop();
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

        private IEnumerator FadeVolume(AudioSource audioSource, float fadeTime)
        {
            float timer = fadeTime;
            while (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                if (timer < 0.0f)
                {
                    timer = 0.0f;
                }
                audioSource.volume = timer / fadeTime;
                yield return 0;
            }
            audioSource.volume = 1f;
        }
    }
}
