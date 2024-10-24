using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PIS.PlatformGame
{
    public class LevelItemUI : MonoBehaviour
    {
        public Image preview;
        public GameObject lokedArea;
        public GameObject checkMark;
        public Text txtPrice;
        public Button btnComp;

        public void UpdateLevelItemUI(LevelItem levelItem, int levelIdx)
        {
            if(levelItem == null) return;
            bool isUnlocked = GameData.Ins.IsLevelUnlocked(levelIdx);
            if(preview != null)
            {
                preview.sprite = levelItem.preview;
            }
            if (txtPrice)
            {
                txtPrice.text = levelItem.price.ToString();
            }
            if (lokedArea)
            {
                lokedArea.SetActive(!isUnlocked);
            }
            if (isUnlocked)
            {
                if(checkMark != null)
                {
                    checkMark.SetActive(GameData.Ins.curLevelId == levelIdx);
                }
            }
            else if(checkMark)
            {
                checkMark.SetActive(false);
            }
        }
    }
}