using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class ActorStat : ScriptableObject
    {
        [Header("Common:")]
        public int hp;
        public int moveSpeed;
        public int damage;
        [Header("Invincible:")]
        public float knockBackTime;
        public float knockBackForce;
        public float invincibleTime;
    }

}