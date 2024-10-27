using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class Hammer : MonoBehaviour
    {
        public LayerMask enemyLayer;
        public float attackRadius;
        public Vector3 offset;
        [SerializeField]
        private Player _player;

        public void AttackCamera()
        {
            CamShake.ins.ShakeTrigger(0.3f, 0.1f, 1);
            AudioController.Ins.PlaySound(AudioController.Ins.attack);
        }
        public void AudioWalk()
        {
            AudioController.Ins.PlaySound(AudioController.Ins.footStep);
        }
        public void Attack()
        {
            if(_player == null) return;
            Collider2D col = Physics2D.OverlapCircle(transform.position + offset, attackRadius, enemyLayer);
            if(col != null)
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if(enemy != null )
                {
                    enemy.TakeDamage(_player.stat.damage, _player);
                }
            }
        }
        private void Update()
        {
            if(_player == null) return;
            if(_player.transform.localScale.x > 0)
            {
                if(offset.x < 0)
                {
                    offset = new Vector3(offset.x * -1, offset.y, offset.z);
                }
            }
            else if(_player.transform.localScale.x < 0)
            {
                if (offset.x > 0)
                {
                    offset = new Vector3(offset.x * -1, offset.y, offset.z);
                }
            }
        }
        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Helper.ChangAlpha(Color.green, 0.4f);
        //    Gizmos.DrawSphere(transform.position + offset, attackRadius);
        //}
    }

}