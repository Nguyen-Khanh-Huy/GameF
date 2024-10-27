using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PIS.PlatformGame
{
    public class ShopDialog : Dialog
    {
        public Transform gridRoot;
        public ShopItemUI shopItemUIPb;
        public Text txtCoinCount;

        public override void Show(bool isShow)
        {
            base.Show(isShow);
            UpdateShopDialog();
        }

        private void UpdateShopDialog()
        {
            if(txtCoinCount != null)
            {
                txtCoinCount.text = GameData.Ins.coin.ToString();
            }
            var shopItems = ShopManager.Ins.items;
            if (shopItems != null || shopItems.Length <= 0)
            { Helper.ClearChilds(gridRoot); }
            for (int i = 0; i < shopItems.Length; i++)
            {
                int shopItemIdx = i;
                var shopItem = shopItems[i];
                if(shopItem != null)
                {
                    var itemUIClone = Instantiate(shopItemUIPb, Vector3.zero, Quaternion.identity);
                    itemUIClone.transform.SetParent(gridRoot);
                    itemUIClone.transform.localPosition = Vector3.zero;
                    itemUIClone.transform.localScale = Vector3.one;
                    itemUIClone.UpdateShopItemUI(shopItem, shopItemIdx);
                    if (itemUIClone.btnComp)
                    {
                        itemUIClone.btnComp.onClick.RemoveAllListeners();
                        itemUIClone.btnComp.onClick.AddListener(() => ItemEvent(shopItem, shopItemIdx));
                    }
                }
            }
        }

        private void ItemEvent(ShopItem shopItem, int shopItemIdx)
        {
            if (shopItem == null) return;
            if(GameData.Ins.coin >= shopItem.price)
            {
                GameData.Ins.coin -= shopItem.price;
                switch (shopItem.itemType)
                {
                    case CollectableType.Heart:
                        GameData.Ins.hp++;
                        break;
                    case CollectableType.Life:
                        GameData.Ins.life++;
                        break;
                    case CollectableType.Bullet:
                        GameData.Ins.bullet++;
                        break;
                    case CollectableType.Key:
                        GameData.Ins.key++;
                        break;
                }
                GameData.Ins.SaveData();
                UpdateShopDialog();
                AudioController.Ins.PlaySound(AudioController.Ins.buy);
            }
            else
            {
                Debug.Log("Don't have enough Coin");
            }
        }
    }
}