using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class LineMoving : MonoBehaviour
    {
        public Diretion moveDir;
        public float movingDist; // quang duong can di chuyen(bat dau -> ket thuc)
        public float speed;
        public bool isOnlyUp;
        public bool isAuto; // tu dong chay

        private Vector2 _dest;// vi tri dich den
        private Vector3 _backDir;// vi tri quay ve
        private Vector3 _startingPos; // vi tri ban dau
        private Rigidbody2D _rb;

        public Vector2 Dest { get => _dest; }
        public Vector3 BackDir { get => _backDir; }
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _startingPos = transform.position;
        }
        private void Start()
        {
            GetMovingDest();
        }
        private void Update()
        {// Vi enemy lien tuc move, nen tranform.position lien tuc thay doi
            _backDir = _startingPos - transform.position;
            _backDir.Normalize();
        }
        private void FixedUpdate()
        {
            if (!isAuto) return;
            Move();
            SwitchCheck();
        }
        public void GetMovingDest() // xac dinh dich' den
        {
            switch(moveDir)
            {
                case Diretion.Left:
                    _dest = new Vector2(_startingPos.x - movingDist, transform.position.y);
                    break;
                case Diretion.Right:
                    _dest = new Vector2(_startingPos.x + movingDist, transform.position.y);
                    break;
                case Diretion.Up:
                    _dest = new Vector2(transform.position.x, _startingPos.y + movingDist);
                    break;
                case Diretion.Down:
                    _dest = new Vector2(transform.position.x, _startingPos.y - movingDist);
                    break;
            }
        }
        public bool IsReached() // enemy da den dich roi
        {
            float dist1 = Vector2.Distance(_startingPos, transform.position);
            float dist2 = Vector2.Distance(_startingPos, _dest);
            return dist1 > dist2;
        }
        public void SwitchDir(Vector2 dir) // thay doi huong di chuyen
        {
            if(moveDir == Diretion.Left || moveDir == Diretion.Right)
            {
                moveDir = dir.x < 0 ? Diretion.Left : Diretion.Right;
            }
            else if(moveDir == Diretion.Down || moveDir == Diretion.Up)
            {
                moveDir = dir.y < 0 ? Diretion.Down : Diretion.Up;
            }
        }
        public void SwitchCheck()
        {// neu da den' dich' -> thay doi huong va xac dinh dich' den'
            if (IsReached())
            {
                SwitchDir(_backDir);
                GetMovingDest();
            }
        }
        public void Move()
        {
            switch (moveDir)
            {
                case Diretion.Left:
                    _rb.velocity = new Vector2(-speed, _rb.velocity.y);
                    //transform.position = new Vector2(transform.position.x, _startingPos.y);
                    break;
                case Diretion.Right:
                    _rb.velocity = new Vector2(speed, _rb.velocity.y);
                    //transform.position = new Vector2(transform.position.x, _startingPos.y);
                    break;
                case Diretion.Up:
                    _rb.velocity = new Vector2(_rb.velocity.x, speed);
                    transform.position = new Vector2(_startingPos.x, transform.position.y);
                    break;
                case Diretion.Down:
                    if (isOnlyUp) return;
                    _rb.velocity = new Vector2(_rb.velocity.x, -speed);
                    transform.position = new Vector2(_startingPos.x, transform.position.y);
                    break;
            }
        }
        /*private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            _startingPos = transform.position;
            GetMovingDest();
            Gizmos.DrawLine(transform.position, _dest);
        }*/
    }

}