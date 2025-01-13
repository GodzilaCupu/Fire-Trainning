using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asperio
{
    public class SceneHandler : Singleton<SceneHandler>
    {
        public void OpenScene(string sceneName)
        {
            PlayerController.Instance.FadeIn(delegate
            {
                StaticData.PreviousSceneName = SceneManager.GetActiveScene().name;
                StaticData.SceneToLoad = sceneName;
                SceneManager.LoadScene(StaticData.LOADING_SCENE);
            });
        }

        public void OpenCurrentScene()
        {
            PlayerController.Instance.FadeIn(delegate
            {
                StaticData.SceneToLoad = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(StaticData.LOADING_SCENE);
            });
        }
    }
}
