using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PIS.PlatformGame
{
    public class GUIManager : Singleton<GUIManager>
    {
        public Text txtLiveCount;
        public Text txtHpCount;
        public Text txtCoinCount;
        public Text txtTimeCount;
        public Text txtKeyCount;
        public Text txtBulletCount;

        public GameObject mobileGamepad;

        public Dialog settingDialog;
        public Dialog pauseDialog;
        public Dialog levelClearDialog;
        public Dialog levelFailDialog;

        public override void Awake()
        {
            MakeSingleton(false);
        }
        public void UpdateTxt(Text txt, string content)
        {
            if(txt != null)
            {
                txt.text = content;
            }
        }
        public void UpdateLive(int live)
        {
            UpdateTxt(txtLiveCount, live.ToString());
        }
        public void UpdateHp(int hp)
        {
            UpdateTxt(txtHpCount, hp.ToString());
        }
        public void UpdateCoin(int coin)
        {
            UpdateTxt(txtCoinCount, coin.ToString());
        }
        public void UpdateTime(string time)
        {
            UpdateTxt(txtTimeCount, time.ToString());
        }
        public void UpdateKey(int key)
        {
            UpdateTxt(txtKeyCount, key.ToString());
        }
        public void UpdateBullet(int bullet)
        {
            UpdateTxt(txtBulletCount, bullet.ToString());
        }
        public void ShowMobileGamepad(bool isShow)
        {
            if(mobileGamepad != null)
            {
                mobileGamepad.SetActive(isShow);
            }
        }
    }
}