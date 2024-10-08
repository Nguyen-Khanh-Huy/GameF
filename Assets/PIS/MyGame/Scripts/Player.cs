using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

namespace PIS.PlatformGame
{
    public class Player : Actor
    {
        private StateMachine<PlayerAnimState> _fsm;
        [Header("Smooth Jumping Setting:")]
        [Range(0f, 5f)]
        public float jumpFallMultipiler = 2.5f;
        [Range(0f, 5f)]
        public float jumpLowMultipiler = 2.5f;

        [Header("References:")]
        public SpriteRenderer sp;
        public ObstacleCheck obstacle;
        public CapsuleCollider2D defaulCol;
        public CapsuleCollider2D flyingCol;
        public CapsuleCollider2D inWaterCol;

        private PlayerStat _playerStat;
        private PlayerAnimState _prevState; // luu lai tthai truoc do, khi chuyen tthai moi
        private float _waterFallTime = 1f; // time cho phep khi o duoi nuoc
        private float _attackTime; // tgian tre khi tan cong
        private bool _isAttacked; // da tan cong hay chua

        private bool IsDead
        {
            get => _fsm.State == PlayerAnimState.Dead || _prevState == PlayerAnimState.Dead;
        }
        private bool IsJumping
        {
            get => _fsm.State == PlayerAnimState.Jump || _fsm.State == PlayerAnimState.OnAir || _fsm.State == PlayerAnimState.Land;
        }
        private bool IsFlying
        {
            get => _fsm.State == PlayerAnimState.OnAir || _fsm.State == PlayerAnimState.Fly || _fsm.State == PlayerAnimState.FlyOnAir;
        }
        private bool IsAttacking
        {
            get => _fsm.State == PlayerAnimState.Attack || _fsm.State == PlayerAnimState.Bullet;
        }
        protected override void Awake()
        {
            base.Awake();
            _fsm = StateMachine<PlayerAnimState>.Initialize(this);
            _fsm.ChangeState(PlayerAnimState.Idle);
            //FSM_MethodGen.Gen<PlayerAnimState>();
        }
        private void Update()
        {
            ActionHandle();
        }
        private void FixedUpdate()
        {
            SmoothJump();
        }
        private void ActionHandle()
        {
            if (IsAttacking || _isKnockBack) return;
            if (GamepadController.Ins.IsStatic)
            {
                _rb.velocity = new Vector2(0f, _rb.velocity.y);
            }
            if (obstacle.IsOnLadder && _fsm.State != PlayerAnimState.LadderIdle && _fsm.State != PlayerAnimState.Ladder)
            {
                ChangeState(PlayerAnimState.LadderIdle);
            }
            if (!obstacle.IsOnWater)
            {
                AttackCheck();
            }
            DelayActionRate(ref _isAttacked, ref _attackTime, _playerStat.attackRate);
        }
        protected override void Init()
        {
            base.Init();
            if(stat != null)
            {
                _playerStat = (PlayerStat)stat;
            }
        }
        protected override void Dead()
        {
            base.Dead();
            if (IsDead) return;
            ChangeState(PlayerAnimState.Dead);
        }
        private void Move(Diretion dir)
        {
            if(_isKnockBack) return;
            _rb.isKinematic = false;
            if(dir == Diretion.Left || dir == Diretion.Right)
            {
                Flip(dir);
                _hozDir = dir == Diretion.Left ? -1 : 1;
                _rb.velocity = new Vector2(_hozDir * _curSpeed, _rb.velocity.y);
            }else if(dir == Diretion.Up || dir == Diretion.Down)
            {
                _vertDir = dir == Diretion.Down ? -1 : 1;
                _rb.velocity = new Vector2(_rb.velocity.x, _vertDir * _curSpeed);
            }
        }
        private void WaterCheck()
        {
            if (obstacle.IsOnLadder) return;
            if (obstacle.IsOnDeepWater) // o duoi sau
            {
                _rb.gravityScale = 0f;
                _rb.velocity = new Vector2(_rb.velocity.x, 0f);
                ChangeState(PlayerAnimState.SwimOnDeep);
            }
            else if(obstacle.IsOnWater && !IsJumping) // o tren mat nuoc
            {
                _waterFallTime -= Time.deltaTime;
                if(_waterFallTime <= 0)
                {
                    _rb.gravityScale = 0f;
                    _rb.velocity = Vector2.zero;
                }
                GamepadController.Ins.CanMoveUp = false;
                ChangeState(PlayerAnimState.Swim);
            }
        }
        private void JumpCheck()
        {
            if (GamepadController.Ins.CanJump)
            {
                Jump();
                ChangeState(PlayerAnimState.Jump);
            }
        }
        private void Jump()
        {
            GamepadController.Ins.CanJump = false;
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.isKinematic = false;
            _rb.gravityScale = _startingGravity;
            _rb.velocity = new Vector2(_rb.velocity.x, _playerStat.jumpForce);
        }
        private void SmoothJump()
        {
            if (obstacle.IsOnGround || obstacle.IsOnWater && IsJumping) return;
            if(_rb.velocity.y < 0)
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpFallMultipiler - 1) * Time.deltaTime;
            }
            else if(_rb.velocity.y > 0 && !GamepadController.Ins.IsJumpHolding)
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpLowMultipiler - 1) * Time.deltaTime;
            }
        }
        private void AttackCheck()
        {
            if (GamepadController.Ins.CanAttack)
            {
                if (_isAttacked) return;
                    ChangeState(PlayerAnimState.Attack);
            }else if (GamepadController.Ins.CanFire)
            {
                ChangeState(PlayerAnimState.Bullet);
            }
        }
        private void HozMoveCheck()
        {
            if(GamepadController.Ins.CanMoveLeft)
                Move(Diretion.Left);
            if(GamepadController.Ins.CanMoveRight)
                Move(Diretion.Right);
        }
        private void VertMoveCheck()
        {
            if (IsJumping) return;
            if (GamepadController.Ins.CanMoveUp)
            {
                Move(Diretion.Up);
            }
            else if (GamepadController.Ins.CanMoveDown)
            {
                Move(Diretion.Down);
            }
            GamepadController.Ins.CanFly = false;
        }
        public void ChangeState(PlayerAnimState state)
        {
            _prevState = _fsm.State;
            _fsm.ChangeState(state);
        }
        private IEnumerator ChangeStateDelayAnim(PlayerAnimState newState, float timeExtra = 0)
        {
            var animClip = Helper.GetClip(_anim, _fsm.State.ToString());
            if (animClip)
            {
                yield return new WaitForSeconds(animClip.length + timeExtra);
                ChangeState(newState);
            }
            yield return null;
        }
        private void ChangeStateDelay(PlayerAnimState newState, float timeExtra = 0)
        {
            StartCoroutine(ChangeStateDelayAnim(newState, timeExtra));
        }
        private void ActiveCol(PlayerCollider Col)
        {
            if(defaulCol)
                defaulCol.enabled = Col == PlayerCollider.Default;
            if (flyingCol)
                flyingCol.enabled = Col == PlayerCollider.Flying;
            if(inWaterCol)
                inWaterCol.enabled = Col == PlayerCollider.InWater;
        }
        public override void TakeDamage(int dmg, Actor whoHit = null)
        {
            if(IsDead) return;
            base.TakeDamage(dmg, whoHit);
            if(_curHp > 0 && !_isInvincible)
            {
                ChangeState(PlayerAnimState.GotHit);
            }
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(GameTag.Enemy.ToString()))
            {
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if(enemy != null)
                {
                    TakeDamage(enemy.stat.damage, enemy);
                }
            }
        }
        // ----------------------------------------------------------------------------

        #region FSM
        private void SayHello_Enter() { }
        private void SayHello_Update() {
            Helper.PlayAnim(_anim, PlayerAnimState.SayHello.ToString());
        }
        private void SayHello_Exit() { }
        private void Walk_Enter() {
            ActiveCol(PlayerCollider.Default);
            _curSpeed = stat.moveSpeed;
        }
        private void Walk_Update() {
            JumpCheck();
            if (!GamepadController.Ins.CanMoveLeft && !GamepadController.Ins.CanMoveRight)
            {
                ChangeState(PlayerAnimState.Idle);
            }
            if (!obstacle.IsOnGround) {
                ChangeState(PlayerAnimState.OnAir);
            }
            HozMoveCheck();
            Helper.PlayAnim(_anim, PlayerAnimState.Walk.ToString());
        }
        private void Walk_Exit() { }
        private void Jump_Enter() {
            ActiveCol(PlayerCollider.Default);
        }
        private void Jump_Update() {
            _rb.isKinematic = false;
            if(_rb.velocity.y < -2 && !obstacle.IsOnGround)
            {
                ChangeState(PlayerAnimState.OnAir);
            }
            HozMoveCheck();
            Helper.PlayAnim(_anim, PlayerAnimState.Jump.ToString());
        }
        private void Jump_Exit() { }
        private void OnAir_Enter() {
            ActiveCol(PlayerCollider.Default);
        }
        private void OnAir_Update() {
            if (obstacle.IsOnGround)
            {
                ChangeState(PlayerAnimState.Land);
            }
            if (GamepadController.Ins.CanFly)
            {
                ChangeState(PlayerAnimState.Fly);
            }
            _rb.gravityScale = _startingGravity;
            HozMoveCheck();

            if (obstacle.IsOnWater)
            {
                _rb.velocity = new Vector2(0f, _rb.velocity.y);
                WaterCheck();
            }
            Helper.PlayAnim(_anim, PlayerAnimState.OnAir.ToString());
        }
        private void OnAir_Exit() { }
        private void Land_Enter() {
            ActiveCol(PlayerCollider.Default);
            ChangeStateDelay(PlayerAnimState.Idle);
        }
        private void Land_Update() {
            _rb.velocity = Vector2.zero;
            Helper.PlayAnim(_anim, PlayerAnimState.Land.ToString());
        }
        private void Land_Exit() { }
        private void Swim_Enter() {
            _curSpeed = _playerStat.swimSpeed;
            ActiveCol(PlayerCollider.InWater);
        }
        private void Swim_Update() {
            JumpCheck();
            WaterCheck();
            HozMoveCheck();
            VertMoveCheck();
            Helper.PlayAnim(_anim, PlayerAnimState.Swim.ToString());
        }
        private void Swim_Exit() {
            _waterFallTime = 1f;
        }
        private void Bullet_Enter() {
            ChangeStateDelay(PlayerAnimState.Idle);
        }
        private void Bullet_Update() {
            Helper.PlayAnim(_anim, PlayerAnimState.Bullet.ToString());
        }
        private void Bullet_Exit() { }
        private void Attack_Enter() {
            _isAttacked = true;
            ChangeStateDelay(PlayerAnimState.Idle);
        }
        private void Attack_Update() {
            _rb.velocity = Vector2.zero;
            Helper.PlayAnim(_anim, PlayerAnimState.Attack.ToString());
        }
        private void Attack_Exit() { }
        private void Fly_Enter() {
            ActiveCol(PlayerCollider.Flying);
            ChangeStateDelay(PlayerAnimState.FlyOnAir);
        }
        private void Fly_Update() {
            _rb.velocity = new Vector2(_rb.velocity.x, -_playerStat.flyingSpeed);
            if (obstacle.IsOnWater)
            {
                _rb.velocity = new Vector2(0f, _rb.velocity.y);
                WaterCheck();
            }
            HozMoveCheck();
            Helper.PlayAnim(_anim, PlayerAnimState.Fly.ToString());
        }
        private void Fly_Exit() { }
        private void FlyOnAir_Enter() {
            ActiveCol(PlayerCollider.Flying);
        }
        private void FlyOnAir_Update() {
            _rb.velocity = new Vector2(_rb.velocity.x, -_playerStat.flyingSpeed);
            if (obstacle.IsOnGround)
            {
                ChangeState(PlayerAnimState.Land);
            }
            if (!GamepadController.Ins.CanFly)
            {
                ChangeState(PlayerAnimState.OnAir);
            }
            if (obstacle.IsOnWater)
            {
                _rb.velocity = new Vector2(0f, _rb.velocity.y);
                WaterCheck();
            }
            HozMoveCheck();
            Helper.PlayAnim(_anim, PlayerAnimState.FlyOnAir.ToString());
        }
        private void FlyOnAir_Exit() { }
        private void SwimOnDeep_Enter() {
            ActiveCol(PlayerCollider.InWater);
            _curSpeed = _playerStat.swimSpeed;
            _rb.velocity = Vector2.zero;
        }
        private void SwimOnDeep_Update() {
            WaterCheck();
            HozMoveCheck();
            VertMoveCheck();
            Helper.PlayAnim(_anim, PlayerAnimState.SwimOnDeep.ToString());
        }
        private void SwimOnDeep_Exit() {
            _rb.velocity = Vector2.zero;
            GamepadController.Ins.CanMoveUp = false;
        }
        private void Ladder_Enter() {
            _rb.velocity = Vector2.zero;
            ActiveCol(PlayerCollider.Default);
        }
        private void Ladder_Update() {
            if (GamepadController.Ins.CanMoveUp || GamepadController.Ins.CanMoveDown && !GamepadController.Ins.CanMoveLeft && !GamepadController.Ins.CanMoveRight)
            {
                _rb.velocity = new Vector2(0f, _rb.velocity.y);
            }
            if (!GamepadController.Ins.CanMoveUp && !GamepadController.Ins.CanMoveDown)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0f);
                ChangeState(PlayerAnimState.LadderIdle);
            }            
            if (!obstacle.IsOnLadder) 
            {
                ChangeState(PlayerAnimState.OnAir);
            }
            GamepadController.Ins.CanFly = false;
            _rb.gravityScale = 0f;
            VertMoveCheck();
            HozMoveCheck();
            Helper.PlayAnim(_anim, PlayerAnimState.Ladder.ToString());
        }
        private void Ladder_Exit() { }
        private void Dead_Enter() { }
        private void Dead_Update() {
            Helper.PlayAnim(_anim, PlayerAnimState.Dead.ToString());
        }
        private void Dead_Exit() { }
        private void Idle_Enter() {
            ActiveCol(PlayerCollider.Default);
        }
        private void Idle_Update() {
            JumpCheck();
            if (GamepadController.Ins.CanMoveLeft || GamepadController.Ins.CanMoveRight)
            {
                ChangeState(PlayerAnimState.Walk);
            }
            Helper.PlayAnim(_anim, PlayerAnimState.Idle.ToString());
        }
        private void Idle_Exit() { }
        private void LadderIdle_Enter() {
            _rb.velocity = Vector2.zero;
            _curSpeed = _playerStat.ladderSpeed;
            ActiveCol(PlayerCollider.Default);
        }
        private void LadderIdle_Update() {
            if (GamepadController.Ins.CanMoveUp || GamepadController.Ins.CanMoveDown)
            {
                ChangeState(PlayerAnimState.Ladder);
            }
            if (!obstacle.IsOnLadder) {
                ChangeState(PlayerAnimState.OnAir);
            }
            GamepadController.Ins.CanFly = false;
            _rb.gravityScale = 0;
            HozMoveCheck();
            Helper.PlayAnim(_anim, PlayerAnimState.LadderIdle.ToString());
        }
        private void LadderIdle_Exit() { }
        private void GotHit_Enter() { }
        private void GotHit_Update() {
            if (_isKnockBack)
            {
                KnockBackMove(0.25f);
            }
            else if (obstacle.IsOnWater)
            {
                ChangeState(_prevState);
            }
            else
            {
                ChangeState(PlayerAnimState.Idle);
            }
        }
        private void GotHit_Exit() { }

        #endregion
    }

}