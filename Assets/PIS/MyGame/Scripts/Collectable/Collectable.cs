using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class Collectable : MonoBehaviour
    {
        public CollectableType collectableType;
        public int minBonus;
        public int maxBonus;
        public AudioClip collisionSfx;
        public GameObject destroyVfxPb;

        protected int _bonus;
        protected Player _player;
        private void Start()
        {
            _player = GameManager.Ins.player;
            if (!_player) return;
            _bonus = Random.Range(minBonus, maxBonus);
            Init();
        }
        public virtual void Init()
        {

        }
        protected virtual void TriggerHandle()
        {

        }
        public void Trigger()
        {
            TriggerHandle();
            if (destroyVfxPb)
            {
                Instantiate(destroyVfxPb, transform.position, Quaternion.identity);
            }
            // play sound
            Destroy(gameObject);
        }
    }

}