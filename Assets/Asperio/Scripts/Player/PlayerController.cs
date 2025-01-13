using System;
using UnityEngine;

namespace Asperio
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField]
        private FadeEffect _fadeEffect;

        public void FadeIn(Action eventFadeDone = null)
        {
            _fadeEffect.Fade(false, eventFadeDone);
        }

        public void FadeOut(Action eventFadeDone = null)
        {
            _fadeEffect.Fade(true, eventFadeDone);
        }

        public void FadeInOut(Action eventFadeDone = null)
        {
            _fadeEffect.FadeInOut(eventFadeDone);
        }

        public void OnCompletedSubTask()
        {

        }
        public void OnAllTaskCompleted()
        {

        }
    }
}

