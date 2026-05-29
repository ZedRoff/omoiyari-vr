using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonManager : MonoBehaviour
{
    public GameObject defaultSelected;
    private GameObject lastValidSelected;

    void Start()
    {
        lastValidSelected = defaultSelected;
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }

    void Update()
    {
        GameObject current = EventSystem.current.currentSelectedGameObject;

        if (current != null)
        {
            lastValidSelected = current;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(lastValidSelected);
        }
    }
}

