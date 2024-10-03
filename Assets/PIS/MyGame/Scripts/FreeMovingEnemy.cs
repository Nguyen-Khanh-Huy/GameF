using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class FreeMovingEnemy : Enemy
    {
        public bool canRotate; // xoay

        private float _movePosXR;
        private float _movePosXL;
        private float _movePosYD;
        private float _movePosYT;

        private bool _haveMovingPos; // Check: Co toa do di chuyen Den hay chua
        private Vector2 _movingPos; // toa do can di chuyen den

        protected override void Awake()
        {
            base.Awake();
            FSMInit(this);
        }
        public void FindMaxMovePos()
        {// tim vi tri Max ma` Enemy di chuyen den
            _movePosXR = _startingPos.x + movingDist;
            _movePosXL = _startingPos.x - movingDist;
            _movePosYD = _startingPos.y - movingDist;
            _movePosYT = _startingPos.y + movingDist;
        }
        public override void Move()
        {
            if (_isKnockBack) return;
            if (!_haveMovingPos)
            {
                float randPosX = Random.Range(_movePosXL, _movePosXR);
                float randPosY = Random.Range(_movePosYD, _movePosYT);
                _movingPos = new Vector2(randPosX, randPosY);
                _movingDir = _movingPos - (Vector2)transform.position;
                _movingPos.Normalize();
            }
        }
    }

}