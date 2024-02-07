using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Core.SceneManagement
{
    public class GameSceneManager: MonoSingleton<GameSceneManager>
    {
        public void TriggerSceneChange(Scene scene, Action onSceneLoaded = null)
        {
            int sceneIndex = (int) scene;
            SceneManager.LoadScene(sceneIndex);

            StartCoroutine(WaitUntilSceneLoaded());
            
            IEnumerator WaitUntilSceneLoaded()
            {
                while (SceneManager.GetActiveScene().buildIndex != sceneIndex)
                {
                    yield return null;
                }

                onSceneLoaded?.Invoke();
            }
        }
    }
}