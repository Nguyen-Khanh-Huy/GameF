using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PIS.PlatformGame
{
    public class LevelFailDialog : Dialog
    {
        public Text txtTimeCount;
        public Text txtCoinCount;
        public override void Show(bool isShow)
        {
            base.Show(isShow);
            if (txtTimeCount != null)
            {
                txtTimeCount.text = $"x{Helper.TimeConvert(GameManager.Ins.TimePlay)}";
            }
            if(txtCoinCount != null)
            {
                txtCoinCount.text = $"x{GameManager.Ins.CurCoin}";
            }
        }
        public void Replay()
        {
            Close();
            GameManager.Ins.Relplay();
        }
        public void BackToMenu()
        {
            SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
        }
    }
}