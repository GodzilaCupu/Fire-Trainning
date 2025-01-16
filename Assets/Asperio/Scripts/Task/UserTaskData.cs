using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asperio
{
    [Serializable]
    [CreateAssetMenu]
    public class UserTaskData : ScriptableObject
    {
        [SerializeField]
        private List<TaskData> _listUserTask = new List<TaskData>();

        public void AddTaskInProgress(TaskData data)
        {
            _listUserTask.Add(data);
        }

        public List<TaskData> GetListUserTask()
        {
            return _listUserTask;
        }

        public void ClearUserTaskData()
        {
            _listUserTask = new List<TaskData>();
        }

        public bool IsUserTaskInProgressExist()
        {
            List<TaskData> _listUserTaskInProgress = _listUserTask.FindAll(result => result.IsCompleted == false);
            return _listUserTaskInProgress.Exists(result => result.IsSubTaskInProgressExist());
        }

        public TaskData GetUserTaskInProgress()
        {
            List<TaskData> _listUserTaskInProgress = _listUserTask.FindAll(result => result.IsCompleted == false);
            return _listUserTaskInProgress.Find(result => result.IsSubTaskInProgressExist());
        }

        public bool IsAllTaskCompleted()
        {
            bool isCompletedFalseExist = _listUserTask.Exists(result => !result.IsCompleted);
            bool isAllCompleted = !isCompletedFalseExist;
            return isAllCompleted;
        }
    }
}
