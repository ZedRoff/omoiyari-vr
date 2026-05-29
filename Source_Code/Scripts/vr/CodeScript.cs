using UnityEngine;
using TMPro; // REQUIS : Pour pouvoir manipuler le composant TextMeshPro

public class CodeScript : MonoBehaviour
{
    // Cette fonction sera appelée à chaque clic sur la RawImage (Flèche Haut)
    public void Up(TextMeshProUGUI t)
    {
        if (t == null) return;

        // 1. On lit le texte actuel et on le convertit en entier (si vide ou invalide, on commence à 0)
        int currentDigit = 0;
        int.TryParse(t.text, out currentDigit);

        // 2. On incrémente
        currentDigit++;

        // 3. Sécurité digicode : si on dépasse 9, on retourne à 0
        if (currentDigit > 9)
        {
            currentDigit = 0;
        }

        // 4. On réécrit la nouvelle valeur dans le TextMeshPro reçu
        t.text = currentDigit.ToString();

        Debug.Log($"[UP] Bloc cliqué : {gameObject.name} | Nouvelle valeur du texte : {t.text}");
    }

    // Cette fonction sera appelée à chaque clic sur la RawImage (Flèche Bas)
    public void Down(TextMeshProUGUI t)
    {
        if (t == null) return;

        // 1. On lit le texte actuel et on le convertit en entier
        int currentDigit = 0;
        int.TryParse(t.text, out currentDigit);

        // 2. On décrémente
        currentDigit--;

        // 3. Sécurité digicode : si on descend sous 0, on passe à 9
        if (currentDigit < 0)
        {
            currentDigit = 9;
        }

        // 4. On réécrit la nouvelle valeur dans le TextMeshPro reçu
        t.text = currentDigit.ToString();

        Debug.Log($"[DOWN] Bloc cliqué : {gameObject.name} | Nouvelle valeur du texte : {t.text}");
    }
}