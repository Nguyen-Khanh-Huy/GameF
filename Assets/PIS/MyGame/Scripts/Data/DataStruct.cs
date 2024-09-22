using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public enum Diretion
    {
        Left,
        Right,
        Up,
        Down,
        None
    }
    public enum GameState
    {
        Starting,
        Playing,
        Win,
        GameOver
    }
    public enum GamePref
    {
        GameData,
        IsFirstTime
    }
    public enum GameTag
    {
        Player,
        Enemy,
        MovingPlatform,
        Thorn,
        Collectable,
        CheckPoint,
        Door,
        DeadZone
    }
    public enum GameScene
    {
        MainMenu,
        GamePlay,
        Level
    }
    public enum SpriteOder
    {
        Nomal =5,
        InWater =2
    }
    public enum PlayerAnimState
    {
        SayHello,
        Walk,
        Jump,
        OnAir,
        Land,
        Swim,
        Shoot,
        Attack,
        Fly,
        FlyOnAir,
        SwimOnDeep,
        Ladder,
        Dead,
        Idle,
        Idle_Ladder,
        GotHit
    }
    public enum EnemyAnimState
    {
        Moving,
        Chasing,
        GotHit,
        Dead
    }
    public enum Detect
    {
        RayCast,
        CircleOverlap
    }
    public enum PlayerCollider
    {
        Default,
        Flying,
        InWater,
        None
    }
    public enum Collectable
    {
        Heart,
        Life,
        Bullet,
        Key,
        Coin,
        None
    }
}