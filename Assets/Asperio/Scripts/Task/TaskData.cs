using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asperio
{
    [Serializable]
    [CreateAssetMenu]
    public class TaskData : ScriptableObject
    {
        public string Id;
        public string title;
        public string description;
        public List<string> ListProgress = new List<string>();
        public int TargetCount;
        public bool IsCompleted;
        public bool IsHideInUserTask;
        public List<SubTask> ListSubTask = new List<SubTask>();

        [Serializable]
        public class SubTask
        {
            public string Id;
            public string description;
            public List<string> ListTargetProgress = new List<string>();
            public int TargetCount;
            public bool IsInProgress;
            public bool IsCompleted;
            public bool IsHideInUserTask;
        }

        public class ProgressData
        {
            public string Id;
        }

        public void ClearProgress()
        {
            ListProgress = new List<string>();
            IsCompleted = false;
            for (int i = 0; i < ListSubTask.Count; i++)
            {
                ListSubTask[i].ListTargetProgress = new List<string>();
                ListSubTask[i].IsInProgress = false;
                ListSubTask[i].IsCompleted = false;
            }
        }

        public void SetSubTaskInProgressById(string id)
        {
            SubTask subTask = ListSubTask.Find(result => result.Id == id);
            subTask.IsInProgress = true;
        }

        public bool IsTaskProgressReachTarget()
        {
            return ListProgress.Count >= TargetCount;
        }

        public bool IsSubTaskProgressReachTarget(string id)
        {
            SubTask subTask = ListSubTask.Find(result => result.Id == id);
            return subTask.ListTargetProgress.Count >= subTask.TargetCount;
        }

        public void SetSubTaskCompleted(string id)
        {
            SubTask subTask = ListSubTask.Find(result => result.Id == id);
            subTask.IsCompleted = true;
        }

        public bool AddSubTaskListProgress(string subTaskId, string objectId)
        {
            SubTask subTask = ListSubTask.Find(result => result.Id == subTaskId);
            subTask.ListTargetProgress.Add(objectId);
            return subTask.ListTargetProgress.Count >= subTask.TargetCount;
        }

        public bool IsAllSubTaskCompleted()
        {
            bool isCompleted = true;
            for (int i = 0; i < ListSubTask.Count; i++)
            {
                if (!ListSubTask[i].IsCompleted)
                {
                    isCompleted = false;
                }
            }
            return isCompleted;
        }

        public SubTask GetSubTaskById(string id)
        {
            SubTask subTask = ListSubTask.Find(result => result.Id == id);
            return subTask;
        }


        public bool IsSubTaskInProgressExist()
        {
            return ListSubTask.Exists(result => result.IsInProgress == true);
        }
    }
}

