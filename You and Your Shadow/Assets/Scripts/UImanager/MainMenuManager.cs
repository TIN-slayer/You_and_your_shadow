using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UImanager
{
    public class MainMenuManager : MonoBehaviour
    {
        public void QuitButton()
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
    }
}
