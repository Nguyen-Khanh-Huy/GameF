using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class CollectableLife : Collectable
    {
        protected override void TriggerHandle()
        {
            GameManager.Ins.CurLive += _bonus;
            GameData.Ins.life = GameManager.Ins.CurLive;
            GameData.Ins.SaveData();
            // update UI
        }
    }

}