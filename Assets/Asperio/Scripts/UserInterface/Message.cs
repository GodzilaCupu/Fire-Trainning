using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Asperio
{
    public class Message : Singleton<Message>
    {
        public enum MessageType
        {
            guide,
            notification
        }

        [SerializeField]
        private InputAction _inputNextMessage;

        [SerializeField]
        private TMP_Text _textMessage;
        [SerializeField]
        private Image _imageIcon;
        [SerializeField]
        private Sprite _defaultIcon;
        [SerializeField]
        private AspectRatioFitter _aspectRatioIcon;

        [SerializeField]
        private RectTransform _rectCanvas;
        [SerializeField]
        private RectTransform _rectIcon;
        [SerializeField]
        private RectTransform _rectTextMesage;
        private UnityEvent _eventNext;
        private MessageType _messageType;

        private void OnEnable()
        {
            SetInputAction();
            CloseMessage();
        }

        public void OpenMessage(string message, Sprite icon, UnityEvent eventNext, MessageType messageType = MessageType.guide)
        {
            _eventNext = eventNext;
            _messageType = messageType;
            _rectCanvas.gameObject.SetActive(true);
            _textMessage.text = message;
            if(icon == null)
            {
                _imageIcon.sprite = _defaultIcon;
            } else
            {
                _imageIcon.sprite = icon;
            }
            _aspectRatioIcon.aspectRatio = (float)_imageIcon.sprite.rect.width / (float)_imageIcon.sprite.rect.height;
            UpdateTextRectPreferredHeight(_textMessage, _rectCanvas, 40, 80);
        }

        private void UpdateTextRectPreferredHeight(TMP_Text tmpText, RectTransform container, float margin, float minHeight = 0)
        {
            float width = tmpText.GetComponent<RectTransform>().sizeDelta.x;
            float baseHeight = tmpText.GetPreferredValues(tmpText.text, width, 0).y;
            float finalHeight = baseHeight + margin;
            finalHeight = finalHeight < minHeight ? minHeight : finalHeight;
            container.sizeDelta = new Vector2(container.sizeDelta.x, finalHeight);
        }

        private void SetInputAction()
        {
            _inputNextMessage.performed += NextMessageAction;
            _inputNextMessage.Enable();
        }

        public void NextMessageAction(InputAction.CallbackContext obj)
        {
            NextMessage();
        }

        public void CloseMessage()
        {
            _rectCanvas.gameObject.SetActive(false);
        }

        public void NextMessage()
        {
            CloseMessage();
            _eventNext?.Invoke();
        }
    }
}
