using UnityEngine;

public class ChemistryManager : MonoBehaviour
{
    //public static GameManager Instance;

    private bool hasCu = false;
    private bool hasNaOH = false;

    public Renderer beakerLiquid; // Matériau du liquide
    public Color initialColor = Color.clear;
    public Color finalColor = Color.blue;

    private void Awake()
    {
        //if (Instance == null) Instance = this;
        //else Destroy(gameObject);
    }

    public void AddChemical(string chem)
    {
        if (chem == "sulfate_cuivre") hasCu = true;
        if (chem == "hydroxyde_sodium") hasNaOH = true;

        CheckReaction();
    }

    void CheckReaction()
    {
        if (hasCu && hasNaOH)
        {
            Debug.Log("Réaction réussie !");
            if (beakerLiquid != null)
                beakerLiquid.material.color = finalColor;
        }
    }
}


