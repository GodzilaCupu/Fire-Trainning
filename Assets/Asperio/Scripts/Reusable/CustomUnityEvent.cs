using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Asperio
{
    public class CustomUnityEvent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _eventOnEnable;
        [SerializeField]
        private UnityEvent _eventOnLateEnable;
        [SerializeField]
        private UnityEvent _eventOnDisable;

        private void OnEnable()
        {
            _eventOnEnable?.Invoke();
            StartCoroutine(OnLateEnableCoroutine());
        }

        private IEnumerator OnLateEnableCoroutine()
        {
            yield return new WaitForEndOfFrame();
            _eventOnLateEnable?.Invoke();
        }

        private void OnDisable()
        {
            _eventOnDisable?.Invoke();
        }
    }
}

