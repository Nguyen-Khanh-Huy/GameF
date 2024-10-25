using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class CollectableHp : Collectable
    {
        protected override void TriggerHandle()
        {
            _player.CurHp += _bonus;
            GameData.Ins.hp = _player.CurHp;
            GameData.Ins.SaveData();
            GUIManager.Ins.UpdateHp(GameData.Ins.hp);
        }
    }

}