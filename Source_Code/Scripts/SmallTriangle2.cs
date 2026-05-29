using UnityEngine;

public class SmallTriangle2 : TangramShape
{
    void OnValidate()
    {
        // On soustrait 1 en X et 1 en Y à tous les points pour ramener le coin sur le pivot (0,0,0)
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0), // Le coin inférieur gauche devient le pivot !
            new Vector3(1, 0), // Le coin inférieur droit
            new Vector3(0, 1)  // Le coin supérieur gauche
        };
        CreateShape(vertices);
    }
}