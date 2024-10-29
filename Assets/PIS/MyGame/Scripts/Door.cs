using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class Door : MonoBehaviour
    {
        public int keyRequired;
        public Sprite openSp;
        public Sprite closeSp;

        private SpriteRenderer _sp;
        private bool _isOpened;

        public bool IsOpened { get => _isOpened; }

        private void Awake()
        {
            _sp = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            DoorCheck();
        }

        private void DoorCheck()
        {
            _isOpened = GameData.Ins.IsLevelUnlocked(LevelManager.Ins.CurLevelId + 1);
            _sp.sprite = _isOpened ? openSp : closeSp;
        }
        public void OpenDoor()
        {
            if (_isOpened)
            {
                GameManager.Ins.CurKey = 0;
                GameManager.Ins.LevelClear();
                GUIManager.Ins.UpdateKey(GameManager.Ins.CurKey);
                return;
            }
            if(GameManager.Ins.CurKey >= keyRequired)
            {
                GameManager.Ins.CurKey = 0;
                GameData.Ins.key = 0;
                GameData.Ins.UpdateLevelUnlocked(LevelManager.Ins.CurLevelId + 1, true);
                GameData.Ins.UpdateLevelPassed(LevelManager.Ins.CurLevelId, true);
                GameData.Ins.SaveData();
                GameManager.Ins.LevelClear();
                DoorCheck();
                GUIManager.Ins.UpdateKey(GameManager.Ins.CurKey);
            }
        }
    }
}