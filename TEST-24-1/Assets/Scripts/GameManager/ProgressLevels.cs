using Core;
using Data;
using GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressLevels : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        ScenarioManager.Win += LevelCompleted;
    }

    private void OnDisable()
    {
        ScenarioManager.Win -= LevelCompleted;
    }

    private void LevelCompleted()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        if (curScene < (int)ScenesEnum.lvl_last)
        {
            LevelsData levelsData = SaveSystem.LoadProgress();
            levelsData.UnlockLevel(curScene - (int)ScenesEnum.lvl_1 + 1);
            SaveSystem.SaveProgress(levelsData);
        }
    }
}
