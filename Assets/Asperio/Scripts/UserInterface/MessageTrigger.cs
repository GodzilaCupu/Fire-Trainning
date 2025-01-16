using UnityEngine;
using UnityEngine.Events;

namespace Asperio
{
    public class MessageTrigger : MonoBehaviour
    {
        [SerializeField]
        private string _message;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private UnityEvent _inputNextMessage;

        public void OpenMessage()
        {
            Message.Instance.OpenMessage(_message, _icon, _inputNextMessage);
        }

        public void Next()
        {
            Message.Instance.NextMessage();
        }
    }
}
