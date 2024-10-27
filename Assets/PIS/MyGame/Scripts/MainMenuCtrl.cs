using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class MainMenuCtrl : MonoBehaviour
    {
        private void Start()
        {
            if (!Pref.IsFirstTime)
            {
                GameData.Ins.LoadData();
            }
            else
            {
                //GameData.Ins.SaveData();
                LevelManager.Ins.Init();
            }
            Pref.IsFirstTime = false;
            AudioController.Ins.PlayMusic(AudioController.Ins.menus);
        }
    }
}