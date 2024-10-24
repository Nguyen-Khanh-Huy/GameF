using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class CollectableKey : Collectable
    {
        protected override void TriggerHandle()
        {
            GameManager.Ins.CurKey += _bonus;
            GameData.Ins.key = GameManager.Ins.CurKey;
            GameData.Ins.SaveData();
            //update UI
        }
    }

}