using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Asperio
{
    public class UISubTaskItem : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _textDescription;
        [SerializeField]
        private GameObject _checkMark;
        private TaskData.SubTask _subTaskData;
        private RectTransform _rect;
        private Vector2 _rectSize;

        private void Awake()
        {
            _rect = this.GetComponent<RectTransform>();
        }

        public void UpdateItem(TaskData.SubTask data)
        {
            _subTaskData = data;
            _textDescription.text = _subTaskData.description;

            if(_subTaskData.TargetCount > 0){
                _textDescription.text += $" ({_subTaskData.ListTargetProgress.Count}/{_subTaskData.TargetCount})";
            }

            _checkMark.SetActive(_subTaskData.IsCompleted);
            UpdateTextRectPreferredHeight(_textDescription, this.GetComponent<RectTransform>(), 0);
            _rectSize = _rect.sizeDelta;
        }

        private void UpdateTextRectPreferredHeight(TMP_Text tmpText, RectTransform container, float margin)
        {
            float width = tmpText.GetComponent<RectTransform>().sizeDelta.x;
            float baseHeight = tmpText.GetPreferredValues(tmpText.text, width, 0).y;
            container.sizeDelta = new Vector2(container.sizeDelta.x, baseHeight + margin);
        }


    }
}
