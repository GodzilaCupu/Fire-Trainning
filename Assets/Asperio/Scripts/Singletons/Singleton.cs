using UnityEngine;

namespace Asperio
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected bool _isPersistant;
        public static T Instance { get; private set; }
        public virtual void Awake()
        {
            if (Instance == null) Instance = this as T;
            else Destroy(gameObject);

            if (_isPersistant) DontDestroyOnLoad(this);
        }
    }
}
