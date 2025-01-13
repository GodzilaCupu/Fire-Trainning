using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Asperio
{
    public class LoadingManager : MonoBehaviour
    {
        //public Image loadingBar;
        private AsyncOperation asyncLoad;

        private void Start()
        {
            Time.timeScale = 1;
            StartCoroutine(LoadYourAsyncScene());
        }

        IEnumerator LoadYourAsyncScene()
        {
            //loadingBar.fillAmount = 0;
            yield return new WaitForSeconds(1f);
            asyncLoad = SceneManager.LoadSceneAsync(StaticData.SceneToLoad);
            //asyncLoad.allowSceneActivation = false;
            while (!asyncLoad.isDone)
            {
                //loadingBar.fillAmount = asyncLoad.progress;
                yield return null;
            }
        }

        public void AllowLoadScene()
        {
            asyncLoad.allowSceneActivation = true;
        }
    }
}
