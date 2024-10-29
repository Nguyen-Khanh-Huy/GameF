using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PIS.PlatformGame
{
    public class SettingDialog : Dialog
    {
        public Slider musicSlider;
        public Slider soundSlider;
        public override void Show(bool isShow)
        {
            base.Show(isShow);
            Time.timeScale = 0f;
            if(musicSlider != null)
            {
                musicSlider.value = GameData.Ins.musicVol;
                AudioController.Ins.SetMusicVolume(GameData.Ins.musicVol);
            }
            if (soundSlider != null)
            {
                soundSlider.value = GameData.Ins.soundVol;
                AudioController.Ins.SetSoundVolume(GameData.Ins.soundVol);
            }
        }
        public void OnMusicChange(float value)
        {
            AudioController.Ins.SetMusicVolume(value);
        }
        public void OnSoundChange(float value)
        {
            AudioController.Ins.SetSoundVolume(value);
        }
        public void Save()
        {
            GameData.Ins.musicVol = AudioController.Ins.musicVolume;
            GameData.Ins.soundVol = AudioController.Ins.sfxVolume;
            GameData.Ins.SaveData();
            Close();
        }
        public override void Close()
        {
            base.Close();
            Time.timeScale = 1f;
        }
    }
}