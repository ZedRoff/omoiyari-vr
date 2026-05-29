using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleUI : MonoBehaviour
{
    public GameObject uiCanvas;
    public Camera mainCamera;
    public InputActionReference displayAction;

    void Start()
    {
        // On cache l'UI au démarrage du jeu
        if (uiCanvas != null)
            uiCanvas.SetActive(false);
    }

    void Update()
    {
        mainCamera.gameObject.SetActive(false);
        uiCanvas.SetActive(displayAction.action.IsPressed());
    }
}