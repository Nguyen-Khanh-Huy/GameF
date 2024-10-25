using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PIS.PlatformGame
{
    public class LevelClearDialog : Dialog
    {
        public Image[] stars;
        public Sprite activeStar;
        public Sprite unactiveStar;

        public Text txtLiveCount;
        public Text txtHpCount;
        public Text txtTimeCount;
        public Text txtCoinCount;

        public override void Show(bool isShow)
        {
            base.Show(isShow);
            if(stars != null && stars.Length > 0)
            {
                for(int i = 0; i < stars.Length; i++)
                {
                    var star = stars[i];
                    if(star != null)
                    {
                        star.sprite = unactiveStar;
                    }
                }
                for (int i = 0; i < GameManager.Ins.GoalStar; i++)
                {
                    var star = stars[i];
                    if(star != null)
                    {
                        star.sprite = activeStar;
                    }
                }
            }
            if(txtLiveCount)
            {
                txtLiveCount.text = $"x{GameManager.Ins.CurLive}";
            }
            if (txtHpCount)
            {
                txtHpCount.text = $"x{GameManager.Ins.player.CurHp}";
            }
            if (txtTimeCount)
            {
                txtTimeCount.text = $"{Helper.TimeConvert(GameManager.Ins.TimePlay)}";
            }
            if (txtCoinCount)
            {
                txtCoinCount.text = $"x{GameManager.Ins.CurCoin}";
            }
        }
        public void Replay()
        {
            Close();
            GameManager.Ins.Relplay();
        }
        public void NextLevel()
        {
            Close();
            GameManager.Ins.NextLevel();
        }
    }
}