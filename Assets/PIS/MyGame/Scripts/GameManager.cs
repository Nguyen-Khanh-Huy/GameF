using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class GameManager : Singleton<GameManager>
    {
        public GameplaySetting setting;
        public Player player;
        public override void Awake()
        {
            MakeSingleton(false); //destroy doi tuong nay khi load sang since khac
        }
    }
}