using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIS.PlatformGame
{
    public class CamShake : MonoBehaviour
    {
        public static CamShake ins;
        public Transform camTransform;

        public float shakeTime; // time shake
        public float shakeRadius; // ban kinh shake
        public float shakeForce; // luc shake

        private bool _canShake;
        private Vector3 _originalPos;

        public void Awake()
        {
            ins = this;
        }

        private void OnEnable()
        {
            _originalPos = camTransform.localPosition;
        }

        private void Update()
        {
            if (!_canShake) return;
            if (shakeTime > 0)
            {
                camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeRadius;

                shakeTime -= Time.deltaTime * shakeForce;
            }
            else
            {
                shakeTime = 0f;
                camTransform.localPosition = _originalPos;
                _canShake = false;
            }
        }

        public void ShakeTrigger(float _shakeTime, float _shakeRadius, float _shakeForce = 1.0f)
        {
            _canShake = true;
            shakeTime = _shakeTime;
            shakeRadius = _shakeRadius;
            shakeForce = _shakeForce;
        }

        public void ShakeTrigger(bool isTrigger = true)
        {
            _canShake = isTrigger;
            camTransform.localPosition = _originalPos;
        }
    }

}