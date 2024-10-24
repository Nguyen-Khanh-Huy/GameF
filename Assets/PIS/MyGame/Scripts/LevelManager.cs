using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class LevelManager : Singleton<LevelManager>
    {
        public LevelItem[] levels;
        private int _curLevelId;

        public int CurLevelId { get => _curLevelId; set => _curLevelId = value; }
        public LevelItem CurLevel
        {
            get => levels[_curLevelId];
        }
        public void Init()
        {
            if (levels == null || levels.Length <= 0) return;
            for(int i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                if(level != null)
                {
                    if (i == 0)
                    {
                        GameData.Ins.UpdateLevelUnlocked(i, true);
                        GameData.Ins.curLevelId = i;
                    }
                    else
                    {
                        GameData.Ins.UpdateLevelUnlocked(i, false);
                    }
                    GameData.Ins.UpdateLevelPassed(i, false);
                    GameData.Ins.UpdatePlayTime(i, 0f);
                    GameData.Ins.UpdateCheckPoint(i, Vector3.zero);
                    GameData.Ins.UpdateLevelScoreNoneCheck(i, 0);
                    GameData.Ins.SaveData();
                }
            }
        }
    }
}