using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PIS.PlatformGame
{
    public class BtnSound : MonoBehaviour
    {
        private Button _btn;
        private void Awake()
        {
            _btn = GetComponent<Button>();
        }
        private void Start()
        {
            if(_btn != null)
            {
                _btn.onClick.RemoveAllListeners();
                _btn.onClick.AddListener(()=> PlaySound());
            }
        }
        private void PlaySound()
        {
            AudioController.Ins.PlaySound(AudioController.Ins.btnClick);
        }
    }
}