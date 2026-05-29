using UnityEngine;

public class DaltonismFilter : MonoBehaviour
{
    public GameObject[] profiles; 
    private int current = -1;

    public bool playGroundStarted;
    public bool postProActivated => current != -1;

    public void SwitchMode()
    {
         if (current != -1)
            profiles[current].SetActive(false);

        current = (current + 1) % profiles.Length;
        profiles[current].SetActive(true);
    }
    public void Activate(int index)
    {
       
        profiles[index].SetActive(true);
        current = index;
    }

    public void DeActivateAll()
    {
        foreach (GameObject obj in profiles)
            obj.SetActive(false);
        current = -1;
    }
}
