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
                GameData.Ins.musicVol = AudioController.Ins.musicVolume;
                GameData.Ins.soundVol = AudioController.Ins.sfxVolume;
                GameData.Ins.SaveData();
                LevelManager.Ins.Init();
            }
            AudioController.Ins.SetMusicVolume(GameData.Ins.musicVol);
            AudioController.Ins.SetSoundVolume(GameData.Ins.soundVol);
            AudioController.Ins.PlayMusic(AudioController.Ins.menus);
            Pref.IsFirstTime = false;
        }
    }
}