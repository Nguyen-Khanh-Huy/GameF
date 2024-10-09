using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PIS.PlatformGame
{
    public class FlashVfx : MonoBehaviour
    {
        public SpriteRenderer[] sp;
        [Range(0f, 0.15f)]
        public float flashRate;
        public Color normalColor;
        public Color flashColor;
        public UnityEvent OnCompleted;

        bool _isFlashing;
        float _flashingTime;
        Coroutine _flash;

        private void OnDisable()
        {
            SetSpritesAlpha(normalColor);
            StopFlash();
        }
        public void Flash(float time)
        {
            if (gameObject.activeInHierarchy)
                _flash = StartCoroutine(FlashCo(time));
        }
        public void StopFlash()
        {
            _isFlashing = false;

            if (_flash != null)
                StopCoroutine(_flash);
        }
        IEnumerator FlashCo(float time)
        {
            if (!_isFlashing)
            {
                _flashingTime = time;

                _isFlashing = true;

                while (_flashingTime > 0)
                {
                    SetSpritesAlpha(flashColor);
                    yield return new WaitForSeconds(flashRate);
                    SetSpritesAlpha(normalColor);
                    yield return new WaitForSeconds(flashRate);
                    SetSpritesAlpha(flashColor);
                    yield return new WaitForSeconds(flashRate);
                    SetSpritesAlpha(normalColor);
                }

                _isFlashing = false;
            }
            yield return null;
        }
        public void SetSpritesAlpha(Color color)
        {
            if (sp != null && sp.Length > 0)
            {
                for (int i = 0; i < sp.Length; i++)
                {
                    if (sp[i] != null)
                        sp[i].color = color;
                }
            }
        }
        private void Update()
        {
            if (_flashingTime > 0 && _isFlashing)
            {
                _flashingTime -= Time.deltaTime;

                if (_flashingTime <= 0)
                {
                    if (OnCompleted != null)
                        OnCompleted.Invoke();

                    _isFlashing = false;
                }
            }
        }
    }
}
