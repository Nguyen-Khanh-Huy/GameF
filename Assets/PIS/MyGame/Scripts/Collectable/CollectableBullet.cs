using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class CollectableBullet : Collectable
    {
        protected override void TriggerHandle()
        {
            base.TriggerHandle();
            GameManager.Ins.CurBullet += _bonus;
            GameData.Ins.bullet = GameManager.Ins.CurBullet;
            GameData.Ins.SaveData();
            //update UI
        }
    }

}