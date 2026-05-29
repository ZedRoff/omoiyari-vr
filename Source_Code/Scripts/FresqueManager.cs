using UnityEngine;

public class FresqueManager : MonoBehaviour
{
    public GameObject tangramFresque;
    public TangramPiece[] allPieces; // Assigné dans l'inspecteur

    void Start()
    {
        tangramFresque.SetActive(true); // Caché au départ
    }

    void Update()
    {
        if (AllPiecesCollected())
        {
            tangramFresque.SetActive(true);
        }
    }

    bool AllPiecesCollected()
    {
        foreach (TangramPiece piece in allPieces)
        {
            if (!piece.isCollected)
                return false;
        }
        return true;
    }
}
