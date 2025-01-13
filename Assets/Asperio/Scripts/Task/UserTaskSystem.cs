using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Asperio
{
    public class UserTaskSystem : Singleton<UserTaskSystem>
    {
        [SerializeField]
        private UserTaskData _userTaskData;
        [SerializeField]
        private ObjectPool _uiTaskPool;
        [SerializeField]
        private Transform _uiTaskContainer;

        private void Start()
        {
            if (!StaticData.IsInitializeUserTaskDone)
            {
                ClearUserTaskData();
                StaticData.IsInitializeUserTaskDone = true;
            }
            UpdateAllUITaskItem();
        }

        public void ClearUserTaskData()
        {
            _userTaskData.ClearUserTaskData();
        }

        public void OnUserTaskUpdate()
        {
            UpdateAllUITaskItem();
        }

        public void RefreshContainer()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_uiTaskContainer as RectTransform);
        }

        private void UpdateAllUITaskItem()
        {
            RefreshAllUITaskItem();
            List<TaskData> _listTaskData = _userTaskData.GetListUserTask();
            for (int i = 0; i < _listTaskData.Count; i++)
            {
                if (_listTaskData[i].IsHideInUserTask)
                {
                    continue;
                }
                GameObject newObj = _uiTaskPool.GetObjectFromPool();
                newObj.transform.SetParent(_uiTaskContainer);
                newObj.transform.localPosition = Vector3.zero;
                newObj.transform.localRotation = Quaternion.identity;
                newObj.transform.localScale = Vector3.one;
                newObj.transform.SetAsLastSibling();
                UITaskItem item = newObj.GetComponent<UITaskItem>();
                item.UpdateItem(_listTaskData[i]);
            }
        }

        private void RefreshAllUITaskItem()
        {
            UITaskItem[] items = _uiTaskContainer.GetComponentsInChildren<UITaskItem>();
            for (int i = 0; i < items.Length; i++)
            {
                _uiTaskPool.AddObjectToPool(items[i].gameObject);
            }
        }

        public UITaskItem GetUITaskItemByTaskData(TaskData data)
        {
            List<UITaskItem> items = _uiTaskContainer.GetComponentsInChildren<UITaskItem>().ToList();
            UITaskItem item = items.Find(result => result.GetTaskData() == data);
            return item;
        }

        public bool IsUserTaskInProgressExist()
        {
            return _userTaskData.IsUserTaskInProgressExist();
        }

        public TaskData GetUserTaskInProgress()
        {
            return _userTaskData.GetUserTaskInProgress();
        }
    }
}
