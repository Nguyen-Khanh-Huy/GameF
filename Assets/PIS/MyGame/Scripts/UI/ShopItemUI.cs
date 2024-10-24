using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PIS.PlatformGame
{
    public class ShopItemUI : MonoBehaviour
    {
        public Text txtPrice;
        public Text txtCount;
        public Image preview;
        public Button btnComp;

        public void UpdateShopItemUI(ShopItem shopItem, int itemIdx)
        {
            if (shopItem == null) return;
            if(preview != null)
            {
                preview.sprite = shopItem.preview;
            }
            switch (shopItem.itemType)
            {
                case CollectableType.Heart:
                    if(txtCount != null)
                    {
                        txtCount.text = GameData.Ins.hp.ToString();
                    }
                    break;
                case CollectableType.Life:
                    if (txtCount != null)
                    {
                        txtCount.text = GameData.Ins.life.ToString();
                    }
                    break;
                case CollectableType.Bullet:
                    if (txtCount != null)
                    {
                        txtCount.text = GameData.Ins.bullet.ToString();
                    }
                    break;
                case CollectableType.Key:
                    if (txtCount != null)
                    {
                        txtCount.text = GameData.Ins.key.ToString();
                    }
                    break;
            }
            if(txtPrice != null)
            {
                txtPrice.text = shopItem.price.ToString();
            }
        }
    }
}