using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public LayerMask targetLayer;

        private Vector3 _previewPos;
        private void Awake()
        {
            _previewPos = transform.position;
        }
        private void Update()
        {
            //transform.position += new Vector3(1,0,0) * speed * Time.deltaTime;
            transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        }
        private void FixedUpdate()
        {
            Vector2 dir = (Vector2)(transform.position - _previewPos);
            float dist = dir.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(_previewPos, dir, dist, targetLayer);
            if(hit && hit.collider)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.TakeDamage(GameManager.Ins.player.stat.damage, GameManager.Ins.player);
                }
                gameObject.SetActive(false);
            }
            _previewPos = transform.position;
        }
    }

}