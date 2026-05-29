using UnityEngine;

public class TangramManager : MonoBehaviour
{
    [Header("Configuration des Socles")]
    // Glisse tes socles ici dans l'inspecteur Unity
    public TangramSocket[] tangramSockets;

    [Header("Résultat du Checking")]
    // Ce tableau sera visible en temps réel dans l'inspecteur d'Unity !
    public bool[] placementResults;

    public GameObject gate;

    void Start()
    {
        // On initialise le tableau de booléens à la même taille que le nombre de socles
        if (tangramSockets != null)
        {
            placementResults = new bool[tangramSockets.Length];
        }
    }

    void Update()
    {
        if (tangramSockets == null || tangramSockets.Length == 0) return;

        // On lance la vérification
        CheckAllPlacements();
    }

    private void CheckAllPlacements()
    {
        // On prépare une variable pour savoir si TOUT le puzzle est validé
        bool allCorrect = true;

        for (int i = 0; i < tangramSockets.Length; i++)
        {
            if (tangramSockets[i] == null) continue;

            GameObject pieceActuelle = tangramSockets[i].currentPieceInSocket;

            // --- CONDITION DE VICTOIRE POUR CE SOCLE ---
            // 1. Il faut qu'il y ait une pièce dans le socle
            // 2. Le nom de la pièce doit être EXACTEMENT le même que le nom du socle
            if (pieceActuelle != null && pieceActuelle.name == tangramSockets[i].name)
            {
                placementResults[i] = true; // Bien placé !
            }
            else
            {
                placementResults[i] = false; // Vide ou mauvaise pièce !
                allCorrect = false; // Du coup, le puzzle global n'est pas totalement fini
            }
        }

        // OPTIONNEL : Un petit récapitulatif visuel dans la console pour débugger facilement
        // Tu pourras supprimer ce bloc de Debug une fois que tout fonctionnera !
        string debugMessage = "Statut des socles : ";
        for (int i = 0; i < placementResults.Length; i++)
        {
            debugMessage += $"[{tangramSockets[i].name}: {(placementResults[i] ? "✅" : "❌")}] ";
        }
        Debug.Log(debugMessage);

        // Si tout est à "true", tu déclenches ta logique de victoire
        if (allCorrect)
        {
            OnPuzzleComplete();
        }
    }

    private void OnPuzzleComplete()
    {
        Debug.Log("Félicitations ! Toutes les pièces sont sur le bon socle !");
        // Désactive ce script ou passe à l'étape suivante de ton jeu ici
        enabled = false;
        gate.transform.Translate(20.0f * Vector3.forward);
    }
}