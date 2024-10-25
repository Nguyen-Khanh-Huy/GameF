using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class PauseDialog : Dialog
    {
        public override void Show(bool isShow)
        {
            base.Show(isShow);
            Time.timeScale = 0f;
        }
        public override void Close()
        {
            Time.timeScale = 1f;
            base.Close();
        }
        public void Resume()
        {
            Close();
        }
        public void Replay()
        {
            Close();
            SceneController.Ins.LoadLevelScene(LevelManager.Ins.CurLevelId);
        }
        public void OpenSetting()
        {
            Close();
            if(GUIManager.Ins.settingDialog != null)
            {
                GUIManager.Ins.settingDialog.Show(true);
            }
        }
        public void Exit()
        {
            Close();
            SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
        }
    }
}