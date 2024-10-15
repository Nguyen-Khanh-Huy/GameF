using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PIS.PlatformGame
{
    public class SceneController : Singleton<SceneController>
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        public void LoadLevelScene(int level)
        {
            string sceneName = GameScene.Level_.ToString() + level;
            LoadScene(sceneName);
        }
    }

}