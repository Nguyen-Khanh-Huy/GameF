using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class FireBullet : MonoBehaviour
    {
        public Player player;
        public Transform firePoint;
        public Bullet bulletPb;

        private float _curSpeed;
        public void Fire()
        {
            if (!bulletPb || !player || !firePoint || GameManager.Ins.CurBullet <= 0) return;
            _curSpeed = player.IsFaceLeft ? -bulletPb.speed : bulletPb.speed;
            var bulletClone = Instantiate(bulletPb, firePoint.position, Quaternion.identity);
            bulletClone.speed = _curSpeed;
            GameManager.Ins.ReduceBullet();
            AudioController.Ins.PlaySound(AudioController.Ins.fireBullet);
        }
    }

}