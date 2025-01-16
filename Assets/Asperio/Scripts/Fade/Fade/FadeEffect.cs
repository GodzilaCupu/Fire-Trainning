using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asperio
{
    public class FadeEffect : MonoBehaviour
    {
        [SerializeField]
        private float _fadeDelay = 0.07f;
        private Material _material;
        private bool _isFading;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            Fade(true);
        }

        public void Fade(bool fadeOut, Action eventFadeDone = null)
        {
            StopAllCoroutines();
            StartCoroutine(PlayEffect(fadeOut, eventFadeDone));
        }

        private IEnumerator PlayEffect(bool fadeOut, Action eventFadeDone)
        {
            _isFading = true;
            float startAlpha = fadeOut ? 1.0f : 0.0f;
            float endAlpha = fadeOut ? 0f : 1.0f;

            float elapsedTime = 0;
            while (elapsedTime < _fadeDelay)
            {
                elapsedTime += Time.deltaTime;
                float tempVal = Mathf.Lerp(startAlpha, endAlpha,
                    elapsedTime / _fadeDelay);
                _material.SetFloat("_Alpha", tempVal);
                yield return null;
            }
            _material.SetFloat("_Alpha", endAlpha);
            eventFadeDone?.Invoke();
            _isFading = false;
        }

        public void FadeInOut(Action eventFadeDone = null)
        {
            StopAllCoroutines();
            StartCoroutine(PlayEffectInOut(eventFadeDone));
        }

        private IEnumerator PlayEffectInOut(Action eventFadeDone)
        {
            _isFading = true;
            float startAlpha = 0f;
            float endAlpha = 1.0f;

            float elapsedTime = 0;
            while (elapsedTime < _fadeDelay)
            {
                elapsedTime += Time.deltaTime;
                float tempVal = Mathf.Lerp(startAlpha, endAlpha,
                    elapsedTime / _fadeDelay);

                _material.SetFloat("_Alpha", tempVal);
                yield return null;
            }
            _material.SetFloat("_Alpha", endAlpha);

            eventFadeDone?.Invoke();

            startAlpha = 1.0f;
            endAlpha = 0f;

            elapsedTime = 0;
            while (elapsedTime < _fadeDelay)
            {
                elapsedTime += Time.deltaTime;
                float tempVal = Mathf.Lerp(startAlpha, endAlpha,
                    elapsedTime / _fadeDelay);

                _material.SetFloat("_Alpha", tempVal);
                yield return null;
            }
            _material.SetFloat("_Alpha", endAlpha);
            _isFading = false;
        }
    }
}
