using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Asperio
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        [SerializeField]
        private bool _isLateResponse;
        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            if (_isLateResponse)
            {
                StartCoroutine(LateResponseCoroutine());
                return;
            }
            Response.Invoke();
        }

        private IEnumerator LateResponseCoroutine()
        {
            yield return new WaitForEndOfFrame();
            Response.Invoke();
        }
    }
}
