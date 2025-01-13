using UnityEngine;

namespace Asperio
{
    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private float _yOffset;

        public void UpdateYRotate()
        {
            this.transform.localRotation = Quaternion.Euler(new Vector3(0, _target.localEulerAngles.y + _yOffset, 0));
        }
    }
}

