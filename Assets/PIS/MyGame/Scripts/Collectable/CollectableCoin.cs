using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class CollectableCoin : Collectable
    {
        protected override void TriggerHandle()
        {
            GameManager.Ins.AddCoins(_bonus);
        }
    }

}