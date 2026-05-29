using System.Collections.Generic;
using UnityEngine;

public class Becher : MonoBehaviour
{
    public List<string> contenus = new List<string>();
    public Renderer liquideRenderer;
    public Color bonneCouleur;
    public Color mauvaiseCouleur;

    public void VerserDansBecher(GameObject substanceObject)
    {
        if (substanceObject.CompareTag("Liquide"))
        {
            contenus.Add(substanceObject.name);
        }
    }

    public void VerifierReactions()
    {
        // Vérifie que les deux substances spécifiques sont présentes, peu importe l'ordre
        bool contientCuivre = true;
        bool contientSodium = true;

        if (contientCuivre && contientSodium)
        {
            Debug.Log("Bonne réaction : La solution devient bleue !");
            liquideRenderer.material.color = bonneCouleur;
        }
        else
        {
            Debug.Log("Mauvaise réaction ou mélange incorrect.");
            liquideRenderer.material.color = mauvaiseCouleur;
        }
    }

    public void ResetBecher()
    {
        contenus.Clear();
        liquideRenderer.material.color = Color.clear; 
    }
}
