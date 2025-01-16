using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Asperio
{
    public class UiTaskPreview : MonoBehaviour
    {
        [SerializeField]
        private Transform _uiSubTaskContainer;
        [SerializeField]
        private ObjectPool _uiSubTaskPool;

        [Header("UI Component")]
        [SerializeField]
        private TMP_Text _textTitle;
        [SerializeField]
        private TMP_Text _textDescription;
        [SerializeField]
        private RectTransform _containerRect;
        [SerializeField]
        private GameObject _buttonConfirm;
        [SerializeField]
        private GameObject _labelInProgress;
        [SerializeField]
        private GameObject _labelCompleted;
        [SerializeField]
        private GameEvent _eventOnPreviewUpdate;

        [Header("Events")]
        [SerializeField]
        private GameEvent _eventOnUserTaskUpdate;

        private TaskData _taskData;
        private RectTransform _rect;
        private RectTransform _rectTitle;
        private List<TaskData.SubTask> _listSubTaskData = new List<TaskData.SubTask>();

        private void Awake()
        {
            _rect = this.GetComponent<RectTransform>();
            _rectTitle = _textTitle.GetComponent<RectTransform>();
        }

        public void UpdateCurrentData()
        {
            if (StaticData.CurrentTaskData == null)
            {
                return;
            }
            UpdateItemPreview(StaticData.CurrentTaskData);
        }

        public void UpdateItemPreview(TaskData data)
        {
            StaticData.CurrentTaskData = data;
            _taskData = StaticData.CurrentTaskData;
            _listSubTaskData = _taskData.ListSubTask;
            _textTitle.text = _taskData.title;
            //_textDescription.text = _taskData.description;
            StartCoroutine(UpdateItemPreviewCoroutine());
            if (_taskData.IsCompleted)
            {
                _buttonConfirm.SetActive(false);
                _labelCompleted.SetActive(true);
                _labelInProgress.SetActive(false);
                return;
            }
            bool isSubTaskInProgressExist = _taskData.IsSubTaskInProgressExist();
            _labelCompleted.SetActive(false);
            if (isSubTaskInProgressExist)
            {
                _buttonConfirm.SetActive(false);
                _labelInProgress.SetActive(true);
            } else
            {
                _buttonConfirm.SetActive(true);
                _labelInProgress.SetActive(false);
            }
        }

        private IEnumerator UpdateItemPreviewCoroutine()
        {
            yield return new WaitForEndOfFrame();
            //UpdateTextRectPreferredHeight(_textTitle, _rectTitle, 0, 40);
            //float reduceHeight = _rectTitle.sizeDelta.y - 40;
            //_containerRect.sizeDelta = new Vector2(_containerRect.sizeDelta.x, 335 - reduceHeight);
            UpdateAllUISubTaskItem();
            _eventOnPreviewUpdate?.Raise();
        }

        private void UpdateTextRectPreferredHeight(TMP_Text tmpText, RectTransform container, float margin, float minHeight = 0)
        {
            float width = tmpText.GetComponent<RectTransform>().sizeDelta.x;
            float baseHeight = tmpText.GetPreferredValues(tmpText.text, width, 0).y;
            float finalHeight = baseHeight + margin;
            finalHeight = finalHeight < minHeight ? minHeight : finalHeight;
            container.sizeDelta = new Vector2(container.sizeDelta.x, finalHeight);
        }

        public void SetStartTaskInProgress()
        {
            TaskManager.Instance.OnStartTaskInProgress(_taskData);
            _eventOnUserTaskUpdate.Raise();
        }

        private void UpdateAllUISubTaskItem()
        {
            RefreshAllUISubTaskItem();
            for (int i = 0; i < _listSubTaskData.Count; i++)
            {
                if (_listSubTaskData[i].IsHideInUserTask)
                {
                    continue;
                }
                GameObject newObj = _uiSubTaskPool.GetObjectFromPool();
                newObj.transform.SetParent(_uiSubTaskContainer);
                newObj.transform.localPosition = Vector3.zero;
                newObj.transform.localRotation = Quaternion.identity;
                newObj.transform.localScale = Vector3.one;
                newObj.transform.SetAsLastSibling();
                UISubTaskItem item = newObj.GetComponent<UISubTaskItem>();
                item.UpdateItem(_listSubTaskData[i]);
            }
        }

        private void RefreshAllUISubTaskItem()
        {
            UISubTaskItem[] items = _uiSubTaskContainer.GetComponentsInChildren<UISubTaskItem>();
            for (int i = 0; i < items.Length; i++)
            {
                _uiSubTaskPool.AddObjectToPool(items[i].gameObject);
            }
        }
    }
}
