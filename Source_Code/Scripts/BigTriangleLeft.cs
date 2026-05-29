using UnityEngine;

public class BigTriangleLeft : TangramShape
{
    void OnValidate()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0),
            new Vector3(2, 0),
            new Vector3(0, 2)
        };
        CreateShape(vertices);
    }
}
