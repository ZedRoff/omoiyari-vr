using UnityEngine;

public class ShelterZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerState.Instance.EnterShelter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerState.Instance.ExitShelter();
    }
}
