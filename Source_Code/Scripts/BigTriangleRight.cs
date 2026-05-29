using UnityEngine;

public class BigTriangleRight : TangramShape
{
    void OnValidate()
    {
        // On ramène la base du triangle sur Y = 0 pour coller au pivot
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(2, -2), // Coin inférieur droit
            new Vector3(2, 0),  // Coin supérieur droit (l'angle droit)
            new Vector3(0, 0)   // Le coin supérieur gauche devient le pivot local (0,0) !
        };
        CreateShape(vertices);
    }
}