using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class GamepadController : Singleton<GamepadController>
    {
        public float jumpHoldingTime; // time gioi han giu nut nhay
        private bool _canMoveLeft;
        private bool _canMoveRight;
        private bool _canMoveUp;
        private bool _canMoveDown;
        private bool _canJump;
        private bool _isJumpHolding;
        private bool _canFly;
        private bool _canSwim;
        private bool _canFire;
        private bool _canAttack;

        private bool _canCheckJumpHolding; // check player co dang giu nut nhay hay ko
        private float _curHoldingTime; // time hien tai khi giu nut nhay cua player

        public bool CanMoveLeft { get => _canMoveLeft; set => _canMoveLeft = value; }
        public bool CanMoveRight { get => _canMoveRight; set => _canMoveRight = value; }
        public bool CanMoveUp { get => _canMoveUp; set => _canMoveUp = value; }
        public bool CanMoveDown { get => _canMoveDown; set => _canMoveDown = value; }
        public bool CanJump { get => _canJump; set => _canJump = value; }
        public bool IsJumpHolding { get => _isJumpHolding; set => _isJumpHolding = value; }
        public bool CanFly { get => _canFly; set => _canFly = value; }
        public bool CanFire { get => _canFire; set => _canFire = value; }
        public bool CanAttack { get => _canAttack; set => _canAttack = value; }

        public bool IsStatic // ko lam gi
        {
            get => !_canMoveLeft && !_canMoveRight && !_canMoveUp && !_canMoveDown && !_canJump && !_canFly && !_isJumpHolding;
        }
        public override void Awake()
        {
            MakeSingleton(false);
        }
        private void Update()
        {
            if (!GameManager.Ins.setting.IsOnMoble)
            {
                float hozCheck = Input.GetAxisRaw("Horizontal");
                float vertCheck = Input.GetAxisRaw("Vertical");
                _canMoveLeft = hozCheck < 0 ? true : false;
                _canMoveRight = hozCheck > 0 ? true : false;
                _canMoveUp = vertCheck > 0 ? true : false;
                _canMoveDown = vertCheck < 0 ? true : false;

                _canJump = Input.GetKeyDown(KeyCode.Space);
                _canFly = Input.GetKey(KeyCode.F);

                _canFire = Input.GetKeyDown(KeyCode.C);
                _canAttack = Input.GetKeyDown(KeyCode.V);

                if (_canJump)
                {
                    _isJumpHolding = false;
                    _canCheckJumpHolding = true;
                    _curHoldingTime = 0;
                }
                if (_canCheckJumpHolding)
                {
                    _curHoldingTime += Time.deltaTime;
                    if (_curHoldingTime > jumpHoldingTime)
                    {
                        _isJumpHolding = Input.GetKey(KeyCode.Space);
                    }
                }
            }
            else
            {
                // Xu ly moble
            }
        }
    }
}