using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    [RequireComponent(typeof(LineMoving))]
    public class LineMovingEnemy : Enemy
    {
        private LineMoving lineMoving;
        protected override void Awake()
        {
            base.Awake();
            lineMoving = GetComponent<LineMoving>();
            FSMInit(this);
        }
        public override void Start()
        {
            base.Start();
            lineMoving.movingDist = movingDist;
        }
        public override void Move()
        {
            base.Move();
            if (_isKnockBack) return;
            lineMoving.Move();
            Flip(lineMoving.moveDir);
        }
    }

}