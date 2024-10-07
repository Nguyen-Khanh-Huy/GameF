using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

namespace PIS.PlatformGame
{
    public class Enemy : Actor
    {
        [Header("Moving: ")]
        public float movingDist;

        protected EnemyDetect _enemyDetect;
        protected EnemyStat _enemyStat;
        protected Vector2 _movingDir; // huong
        protected Vector2 _movingDirBack;
        protected Vector2 _startingPos; // vi tri bat dau
        protected Vector2 _targetDir; // huong den player
        protected StateMachine<EnemyAnimState> _fsm;

        public bool IsDead { get => _fsm.State == EnemyAnimState.Dead; }
        protected override void Awake()
        {
            base.Awake();
            _enemyDetect = GetComponent<EnemyDetect>();
            _startingPos = transform.position;
        }
        public override void Start()
        {
            base.Start();
            //FSM_MethodGen.Gen<EnemyAnimState>();
        }
        protected virtual void Update()
        {
            if (IsDead)
            {
                _fsm.ChangeState(EnemyAnimState.Dead);
            }
            if (_isKnockBack || IsDead) return;
            if (_enemyDetect.IsDetected)
            {
                _fsm.ChangeState(EnemyAnimState.Chasing);
            }
            else
            {
                _fsm.ChangeState(EnemyAnimState.Moving);
            }
            if (_rb.velocity.y <= -50) 
            { 
                Dead(); 
            }
        }
        protected virtual void FixedUpdate()
        {
            if (_isKnockBack || IsDead) return;
            Move();
        }
        public virtual void Move()
        {

        }
        public override void TakeDamage(int dmg, Actor whoHit = null)
        {
            if(IsDead) return;
            base.TakeDamage(dmg, whoHit);
            if(_curHp > 0 && !_isInvincible)
            {
                _fsm.ChangeState(EnemyAnimState.GotHit);
            }

        }
        protected void FSMInit(MonoBehaviour mono)
        {
            _fsm = StateMachine<EnemyAnimState>.Initialize(mono);
            _fsm.ChangeState(EnemyAnimState.Moving);
        }
        protected override void Init()
        {
            base.Init();
            if(stat != null)
            {
                _enemyStat = (EnemyStat)stat;
            }
        }
        protected override void Dead()
        {
            base.Dead();
            _fsm.ChangeState(EnemyAnimState.Dead);
        }        
        protected void GetTargetDir() // huong den player
        {
            _targetDir = _enemyDetect.Target.transform.position - transform.position;
            _targetDir.Normalize();
        }

        //---------------------------------------------------------------------
        
        #region FSM
        protected virtual void Moving_Enter() { }
        protected virtual void Moving_Update() {
            _curSpeed = _enemyStat.moveSpeed;
            Helper.PlayAnim(_anim, EnemyAnimState.Moving.ToString());
        }
        protected virtual void Moving_Exit() { }
        protected virtual void Chasing_Enter() { }
        protected virtual void Chasing_Update() {
            _curSpeed = _enemyStat.chasingSpeed;
            Helper.PlayAnim(_anim, EnemyAnimState.Chasing.ToString());
        }
        protected virtual void Chasing_Exit() { }
        protected virtual void GotHit_Enter() { }
        protected virtual void GotHit_Update() { }
        protected virtual void GotHit_Exit() { }
        protected virtual void Dead_Enter() {
            if (deadVfxPb)
            {
                Instantiate(deadVfxPb, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
        protected virtual void Dead_Update() { }
        protected virtual void Dead_Exit() { }


        #endregion
    }

}