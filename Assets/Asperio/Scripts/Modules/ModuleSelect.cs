using UnityEngine;
using UnityEngine.Events;

namespace Asperio
{
    public class ModuleSelect : MonoBehaviour
    {
        [SerializeField]
        private ModuleData _moduleData;
        [SerializeField]
        private UnityEvent _OnModuleSelected;

        public void OnModuleSelected()
        {
            StaticData.CurrentModuleData = _moduleData;
            SceneHandler.Instance.OpenScene(StaticData.CurrentModuleData.SceneName);
            _OnModuleSelected?.Invoke();
        }
    }
}

