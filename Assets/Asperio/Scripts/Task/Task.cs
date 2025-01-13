using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Asperio
{
    public abstract class Task : MonoBehaviour
    {
        [Header ("Task")]
        public TaskData _taskData;
        [Tooltip("Called when task add to User Task List")]
        public UnityEvent _eventOnStartTask;
        [Tooltip ("Called once on start scene when task has completed")]
        public UnityEvent _eventOnStartTaskIsCompleted;
        [Tooltip("Called when button in progress task list pressed")]
        public UnityEvent _eventOnStartTaskInProgress;
        [Tooltip("Called when task is completed")]
        public UnityEvent _eventOnCompletedTask;
        [Tooltip("Called On Awake")]
        public UnityEvent _eventOnAwake;

        [Header("SubTask")]
        public List<SubTask> _listEventSubTask;

        [Serializable]
        public class SubTask
        {
            public string Id;
            public UnityEvent EventOnStartTask;
            public UnityEvent EventOnStartTaskIsCompleted;
            public UnityEvent EventOnCompletedTask;
        }

        private void Awake()
        {
            _eventOnAwake?.Invoke();
            
        }

        private void OnDestroy()
        {
            TaskManager.Instance.EventOnStartTaskInProgress -= OnStartTaskInProgress;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            TaskManager.Instance.EventOnStartTaskInProgress += OnStartTaskInProgress;
            InitTaskByUserTaskData();
        }

        private void OnStartTaskInProgress(TaskData taskData)
        {
            if(_taskData != taskData)
            {
                return;
            }
            _eventOnStartTaskInProgress?.Invoke();
        }

        public void InitTaskByUserTaskData()
        {
            if (!TaskManager.Instance.IsTaskExistInUserTaskData(_taskData.Id))
                return;
            if (_taskData.IsCompleted)
            {
                OnStartTaskIsCompleted();
                List<TaskData.SubTask> listSubTask = _taskData.ListSubTask;
                if (listSubTask.Count == 0)
                    return;
                for (int i = 0; i < listSubTask.Count; i++)
                {
                    if (listSubTask[i].IsInProgress && listSubTask[i].IsCompleted)
                    {
                        OnStartSubTaskIsCompletedById(listSubTask[i].Id);
                    }
                }
            } else
            {
                OnStartTask();
                List<TaskData.SubTask> listSubTask = _taskData.ListSubTask;
                if (listSubTask.Count == 0)
                    return;
                for (int i = 0; i < listSubTask.Count; i++)
                {
                    if (listSubTask[i].IsInProgress && !listSubTask[i].IsCompleted)
                    {
                        OnStartSubTaskById(listSubTask[i].Id);
                    }
                }
            }
        }

        public void OnStartTask()
        {
            _eventOnStartTask?.Invoke();
        }

        public void OnStartTaskIsCompleted()
        {
            _eventOnStartTaskIsCompleted?.Invoke();
        }

        public void OnCompletedTask()
        {
            _taskData.IsCompleted = true;
            _eventOnCompletedTask?.Invoke();
            TaskManager.Instance.OnUserTaskUpdate();
            TaskManager.Instance.OnCompletedTask();
        }

        public void OnStartSubTaskById(string id)
        {
            TaskData.SubTask subTaskData = _taskData.GetSubTaskById(id);
            subTaskData.IsInProgress = true;
            SubTask subTask = _listEventSubTask.Find(result => result.Id == id);
            if (subTask == null)
            {
                Debug.Log($"Subtask {id} not found in {_taskData.Id}");
                return;
            }
            subTask.EventOnStartTask?.Invoke();
        }

        public void OnStartSubTaskIsCompletedById(string id)
        {
            TaskData.SubTask subTaskData = _taskData.GetSubTaskById(id);
            SubTask subTask = _listEventSubTask.Find(result => result.Id == id);
            if (subTask == null)
            {
                Debug.Log($"Subtask {id} not found in {_taskData.Id}");
                return;
            }
            subTask.EventOnStartTaskIsCompleted?.Invoke();
        }

        public void OnCompletedSubTaskById(string id)
        {
            TaskData.SubTask subTaskData = _taskData.GetSubTaskById(id);
            subTaskData.IsCompleted = true;
            SubTask subTask = _listEventSubTask.Find(result => result.Id == id);
            if (subTask == null)
            {
                Debug.Log($"Subtask {id} not found in {_taskData.Id}");
                return;
            }
            subTask.EventOnCompletedTask?.Invoke();
            if (_taskData.IsAllSubTaskCompleted())
            {
                OnCompletedTask();
            } else
            {
                if (!subTaskData.IsHideInUserTask && !_taskData.IsHideInUserTask)
                {
                    PlayerController.Instance.OnCompletedSubTask();
                }
                TaskManager.Instance.OnUserTaskUpdate();
            }
        }

        public void ClearProgress()
        {
            _taskData.ClearProgress();
        }
    }
}
