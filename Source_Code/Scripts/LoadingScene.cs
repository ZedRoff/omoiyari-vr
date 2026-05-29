using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScene : MonoBehaviour
{
    public Slider loadingBar;
    private string sceneToLoad = "Main_Scene"; // Remplace par ta scène cible
    public float minLoadTime = 5f; // Durée minimum d'affichage de la barre de chargement

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float elapsedTime = 0f;
        float fakeProgress = 0f;

        while (!operation.isDone)
        {
            // Progression réelle entre 0 et 0.9
            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // On augmente progressivement la "fausse" progression
            elapsedTime += Time.deltaTime;
            float timeProgress = Mathf.Clamp01(elapsedTime / minLoadTime);

            // On prend le minimum entre la progression simulée et la réelle pour rester cohérent
            fakeProgress = Mathf.Min(timeProgress, realProgress);

            loadingBar.value = fakeProgress;

            // Quand les deux sont à 1.0, on peut activer la scène
            if (fakeProgress >= 1f && operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f); // petite pause finale
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
