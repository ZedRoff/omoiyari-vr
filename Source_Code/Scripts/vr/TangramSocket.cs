using System.Collections.Generic;
using UnityEngine;

public class TangramSocket : MonoBehaviour
{
    // Garde en mémoire le bon objet s'il est présent
    public GameObject currentPieceInSocket = null;

    // Liste interne pour suivre TOUS les objets actuellement à l'intérieur du socle
    private List<GameObject> m_ObjectsInSocket = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        // 1. Dès qu'un objet "draggable" entre, on l'ajoute à la liste des présents
        if (other.CompareTag("draggable") && !m_ObjectsInSocket.Contains(other.gameObject))
        {
            m_ObjectsInSocket.Add(other.gameObject);
            UpdateSocketStatus();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 2. Dès qu'un objet sort, on l'enlève de la liste
        if (other.CompareTag("draggable") && m_ObjectsInSocket.Contains(other.gameObject))
        {
            m_ObjectsInSocket.Remove(other.gameObject);
            UpdateSocketStatus();
        }
    }

    // 3. On fait le tri pour voir si le BON élément fait partie de la liste
    private void UpdateSocketStatus()
    {
        bool bonItemTrouve = false;

        foreach (GameObject obj in m_ObjectsInSocket)
        {
            // Si l'un des objets dans la liste a le même nom que le socle, c'est gagné
            if (obj.name == gameObject.name)
            {
                currentPieceInSocket = obj;
                bonItemTrouve = true;
                break; // On a trouvé le bon, pas besoin de regarder les autres
            }
        }

        // Si le bon item n'est plus du tout dans la liste, le socle redevient vide
        if (!bonItemTrouve)
        {
            currentPieceInSocket = null;
        }
    }
}