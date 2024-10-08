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
            movingDist = lineMoving.movingDist;
        }
        public override void Move()
        {
            //base.Move();
            if (_isKnockBack) return;
            lineMoving.Move();
            Flip(lineMoving.moveDir);
        }
        #region FSM
        protected override void Moving_Update()
        {
            base.Moving_Update();
            _targetDir = lineMoving.BackDir;
            lineMoving.speed = _curSpeed;
            lineMoving.SwitchCheck();
        }
        protected override void Chasing_Enter()
        {
            base.Chasing_Enter();
            GetTargetDir();
            lineMoving.SwitchDir(_targetDir);
        }
        protected override void Chasing_Update()
        {
            base.Chasing_Update();
            GetTargetDir();
            lineMoving.SwitchDir(_targetDir);
            lineMoving.speed = _curSpeed;
        }
        protected override void Chasing_Exit()
        {
            base.Chasing_Exit();
            lineMoving.SwitchCheck();
        }
        protected override void GotHit_Update()
        {
            base.GotHit_Update();
            lineMoving.SwitchCheck();
            GetTargetDir();
            if (_isKnockBack)
            {
                KnockBackMove(0.55f);
            }
            else
            {
                _fsm.ChangeState(EnemyAnimState.Moving);
            }
        }
        #endregion
    }
}