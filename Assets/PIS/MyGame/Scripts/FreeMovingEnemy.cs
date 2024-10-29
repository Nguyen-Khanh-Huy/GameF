using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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
        protected override void Update()
        {
            base.Update();
            GetTargetDir();
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
                _movingDir.Normalize();
                _movingDirBack = _movingDir;
                _haveMovingPos = true;
            }
            float angle = 0f;
            if (canRotate)
            {
                angle = Mathf.Atan2(_movingDir.y, _movingDir.x) * Mathf.Rad2Deg;
            }
            if (_movingDir.x > 0)
            {
                if (canRotate)
                {
                    //angle = Mathf.Clamp(angle, -41, 41);
                    angle = Mathf.Clamp(angle, -89, 89);
                    transform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
                Flip(Diretion.Right);
            } 
            else if (_movingDir.x < 0)
            {
                if (canRotate)
                {
                    float newAngle = angle + 180f;
                    //newAngle = Mathf.Clamp(newAngle, 25, 325);
                    newAngle = Mathf.Clamp(newAngle, 0, 360);
                    transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
                }
                Flip(Diretion.Left);
            }
            DestReachedChecking();
        }
        private void DestReachedChecking()
        {
            //Debug.Log(Vector2.Distance(transform.position, _movingPos));
            if(Vector2.Distance(transform.position, _movingPos) <= 0.5f)
            {
                _haveMovingPos = false;
            }
            else
            {
                _rb.velocity = _movingDir * _curSpeed;
            }
        }
        //--------------------------------------------------------------------
        #region FSM
        protected override void Moving_Enter()
        {
            base.Moving_Enter();
            FindMaxMovePos();
            _haveMovingPos = false;
            //Debug.Log(_movingDir);
        }
        protected override void Chasing_Update()
        {
            base.Chasing_Update();
            _movingDir = _targetDir;
        }
        protected override void GotHit_Update()
        {
            if (_isKnockBack)
            {
                KnockBackMove(_targetDir.y);
            }
            else
            {
                _fsm.ChangeState(EnemyAnimState.Moving);
            }
        }

        #endregion
        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + movingDist, transform.position.y, transform.position.z));
            
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - movingDist, transform.position.y, transform.position.z));

        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - movingDist, transform.position.z));

        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + movingDist, transform.position.z));

        //    Gizmos.color = Helper.ChangAlpha(Color.black, 0.5f);
        //    Gizmos.DrawSphere(_movingPos, 0.2f);
        //}
    }

}