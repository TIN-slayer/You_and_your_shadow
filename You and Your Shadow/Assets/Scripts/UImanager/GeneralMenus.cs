using General;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameManager;
using Player;
using System;
using UnityEngine.UI;
using TMPro;

namespace UImanager
{
    public class GeneralMenus : MonoBehaviour
    {
        private bool _isPaused;
        private bool _isEndGameScreenActive;
        [SerializeField] private bool _isControlScreenActive;
        [SerializeField] private GameObject _shadeScreen;
        [SerializeField] private GameObject _controlsMenu;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _winMenu;
        [SerializeField] private GameObject _loseMenu;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TextMeshProUGUI _goalCounter;
        public static event Action GamePause;
        public static event Action GameResume;

        private void Start()
        {
            if (_isControlScreenActive)
            {
                Pause(_controlsMenu);
            }
        }

        private void OnEnable()
        {
            ScenarioManager.Win += LoadWinScreen;
            PlayerHealth.Lose += LoadLoseScreen;
        }

        private void OnDisable()
        {
            ScenarioManager.Win -= LoadWinScreen;
            PlayerHealth.Lose -= LoadLoseScreen;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel") && !_isEndGameScreenActive)
            {
                if (_isControlScreenActive)
                {
                    CloseControlScreen();
                }
                else if (_isPaused)
                {
                    Resume(_pauseMenu);
                }
                else
                {
                    Pause(_pauseMenu);
                }
            }
            if (Input.GetButtonDown("Reset") && _isPaused && !_isControlScreenActive)
            {
                ReplayLevel();
            }
        }

        public void Resume(GameObject menu = null)
        {
            _shadeScreen.SetActive(false);
            menu?.SetActive(false);
            Time.timeScale = 1;
            _isPaused = false;
            GameResume?.Invoke();
            _pauseButton.interactable = true;
            _goalCounter.color = Color.white;
        }

        public void Pause(GameObject menu = null)
        {
            _shadeScreen.SetActive(true);
            menu?.SetActive(true);
            Time.timeScale = 0;
            _isPaused = true;
            GamePause?.Invoke();
            _pauseButton.interactable = false;
            _goalCounter.color = new Color32(171, 171, 171, 255);
        }

        public void LoadLevelsMenu()
        {
            MenuMode.MenuType = 1;
            Time.timeScale = 1;
            SceneManager.LoadScene((int)ScenesEnum.MainMenu);
        }

        public void LoadNextLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex < (int)ScenesEnum.lvl_1 + (LevelsManager.NumLevels - 1))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                LoadLevelsMenu();
            }
        }

        public void ReplayLevel()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void LoadWinScreen()
        {
            _isEndGameScreenActive = true;
            Pause(_winMenu);
        }

        private void LoadLoseScreen()
        {
            _isEndGameScreenActive = true;
            Pause(_loseMenu);
        }

        public void CloseControlScreen()
        {
            _isControlScreenActive = false;
            Resume(_controlsMenu);
        }
    }
}
