using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Asperio
{
    public class TaskManager : Singleton<TaskManager>
    {
        [SerializeField]
        private UserTaskData _userTaskData;

        [Header("Events")]
        [SerializeField]
        private GameEvent _eventOnUserTaskUpdate;
        [SerializeField]
        private GameEvent _eventOnTaskManagerDoneInit;
        [SerializeField]
        private UnityEvent _eventIsAllTaskCompleted;
        public event Action<TaskData> EventOnStartTaskInProgress;

        [Header("For Testing Only")]
        [SerializeField]
        private Task _taskTesting;
        [SerializeField]
        private UnityEvent _eventAfterAddTask;

        [SerializeField]
        private bool _isInitTaskUser;
        [SerializeField]
        private InputAction _inputTesting;
        [SerializeField]
        private UnityEvent _eventTriggerTesting;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);
            if (!string.IsNullOrEmpty(StaticData.PreviousSceneName))
            {
                _eventOnTaskManagerDoneInit?.Raise();
                yield break;
            } 
            //Wait for UserTaskSystem reset done & GeneralTask start done
#if UNITY_EDITOR
            if (_taskTesting == null)
            {
                _eventOnTaskManagerDoneInit?.Raise();
                yield break;
            }
                
            AddTaskInProgress(_taskTesting._taskData);
            _eventAfterAddTask?.Invoke();
            if (_isInitTaskUser)
            {
                _taskTesting.InitTaskByUserTaskData();
            }
            SetInputAction();
#endif
            _eventOnTaskManagerDoneInit?.Raise();
        }

        private void SetInputAction()
        {
            _inputTesting.performed += OnToggleMenuAction;
            _inputTesting.Enable();
        }

        private void OnToggleMenuAction(InputAction.CallbackContext obj)
        {
            _eventTriggerTesting?.Invoke();
        }


        public void AddTaskInProgress(TaskData data)
        {
            if (_userTaskData.GetListUserTask().Contains(data))
            {
                Debug.Log("Task already exist");
                return;
            }
            data.ClearProgress();
            _userTaskData.AddTaskInProgress(data);
            OnUserTaskUpdate();
        }

        public void OnUserTaskUpdate()
        {
            _eventOnUserTaskUpdate?.Raise();
        }

        public void OnCompletedTask()
        {
            if (_userTaskData.IsAllTaskCompleted())
            {
                PlayerController.Instance.OnAllTaskCompleted();
                _eventIsAllTaskCompleted?.Invoke();
            }
        }

        public bool IsTaskExistInUserTaskData(string id)
        {
            return _userTaskData.GetListUserTask().Exists(result => result.Id == id);
        }

        public void OnStartTaskInProgress(TaskData taskData)
        {
            EventOnStartTaskInProgress.Invoke(taskData);
        }
    }
}

