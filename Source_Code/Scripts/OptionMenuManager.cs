using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;



public class OptionMenuManager : MonoBehaviour
{
    [Header("Tabs")]
    public GameObject audioTab;
    public GameObject controlsTab;
    public GameObject firstSelectedButton;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null); // reset d'abord
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        //ShowAudioTab();
    }

    public void ShowAudioTab()
    {
        audioTab.SetActive(true);
        controlsTab.SetActive(false);
    }

    public void ShowControlsTab()
    {
        audioTab.SetActive(false);
        controlsTab.SetActive(true);
    }
    public void BackToMenu()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");
    }
}
