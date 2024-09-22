using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    [CreateAssetMenu(fileName ="Gameplay Setting", menuName ="PIS/Gameplay Setting")]
    public class GameplaySetting : ScriptableObject
    {
        public bool IsOnMoble;
        public int startingLife;
        public int startingBullet;
    }
}
