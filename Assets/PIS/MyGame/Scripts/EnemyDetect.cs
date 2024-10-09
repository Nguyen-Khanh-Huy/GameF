using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class EnemyDetect : MonoBehaviour
    {
        public bool disable;
        public Detect detect;
        public LayerMask targetLayer;
        public float detectDist; // khoang cach

        private Player _target;
        private Vector2 _dirToTarget;
        private bool _isDetected;

        public Player Target { get => _target; }
        public Vector2 DirToTarget { get => _dirToTarget; }
        public bool IsDetected { get => _isDetected; }

        private void Start()
        {
            _target = GameManager.Ins.player;
        }
        private void FixedUpdate()
        {
            if (!_target || disable) return;
            if(detect == Detect.RayCast)
            {
                _dirToTarget = _target.transform.position - transform.position;
                _dirToTarget.Normalize();
                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(_dirToTarget.x, 0f), detectDist, targetLayer);
                _isDetected = hit;
            }else if(detect == Detect.CircleOverlap)
            {
                Collider2D col = Physics2D.OverlapCircle(transform.position, detectDist, targetLayer);
                _isDetected = col;
            }
            //if (_isDetected) { Debug.Log("Da va cham"); }
        }
        /*private void OnDrawGizmos()
        {
            if(detect == Detect.RayCast)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + detectDist, transform.position.y, transform.position.z));
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - detectDist, transform.position.y, transform.position.z));
            }else if(detect == Detect.CircleOverlap)
            {
                Gizmos.color = Helper.ChangAlpha(Color.green, 0.2f);
                Gizmos.DrawSphere(transform.position, detectDist);
            }
        }*/
    }

}