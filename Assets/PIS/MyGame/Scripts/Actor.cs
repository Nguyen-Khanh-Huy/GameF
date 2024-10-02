using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace PIS.PlatformGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Actor : MonoBehaviour
    {
        [Header("Common:")]
        public ActorStat stat;

        [Header("Layer:")]
        [LayerList] public int normalLayer;
        [LayerList] public int invincibleLayer;
        [LayerList] public int deadLayer;

        [Header("Reference:")]
        [SerializeField] 
        protected Animator _anim;
        protected Rigidbody2D _rb;

        [Header("VFX:")]
        public GameObject deadVfxPb;

        protected Actor _whoHit;

        protected int _curHp;
        protected bool _isKnockBack;
        protected bool _isInvincible; // phan cong
        protected float _startingGravity; // trong luc trai dat
        protected bool _isFaceLeft;
        protected float _curSpeed;
        protected int _hoz, _vert;

        public int CurHp { get => _curHp; set => _curHp = value; }
        public float CurSpeed { get => _curSpeed; }
        public bool IsFaceLeft { get => _isFaceLeft; }
        
        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            if(_rb)
                _startingGravity = _rb.gravityScale;
            if (stat == null) return;
            _curHp = stat.hp;
            _curSpeed = stat.moveSpeed;
        }
        public virtual void Start()
        {
            Init();
        }
        protected virtual void Init()
        {
            
        }
        public virtual void TakeDamage(int dmg, Actor whoHit = null)
        {
            if(_isInvincible || _isKnockBack) return;
            if(_curHp > 0)
            {
                _whoHit = whoHit;
                _curHp -= dmg;
                if (_curHp <= 0)
                {
                    _curHp = 0;
                    Dead();
                }
                KnockBack();
            }
        }

        protected void KnockBack()
        {
            if (_isInvincible || _isKnockBack || !gameObject.activeInHierarchy) return;
            _isKnockBack = true;
            StartCoroutine(StopKnockBack());
        }
        protected IEnumerator StopKnockBack()
        {
            yield return new WaitForSeconds(stat.knockBackTime);
            _isKnockBack = false;
            _isInvincible = true;
            gameObject.layer = invincibleLayer;
            StartCoroutine(StopInvincible(stat.invincibleTime));
        }
        protected IEnumerator StopInvincible(float time)
        {
            yield return new WaitForSeconds (time);
            _isInvincible = false;
            gameObject.layer = normalLayer;
        }
        protected void KnockBackMove()
        {
            // xu ly viec bi day lui khi nhan sat thuong
        }
        protected void Flip(Diretion moveDir)
        {
            switch (moveDir) 
            {
                case Diretion.Left:
                    if(transform.localScale.x > 0)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        _isFaceLeft = true;
                    }
                    break;
                case Diretion.Right:
                    if (transform.localScale.x < 0)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        _isFaceLeft = false;
                    }
                    break;
                case Diretion.Up:
                    if(transform.localScale.y < 0)
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
                    }
                    break;
                case Diretion.Down:
                    if (transform.localScale.y > 0)
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
                    }
                    break;

            }
        }
        protected void DelayActionRate(ref bool isActed, ref float curTime, float startingTime)
        {// time delay
            if (isActed)
            {
                curTime -= Time.deltaTime;
                if (curTime <= 0) 
                {
                    isActed = false;
                    curTime = startingTime;
                }
            }
        }
        protected virtual void Dead()
        {
            gameObject.layer = deadLayer;
            if(_rb)
                _rb.velocity = Vector2.zero;
        }
    }

}