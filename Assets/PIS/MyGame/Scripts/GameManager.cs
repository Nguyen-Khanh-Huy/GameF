using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.SceneManagement;

namespace PIS.PlatformGame
{
    public class GameManager : Singleton<GameManager>
    {
        public GameplaySetting setting;
        public Player player;
        public FreeParallax map;

        private StateMachine<GameState> _fsm;

        private int _curLive;
        private int _curCoin;
        private int _curBullet;
        private int _curKey;
        private float _gameplayTime;
        private int _goalStar;

        public StateMachine<GameState> Fsm { get => _fsm; }
        public int CurLive { get => _curLive; set => _curLive = value; }
        public int CurCoin { get => _curCoin; set => _curCoin = value; }
        public int CurBullet { get => _curBullet; set => _curBullet = value; }
        public int CurKey { get => _curKey; set => _curKey = value; }
        public float TimePlay { get => _gameplayTime; }
        public int GoalStar { get => _goalStar; }

        public override void Awake()
        {
            MakeSingleton(false); //destroy doi tuong nay khi load sang since khac
            _fsm = StateMachine<GameState>.Initialize(this);
            _fsm.ChangeState(GameState.Playing);
        }
        private void Start()
        {
            LoadData();
            StartCoroutine(CamFollowDelay());
            if(setting.IsOnMoble)
            {
                GUIManager.Ins.ShowMobileGamepad(true);
            }
            else
            {
                GUIManager.Ins.ShowMobileGamepad(false);
            }
        }
        private void LoadData()
        {
            _curLive = setting.startingLife;
            _curBullet = setting.startingBullet;
            if(GameData.Ins.life != 0)
            {
                _curLive = GameData.Ins.life;
            }
            if (GameData.Ins.bullet != 0)
            {
                _curBullet = GameData.Ins.bullet;
            }
            if(GameData.Ins.key != 0)
            {
                _curKey = GameData.Ins.key;
            }
            if (GameData.Ins.hp != 0)
            {
                player.CurHp = GameData.Ins.hp;
            }
            Vector3 checkPoint = GameData.Ins.GetCheckPoint(LevelManager.Ins.CurLevelId);
            if(checkPoint != Vector3.zero)
            {
                player.transform.position = checkPoint;
            }
            float gameplayTime = GameData.Ins.GetPlayTime(LevelManager.Ins.CurLevelId);
            if(gameplayTime > 0)
            {
                _gameplayTime = gameplayTime;
            }
            GUIManager.Ins.UpdateLive(_curLive);
            GUIManager.Ins.UpdateHp(player.CurHp);
            GUIManager.Ins.UpdateCoin(_curCoin);
            GUIManager.Ins.UpdateTime(Helper.TimeConvert(_gameplayTime));
            GUIManager.Ins.UpdateBullet(_curBullet);
            GUIManager.Ins.UpdateKey(_curKey);
        }
        public void BackToCheckPoint()
        {
            player.transform.position = GameData.Ins.GetCheckPoint(LevelManager.Ins.CurLevelId);
        }
        public void Revice()
        {
            _curLive--;
            player.CurHp = player.stat.hp;
            GameData.Ins.hp = player.CurHp;
            GameData.Ins.SaveData();
        }
        public void AddCoins(int coins)
        {
            _curCoin += coins;
            GameData.Ins.coin += coins;
            GameData.Ins.SaveData();
            GUIManager.Ins.UpdateCoin(GameData.Ins.coin);
        }
        public void Relplay()
        {
            SceneController.Ins.LoadLevelScene(LevelManager.Ins.CurLevelId);
        }
        public void NextLevel()
        {
            LevelManager.Ins.CurLevelId++;
            if(LevelManager.Ins.CurLevelId >= SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
            }
            else
            {
                SceneController.Ins.LoadLevelScene(LevelManager.Ins.CurLevelId);
            }
        }
        public void SaveCheckPoint()
        {
            GameData.Ins.UpdatePlayTime(LevelManager.Ins.CurLevelId, _gameplayTime);
            GameData.Ins.UpdateCheckPoint(LevelManager.Ins.CurLevelId,
                new Vector3(player.transform.position.x - 0.5f,
                player.transform.position.y + 0.5f,
                player.transform.position.z));
            GameData.Ins.SaveData();
        }
        public void GameOver()
        {
            _fsm.ChangeState(GameState.GameOver);
            GameData.Ins.UpdateLevelScore(LevelManager.Ins.CurLevelId, Mathf.RoundToInt(_gameplayTime));
            GameData.Ins.SaveData();
            if (GUIManager.Ins.levelFailDialog != null)
            {
                GUIManager.Ins.levelFailDialog.Show(true);
            }
        }
        public void LevelClear()
        {
            _fsm.ChangeState(GameState.Win);
            GameData.Ins.UpdateLevelScore(LevelManager.Ins.CurLevelId, Mathf.RoundToInt(_gameplayTime));
            _goalStar = LevelManager.Ins.CurLevel.goal.GetStar(Mathf.RoundToInt(_gameplayTime));
            GameData.Ins.SaveData();
            if(GUIManager.Ins.levelClearDialog != null)
            {
                GUIManager.Ins.levelClearDialog.Show(true);
            }
        }
        private IEnumerator CamFollowDelay()
        {
            yield return new WaitForSeconds(0.3f);
            CameraFollow.ins.target = player.transform;
        }
        public void SetMapSpeed(float speed)
        {
            if (map)
            {
                map.Speed = speed;
            }
        }
        #region FSM
        protected void Starting_Enter() { }
        protected void Starting_Update() { }
        protected void Starting_Exit() { }
        protected void Playing_Enter() { }
        protected void Playing_Update() {
            if (GameData.Ins.IsLevelPassed(LevelManager.Ins.CurLevelId)) return;
            _gameplayTime += Time.deltaTime;
            GUIManager.Ins.UpdateTime(Helper.TimeConvert(_gameplayTime));
        }
        protected void Playing_Exit() { }
        protected void Win_Enter() { }
        protected void Win_Update() { }
        protected void Win_Exit() { }
        protected void GameOver_Enter() { }
        protected void GameOver_Update() { }
        protected void GameOver_Exit() { }
        #endregion
    }
}