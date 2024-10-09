using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class FishFreeMoving : FreeMovingEnemy
    {
        protected override void Update()
        {
            if (!GameManager.Ins.player.obstacle.IsOnWater)
            {
                _fsm.ChangeState(EnemyAnimState.Moving);
                return;
            }
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!GameManager.Ins.player.obstacle.IsOnWater)
            {
                _fsm.ChangeState(EnemyAnimState.Moving);
            }
        }
    }

}