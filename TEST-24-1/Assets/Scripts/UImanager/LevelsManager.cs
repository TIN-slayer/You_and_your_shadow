using General;
using Data;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UImanager
{
    // МБ разделить логику
    public class LevelsManager : MonoBehaviour
    {
        public static int NumLevels;
        [SerializeField] private Sprite _unlocked;
        [SerializeField] private Sprite _locked;

        private void Awake()
        {
            NumLevels = transform.childCount;
        }

        private void Start()
        {
            SetUpLevels();
        }

        private void SetUpLevels()
        {
            LevelsData levelsData = SaveSystem.LoadProgress();
            for (int i = 0; i < NumLevels; i++)
            {
                int lvl = i;
                Transform child = transform.GetChild(i);
                if (levelsData.Levels[i])
                {
                    child.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(lvl); });
                    child.GetComponent<Button>().interactable = true;
                    child.GetComponent<Image>().sprite = _unlocked;
                    child.GetChild(0).GetComponent<TMP_Text>().text = (i + 1).ToString();
                }
                else
                {
                    child.GetComponent<Button>().onClick.RemoveAllListeners();
                    child.GetComponent<Button>().interactable = false;
                    child.GetComponent<Image>().sprite = _locked;
                    child.GetChild(0).GetComponent<TMP_Text>().text = "";
                }
            }
        }

        public void LoadLevel(int lvl)
        {
            SceneManager.LoadScene((int)ScenesEnum.lvl_1 + lvl);
        }

        public void ResetLevels()
        {
            LevelsData levelsData = SaveSystem.LoadProgress();
            levelsData.ResetAllLevels();
            SaveSystem.SaveProgress(levelsData);
            SetUpLevels();
        }

        public void UnlockLevels()
        {
            LevelsData levelsData = SaveSystem.LoadProgress();
            levelsData.UnlockAllLevels();
            SaveSystem.SaveProgress(levelsData);
            SetUpLevels();
        }
    }
}
