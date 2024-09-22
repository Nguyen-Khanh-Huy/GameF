using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    [CreateAssetMenu(fileName ="Player Stat", menuName ="PIS/Player Stat")]
    public class PlayerStat : ActorStat
    {
        public float jumpForce;
        public float flyingSpeed;
        public float ladderSpeed;
        public float swimSpeed;
        public float attackRate;
    }

}