using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class ObstacleCheck : MonoBehaviour
    {
        public LayerMask groundLayer;
        public LayerMask waterLayer;
        public LayerMask ladderLayer;
        public float deepWater; //khoang cach raycast, check deepWater
        public float checkingRadius; // ban kinh
        public Vector3 offSet; // Overlapcicle khoang cach + them o vi tri Player
        public Vector3 deepWaterOffset; // Raycast khoang cach + them tu` vi tri Player
        private bool _isOnGround;
        private bool _isOnWater;
        private bool _isOnLadder;
        private bool _isOnDeepWater;
        public bool IsOnGround { get => _isOnGround; }
        public bool IsOnWater { get => _isOnWater; }
        public bool IsOnLadder { get => _isOnLadder; }
        public bool IsOnDeepWater { get => _isOnDeepWater; }

        private void FixedUpdate()
        {
            _isOnGround = OverlapChecking(groundLayer);
            _isOnWater = OverlapChecking(waterLayer);
            _isOnLadder = OverlapChecking(ladderLayer);

            RaycastHit2D waterHit = Physics2D.Raycast(transform.position + deepWaterOffset, Vector2.up, deepWater, waterLayer);
            _isOnDeepWater = waterHit;

            //Debug.Log(IsOnDeepWater);
            //Debug.Log($"Ground: {_isOnGround} _Water: {_isOnWater} _Ladder: {_isOnLadder} _WaterDeep: {_isOnDeepWater}");
        }
        private bool OverlapChecking(LayerMask layerToCheck)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position + offSet, checkingRadius, layerToCheck);
            return col != null;
        }
        /*private void OnDrawGizmos()
        {
            Gizmos.color = Helper.ChangAlpha(Color.red, 0.4f);
            Gizmos.DrawSphere(transform.position + offSet, checkingRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + deepWaterOffset, new Vector3(transform.position.x + deepWaterOffset.x, transform.position.y + deepWaterOffset.y + deepWater, transform.position.z));
        }*/
    }

}