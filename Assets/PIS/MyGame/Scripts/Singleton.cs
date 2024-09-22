using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _ins;
        public static T Ins { get { return _ins; } }
        public virtual void Awake()
        {
            MakeSingleton(true);
        }
        public void MakeSingleton(bool destroyOnload)
        {
            if (_ins == null)
            {
                _ins = this as T;
                if (destroyOnload)
                {
                    DontDestroyOnLoad(this.gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}