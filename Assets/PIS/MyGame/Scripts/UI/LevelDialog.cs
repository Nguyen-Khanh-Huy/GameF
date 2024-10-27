using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PIS.PlatformGame
{
    public class LevelDialog : Dialog
    {
        public Transform gridRoot;
        public LevelItemUI levelItemUIPb;
        public Text TxtcoinCount;
        public override void Show(bool isShow)
        {
            base.Show(isShow);
            UpdateLevelDialog();
        }

        private void UpdateLevelDialog()
        {
            if (TxtcoinCount)
            {
                TxtcoinCount.text = GameData.Ins.coin.ToString();
            }
            var levels = LevelManager.Ins.levels;
            if (levels == null || gridRoot == null || levelItemUIPb == null) return;
            { Helper.ClearChilds(gridRoot); }
            for (int i = 0; i < levels.Length; i++)
            {
                int levelIdx = i;
                var levelItem = levels[levelIdx];
                if(levelItem == null) continue;
                var itemUIClone = Instantiate(levelItemUIPb, Vector3.zero, Quaternion.identity);
                itemUIClone.transform.SetParent(gridRoot);
                itemUIClone.transform.localScale = Vector3.one;
                itemUIClone.transform.localPosition = Vector3.zero;
                itemUIClone.UpdateLevelItemUI(levelItem, levelIdx);
                if (itemUIClone.btnComp)
                {
                    itemUIClone.btnComp.onClick.RemoveAllListeners();
                    itemUIClone.btnComp.onClick.AddListener(() => ItemEvent(levelItem, levelIdx));
                }
            }
        }

        private void ItemEvent(LevelItem levelItem, int levelIdx)
        {
            if(levelItem == null) return;
            bool isUnlocked = GameData.Ins.IsLevelUnlocked(levelIdx);
            if (isUnlocked)
            {
                GameData.Ins.curLevelId = levelIdx;
                GameData.Ins.SaveData();

                LevelManager.Ins.CurLevelId = levelIdx;
                UpdateLevelDialog();

                SceneController.Ins.LoadLevelScene(levelIdx);
            }
            else
            {
                if(GameData.Ins.coin >= levelItem.price)
                {
                    GameData.Ins.coin -= levelItem.price;
                    GameData.Ins.curLevelId = levelIdx;
                    GameData.Ins.UpdateLevelUnlocked(levelIdx, true);
                    GameData.Ins.SaveData();

                    LevelManager.Ins.CurLevelId = levelIdx;
                    UpdateLevelDialog();

                    SceneController.Ins.LoadLevelScene(levelIdx);
                    AudioController.Ins.PlaySound(AudioController.Ins.unlock);
                }
                else
                {
                    Debug.Log("Don't enought coin");
                }
            }
        }
    }
}