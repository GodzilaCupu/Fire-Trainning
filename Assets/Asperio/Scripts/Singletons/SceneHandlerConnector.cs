using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asperio
{
    public class SceneHandlerConnector : MonoBehaviour
    {
        public void OpenScene(string sceneName)
        {
            SceneHandler.Instance.OpenScene(sceneName);
        }

        public void RestartScene()
        {
            SceneHandler.Instance.OpenCurrentScene();
        }

        public void OpenWelcomeScene()
        {
            SceneHandler.Instance.OpenScene(StaticData.WELCOME_SCENE);
        }
    }
}

