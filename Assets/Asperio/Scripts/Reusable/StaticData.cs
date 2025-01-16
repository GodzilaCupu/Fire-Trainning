using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asperio
{
    public static class StaticData
    {
        public static string PreviousSceneName;
        public static string SceneToLoad;
        public static bool IsInitializeUserTaskDone;
        public static TaskData CurrentTaskData;
        public static ModuleData CurrentModuleData;
        public const string WELCOME_SCENE = "Welcome";
        public const string LOADING_SCENE = "Loading";
    }
}
