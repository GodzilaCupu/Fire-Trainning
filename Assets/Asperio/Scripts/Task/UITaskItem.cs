using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asperio
{
    public class UITaskItem : MonoBehaviour
    {
        [Header ("UI Component")]
        [SerializeField]
        private TMP_Text _textTitle;
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Image _buttonIcon;
        [SerializeField]
        private Sprite _iconPlay;
        [SerializeField]
        private Sprite _iconInProgress;
        [SerializeField]
        private Sprite _iconCompleted;
        [SerializeField]
        private Image _bulletIcon;
        [SerializeField]
        private Color _bulletIconDefault;
        [SerializeField]
        private Color _bulletIconCompleted;

        private TaskData _taskData;
        private RectTransform _rect;
        private Vector2 _rectSize;
        private float _minHeight = 50;


        private void Awake()
        {
            _rect = this.GetComponent<RectTransform>();
        }

        public void UpdateItem(TaskData data)
        {
            _taskData = data;
            _textTitle.text = _taskData.title;
            if (_taskData.IsCompleted)
            {
                _bulletIcon.color = _bulletIconCompleted;
                UpdateTextRectPreferredHeight(_textTitle, _rect, 20, _minHeight);
                _button.interactable = false;
                _buttonIcon.sprite = _iconCompleted;
                _buttonIcon.color = Color.white;
                return;
            } else
            {
                _bulletIcon.color = _bulletIconDefault;
            }
            bool isUserTaskInProgressExist = UserTaskSystem.Instance.IsUserTaskInProgressExist();
            TaskData taskInProgress = UserTaskSystem.Instance.GetUserTaskInProgress();
            if (isUserTaskInProgressExist && taskInProgress != _taskData)
            {
                _button.interactable = false;
                _buttonIcon.color = Color.white;
            } else
            {
                _button.interactable = false;
                _buttonIcon.color = Color.white;
            }
            bool isSubTaskInProgressExist = _taskData.IsSubTaskInProgressExist();
            if (isSubTaskInProgressExist)
            {
                _buttonIcon.sprite = _iconInProgress;
            } else
            {
                _buttonIcon.sprite = _iconPlay;
            }
            UpdateTextRectPreferredHeight(_textTitle, _rect, 20, _minHeight);
            _rectSize = _rect.sizeDelta;
        }

        private void UpdateTextRectPreferredHeight(TMP_Text tmpText, RectTransform container, float margin, float minHeight = 0)
        {
            float width = tmpText.GetComponent<RectTransform>().sizeDelta.x;
            float baseHeight = tmpText.GetPreferredValues(tmpText.text, width, 0).y;
            float finalHeight = baseHeight + margin;
            finalHeight = finalHeight < minHeight ? minHeight : finalHeight;
            container.sizeDelta = new Vector2(container.sizeDelta.x, finalHeight);
        }

        public void SelectTask()
        {
            Menu.Instance.OpenConfirmStartTask(this);
        }

        public TaskData GetTaskData()
        {
            return _taskData;
        }
    }
}

