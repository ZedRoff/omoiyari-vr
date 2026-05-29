using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    public int delayBeforeLoad = 3;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void NewGame()
    {
        audioSource.PlayOneShot(clickSound);
        StartCoroutine(LoadSceneAfterDelay("LoadingScene"));
    }

    public void Options()
    {
        //audioSource.PlayOneShot(clickSound);
        StartCoroutine(LoadSceneAfterDelay("Menu options"));
    }

    public void QuitGame()
    {
        //audioSource.PlayOneShot(clickSound);
        StartCoroutine(QuitAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        Application.Quit();
    }
}
